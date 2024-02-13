using System;
using Ioni.SceneReferencing;
using UnityEngine;

namespace Ioni.DesignPatterns.Singleton
{
    /// <summary>
    /// Defines the configuration for Singleton Initialization.
    /// This includes scene priorization, scene persistence and setting for the scene into which the singleton should be transferred.
    /// </summary>
    [Serializable]
    public class Configuration
    {
        /// <summary>
        /// Gets the SingletonScenePriorization used to decide to which scene the singleton should be transferred.
        /// </summary>
        /// <returns>A value of type SingletonScenePriorization that represents the priorization used for initialization.</returns>
        [Header("Which initialization to priorize")]
        [Tooltip("Should the singleton be transferred to scene or make it a dont-destroy-on-load singleton?")]
        [SerializeField] private SingletonScenePriorization priorization;
        
        /// <summary>
        /// Gets a value that indicates whether the scene should be persistent.
        /// </summary>
        /// <returns>Returns a boolean indication whether the scene should be persistent.</returns>
        [Header("Should your scene be persistent")]
        [Tooltip("Initializes the scene as a dont-destroy-on-load")]
        [SerializeField] private bool scenePersistent;
        
        /// <summary>
        /// Gets the SceneReference into which the singleton should be initialized.
        /// </summary>
        /// <returns>A SceneReference representing the scene into which the singleton should be initialized.</returns>
        [Space(4)]
        [Header("Initialization of singleton into a scene")]
        [Tooltip("The scene you want singleton to transfer into")]
        [SerializeField] private SceneReference initializeInto;
        
        /// <summary>
        /// Gets the common SceneManagement object required for scene manipulation.
        /// </summary>
        /// <returns>The SceneManagement object required for scene manipulation.</returns>
        [Tooltip("The common scene manager required")]
        [SerializeField] private SceneManagement sceneManagement;

        private bool _initializedFromCode = false;

        public Configuration()
        {
            _initializedFromCode = true;
        }
        
        /// <summary>
        /// Gets a value indicating whether the instance was initialized from code.
        /// </summary>
        /// <returns>A boolean value, true if the C# singleton instance is initialized from code, false otherwise.</returns>
        public bool InitializedFromCode => _initializedFromCode;
        
        /// <summary>
        /// Gets a value indicating whether the scene should be persisted.
        /// </summary>  
        /// <returns>A boolean value indicating whether the scene is set to be persistent or not.</returns>  
        public bool ScenePersistent => scenePersistent;
        
        /// <summary>
        /// Gets the scene reference into which the singleton should be initialized.
        /// </summary>  
        /// <returns>An object of type SceneReference representing the scene into which the singleton should be initialized.</returns>  
        public SceneReference InitializeInto => initializeInto;
        
        /// <summary>
        /// Gets the scene management object.
        /// </summary>  
        /// <returns>An object of type SceneManagement used for managing the scene.</returns>       
        public SceneManagement SceneManagement => sceneManagement;

        /// <summary>
        /// Gets the scene priorization setting.
        /// </summary>
        /// <returns>An enum of SingletonScenePriorization specifying the scene priorization setting.</returns>
        public SingletonScenePriorization ScenePrio => priorization;
        
        /// <summary>
        /// Determines whether the singleton should stay in the current scene.
        /// </summary>
        /// <returns>Returns true if the singleton should stay in the current scene, otherwise false.</returns>
        public bool ShouldStayInScene => _initializedFromCode || (!scenePersistent && (initializeInto == null || SceneManagement == null));
        
        /// <summary>
        /// Determines whether initialization into scene is set.
        /// </summary>
        /// <returns>Returns true if both SceneReference and SceneManagement are not null, otherwise false.</returns>
        private bool _initIntoSceneIsSet => initializeInto != null && SceneManagement != null;
        
        /// <summary>
        /// Determines whether SceneAllocation should take priority when using scene persistent singleton.
        /// </summary>
        /// <returns>Returns true if SceneAllocation should take priority, otherwise false.</returns>
        private bool sceneAllocPrio => scenePersistent && priorization == SingletonScenePriorization.SceneAllocation;
        
        /// <summary>
        /// Determines whether the singleton should direct to InitializeInto.
        /// </summary>
        /// <returns>Returns true if the singleton should direct to InitializeInto, otherwise false.</returns>
        public bool ShouldDirectToInitializeInto => !_initializedFromCode && _initIntoSceneIsSet && sceneAllocPrio;
        
        /// <summary>
        /// Determines whether the singleton should direct to DontDestroyOnLoad.
        /// </summary>
        /// <returns>Returns true if the singleton should direct to DontDestroyOnLoad, otherwise false.</returns>
        public bool ShouldDirectToDdol => !_initializedFromCode;
    }
}
