using System;
using Ioni;
using Ioni.Extensions;
using UnityEngine;

namespace Ioni.DP
{
    /*
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;
        public static bool HasInstance => _instance != null;

        [field: SerializeField] private bool persistentThroughScenes = true;
        [field: SerializeField] private bool debugMode = false;
        
        public static T Instance
        {
            get
            {
                if (HasInstance)
                {
                    return _instance;
                }
                
                _instance = FindFirstObjectByType<T>();
                if (HasInstance)
                {
                    return _instance;
                }

                var gameObject = new GameObject
                {
                    name = typeof(T).Name.WithSuffix(" - AutoCreated Singleton")
                };
                _instance = gameObject.AddComponent<T>();
                
                return _instance;
            }
        }

        protected virtual void Awake() => InitializeSingleton();

        protected virtual void InitializeSingleton()
        {
            if (!Application.isPlaying)
            {
                Log($"Initialization of Singleton<{typeof(T).Name}> while application is not running");
                return;
            }
            
            if(debugMode) 
                Log($"Initialization of Singleton<{typeof(T).Name}>");
            

            if (HasInstance && Instance != this)
            {
                Log($"Initialization of Singleton<{typeof(T).Name}>. Found another instance " +
                    $"called {Instance.name}. Destroying my own object.");
                Destroy(gameObject);
                return;
            }
            
            _instance = Instance;

            if (!persistentThroughScenes) return;
            Log($"Initialization of Singleton<{typeof(T).Name}>. Setting myself persistent " +
                $"among scenes");
            DontDestroyOnLoad(_instance.gameObject);
        }

        private void Log(string message)
        {
            if(debugMode) D.Info(message);   
        }
    }
    */
}
