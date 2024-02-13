using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.Serialization;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Ioni.SceneReferencing
{
    /// <summary>
    /// Makes it possible to assign a scene asset in the inspector and load the scene data in a build.
    /// </summary>

    [Serializable]
    public class SceneReference
    #if UNITY_EDITOR
    : ISerializationCallbackReceiver
    #endif
    {
        #region Parameters
        
        #if UNITY_EDITOR
        [SerializeField] SceneAsset sceneAsset;
        [FormerlySerializedAs("logErrorIfNotInBuild")]
        [SerializeField] bool required;
        #endif

        #pragma warning disable 414
        [SerializeField] int buildIndex;
        #pragma warning restore 414
        
        #endregion

        ///<summary>
        ///Gets the build index of the Unity Scene, and checks if a required scene is added to the build in the Unity Editor.
        ///</summary>
        ///<value>
        ///Returns the build index of the Unity Scene.
        ///</value>
        ///<remarks>
        ///In the Unity Editor, the scene asset's build index is refreshed every time this property is accessed. 
        ///If the scene is marked as required but isn't added to the build, or no scene is assigned, an error message will be logged.
        ///</remarks>
        ///<exception cref="System.Exception">Thrown when a required scene is not added to the build or no scene is assigned in the Unity Editor.</exception>
        ///<example>
        ///This is an example of how to use this property:
        ///<code>
        ///int index = YourObject.BuildIndex;
        ///</code>
        ///</example>
        public int BuildIndex
        {
            get
            {
                #if UNITY_EDITOR
                {
                    buildIndex = GetSceneBuildIndex(sceneAsset);
                    if (!required || buildIndex >= 0) return buildIndex;
                    var errorMessage = sceneAsset != null
                        ? $"isn't added to builds: {AssetDatabase.GetAssetPath(sceneAsset)}"
                        : "no scene is assigned";
                    D.Err($"SceneReference: Marked as required but {errorMessage}");
                }
                #endif

                return buildIndex;
            }
        }

        /// <summary>
        /// Is the build index found for scene
        /// Checks whether build index != -1
        /// </summary>
        public bool IsSet => buildIndex != -1;

        
        #region Implicit Operations
        /// <summary>
        /// Operate SceneReferences scene name as string 
        /// </summary>
        /// <param name="sceneReference">this</param>
        /// <returns>SceneReference name</returns>
        public static implicit operator string(SceneReference sceneReference)
        {
            return sceneReference.sceneAsset.name;
        }
        
        /// <summary>
        /// Operate SceneReferences build index as int 
        /// </summary>
        /// <param name="sceneReference">this</param>
        /// <returns>SceneReference build index</returns>
        public static implicit operator int(SceneReference sceneReference)
        {
            return sceneReference.buildIndex;
        }

        #endregion
        
        #region ISerializationCallbackReceiver implementation
        #if UNITY_EDITOR
        
        /// <summary>
        /// Implementation of <see cref="ISerializationCallbackReceiver.OnBeforeSerialize"/>.
        /// </summary>
        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            buildIndex = GetSceneBuildIndex(sceneAsset);
            
            if (required  &&  buildIndex < 0)
                BuildProcessor.AddMissingRequiredSceneBuildError(sceneAsset);
        }

        /// <summary>
        /// Implementation of <see cref="ISerializationCallbackReceiver.OnAfterDeserialize"/>.
        /// </summary>

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
        }
        
        #endif
        #endregion
        
        #region Build Processor
        #if UNITY_EDITOR
        private class BuildProcessor : IPreprocessBuildWithReport, IPostprocessBuildWithReport
        {
            static readonly HashSet<SceneAsset> MissingRequiredSceneAssets = new();
            static bool _requiredSceneIsUnassigned;


            /// <summary>
            /// Adds a missing required scene error to be shown when building.
            /// The added errors will be cleared when a new build is started.
            /// </summary>
            /// <param name="sceneAsset">The asset of the scene missing in the build. Can be null.</param>
            public static void AddMissingRequiredSceneBuildError(SceneAsset sceneAsset)
            {
                if (sceneAsset != null)
                    MissingRequiredSceneAssets.Add(sceneAsset);
                else
                    _requiredSceneIsUnassigned = true;
            }
            
                
            /// <summary>
            /// Implementation of <see cref="IOrderedCallback.callbackOrder"/>.
            /// </summary>
            int IOrderedCallback.callbackOrder => 0;


            /// <summary>
            /// Implementation of <see cref="IPreprocessBuildWithReport.OnPreprocessBuild"/>.
            /// </summary>
            void IPreprocessBuildWithReport.OnPreprocessBuild(BuildReport report)
            {
                UpdateCachedBuildIndexes();
                MissingRequiredSceneAssets.Clear();
                _requiredSceneIsUnassigned = false;
            }


            ///
            /// <summary>
            /// Implementation of <see cref="IPostprocessBuildWithReport.OnPostprocessBuild"/>.
            /// </summary>
            void IPostprocessBuildWithReport.OnPostprocessBuild(BuildReport report)
            {
                string errorMessage = null;

                if (_requiredSceneIsUnassigned)
                    errorMessage += "  - A required scene field doesn't have an assigned scene";
                
                if (MissingRequiredSceneAssets.Count > 0)
                {
                    if (errorMessage != null)
                        errorMessage += "\n";
                    errorMessage += "  - The following scenes are assigned to scene fields as required, " +
                                    "but aren't added to builds:";
                    errorMessage = MissingRequiredSceneAssets.Aggregate(errorMessage, (current, sceneAsset) 
                        => current + $"\n    - {AssetDatabase.GetAssetPath(sceneAsset)}");

                    MissingRequiredSceneAssets.Clear();
                }

                if (errorMessage == null) return;
                errorMessage = "SceneReference: The following errors have been found:\n" + errorMessage;
                throw new BuildFailedException(errorMessage);
            }
        }

        #endif
        #endregion

        #region Editor Members
        #if UNITY_EDITOR
        static Dictionary<SceneAsset, int> _cachedBuildIndexes = new();

        /// <summary>
        /// Updates the cached build indexes.
        /// </summary>
        private static void UpdateCachedBuildIndexes()
        {
            _cachedBuildIndexes.Clear();

            var buildIndex = -1;
            foreach (var scene in EditorBuildSettings.scenes)
            {
                if (!scene.enabled) continue;
                buildIndex++;
                var sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(scene.path);
                if (sceneAsset != null)
                {
                    _cachedBuildIndexes.Add(sceneAsset, buildIndex);
                }
            }
        }

        ///<summary>
        ///This method is called upon loading the editor thanks to the InitializeOnLoadMethod attribute.
        ///It updates the cache of scene build indexes and binds the method to the sceneListChanged event of the EditorBuildSettings.
        ///</summary>
        ///<remarks>
        ///The method ensures it doesn't multiple times subscribe to the sceneListChanged event by first unsubscribing.
        ///This method is useful for initializing things when the editor first loads and for responding to changes in the editor's build settings scene list.
        ///</remarks>
        ///<example>
        ///This method is automatically called upon loading the editor and does not normally need to be manually invoked.
        ///</example>
        [InitializeOnLoadMethod]
        private static void OnEditorInitializeOnLoad()
        {
            UpdateCachedBuildIndexes();

            EditorBuildSettings.sceneListChanged -= UpdateCachedBuildIndexes;
            EditorBuildSettings.sceneListChanged += UpdateCachedBuildIndexes;
        }

        /// <summary>
        /// ** Editor-only **
        /// Gets the scene asset, if assigned.
        /// </summary>
        public SceneAsset EditorSceneAsset => sceneAsset;

        ///<summary>
        ///Gets the build index for the provided scene from a cache.
        ///</summary>
        ///<param name="sceneAsset">The SceneAsset from which to get the build index.</param>
        ///<returns>Returns the cached build index for the supplied SceneAsset. If sceneAsset is null or if the index is not in the cache, returns -1.</returns>
        ///<example>
        ///This is an example of how to use the `GetSceneBuildIndex` method:
        ///<code>
        /// int buildIndex = YourClassName.GetSceneBuildIndex(sceneAsset);
        ///</code>
        ///</example>
        public static int GetSceneBuildIndex(SceneAsset sceneAsset)
        {
            if (sceneAsset == null  ||  !_cachedBuildIndexes.TryGetValue(sceneAsset, out int buildIndex))
                return -1;
            return buildIndex;
        }
        #endif
        #endregion
    }
}
