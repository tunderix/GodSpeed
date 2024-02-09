using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ioni.DP.Singleton
{
    public struct SceneOperation
    {
        public int SceneIndex;
        public AsyncOperation Operation;

        public SceneOperation(int sceneBuildIndex, AsyncOperation operation)
        {
            SceneIndex = sceneBuildIndex;
            Operation = operation;
        }
    }
    
    public class CSceneManager : MonoBehaviour
    {
        private List<SceneOperation> operations = new List<SceneOperation>();

        public void LoadSceneAsync(int sceneBuildIndex, LoadSceneMode mode, Action<int> callback )
        {
            var scene = SceneManager.GetSceneByBuildIndex(sceneBuildIndex);
            if (!scene.isLoaded)
            {
                if (!operations.Exists(o => o.SceneIndex == sceneBuildIndex))
                {
                    var operation = SceneManager.LoadSceneAsync(sceneBuildIndex, mode);
                
                    operations.Add(new SceneOperation(sceneBuildIndex, operation));
                    operation.completed += operation => callback(sceneBuildIndex);
                }
                else
                {
                    var so = operations.Find(o => o.SceneIndex == 1);
                    so.Operation.completed += operation => callback(sceneBuildIndex);
                }
            }
        }

        public void LoadSceneAsync(string sceneName, LoadSceneMode mode)
        {
        }
    }
}
