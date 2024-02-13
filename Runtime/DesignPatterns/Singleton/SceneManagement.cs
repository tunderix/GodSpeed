using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ioni.DesignPatterns.Singleton
{
    /// <summary>
    /// Manages scene operations in the game, such as moving game objects between scenes, and loading scenes asynchronously.
    /// </summary>
    /// <example>
    /// This sample shows how to use SceneManagement class.
    /// <code>
    /// class ExampleClass : MonoBehaviour
    /// {
    ///    void Start()
    ///    {
    ///       var sceneManagement = new SceneManagement();
    ///       var myGameObject = GameObject.Find("MyGameObject");
    /// 
    ///       // Example of MoveToScene usage
    ///       sceneManagement.MoveToScene(myGameObject, 1);
    ///
    ///       // Example of LoadSceneAsync usage.
    ///       sceneManagement.LoadSceneAsync(1, LoadSceneMode.Additive, (sceneIndex) => Debug.Log("Scene loaded! Index: " + sceneIndex));
    ///     }
    /// }
    /// </code>
    /// </example>
    /// <remarks>
    /// Use this class when you need to perform scene-level operations such as moving GameObjects between scenes or loading scenes asynchronously.
    /// </remarks>
    public class SceneManagement : MonoBehaviour
    {
        [SerializeField] private List<SceneOperation> operations = new List<SceneOperation>();

        /// <summary>
        /// Moves a given GameObject to a specified scene via its build index.
        /// </summary>
        /// <param name="toMove">The GameObject to be moved.</param>
        /// <param name="sceneBuildIndex">The build index of the scene to which the GameObject will be moved.</param>
        /// <example>
        /// <code>
        /// var sceneManagement = new SceneManagement();
        /// var myGameObject = GameObject.Find("MyGameObject");
        /// sceneManagement.MoveToScene(myGameObject, 1);
        /// </code>
        /// </example>
        /// <remarks>
        /// Use this method when you want to move a GameObject from its current scene to another scene.
        /// </remarks>
        public void MoveToScene(GameObject toMove, int sceneBuildIndex)
        {
            var scene = SceneManager.GetSceneByBuildIndex(sceneBuildIndex);
            SceneManager.MoveGameObjectToScene(toMove, scene);
        }
        
        /// <summary>
        /// Loads a scene asynchronously, represented by its build index, and calls a callback upon completion.
        /// </summary>
        /// <param name="sceneBuildIndex">The build index of the scene to be loaded.</param>
        /// <param name="mode">The mode to use when loading the scene.</param>
        /// <param name="callback">The callback function to be called once the scene has been loaded.</param>
        /// <example>
        /// <code>
        /// var sceneManagement = new SceneManagement();
        /// sceneManagement.LoadSceneAsync(1, LoadSceneMode.Additive, (sceneIndex) => Debug.Log("Scene loaded! Index: " + sceneIndex));
        /// </code>
        /// </example>
        /// <remarks>
        /// This method is used when you want to load a new scene asynchronously and perform actions upon completion of the scene load.
        /// </remarks>
        public void LoadSceneAsync(int sceneBuildIndex, LoadSceneMode mode, Action<int> callback )
        {
            var scene = SceneManager.GetSceneByBuildIndex(sceneBuildIndex);
            if (scene.isLoaded) return;
            if (!operations.Exists(o => o.SceneIndex == sceneBuildIndex))
            {
                var operation = SceneManager.LoadSceneAsync(sceneBuildIndex, mode);
                
                operations.Add(new SceneOperation(sceneBuildIndex, operation));
                operation.completed += operation => callback(sceneBuildIndex);
            }
            else
            {
                var so = operations.Find(o => o.SceneIndex == sceneBuildIndex);
                so.Operation.completed += operation => callback(sceneBuildIndex);
            }
        }
    }
}
