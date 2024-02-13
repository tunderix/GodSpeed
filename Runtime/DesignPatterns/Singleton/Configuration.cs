using System;
using System.ComponentModel;
using Ioni.SceneReferencing;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Ioni.DesignPatterns.Singleton
{
    [Serializable]
    public class Configuration
    {
        [Header("Which initialization to priorize")]
        [Tooltip("Should the singleton be transferred to scene or make it a dont-destroy-on-load singleton?")]
        [SerializeField] private SingletonScenePriorization priorization;
        
        [Header("Should your scene be persistent")]
        [Tooltip("Initializes the scene as a dont-destroy-on-load")]
        [SerializeField] private bool scenePersistent;
        [Space(4)]
        [Header("Initialization of singleton into a scene")]
        [Tooltip("The scene you want singleton to transfer into")]
        [SerializeField] private SceneReference initializeInto;
        [Tooltip("The common scene manager required")]
        [SerializeField] private SceneManagement sceneManagement;

        private bool _initializedFromCode = false;
        public bool InitializedFromCode
        {
            get { return _initializedFromCode; }
        }
        
        public Configuration()
        {
            _initializedFromCode = true;
        }
        
        public bool ScenePersistent => scenePersistent;
        public SceneReference InitializeInto => initializeInto;
        public SceneManagement SceneManagement => sceneManagement;

        public SingletonScenePriorization ScenePrio => priorization;
        
        public bool ShouldStayInScene => _initializedFromCode || (!scenePersistent && (initializeInto == null || SceneManagement == null));
        private bool _initIntoSceneIsSet => initializeInto != null && SceneManagement != null;
        private bool sceneAllocPrio => scenePersistent && priorization == SingletonScenePriorization.SceneAllocation;
        public bool ShouldDirectToInitializeInto => !_initializedFromCode && _initIntoSceneIsSet && sceneAllocPrio;
        public bool ShouldDirectToDdol => !_initializedFromCode;
    }
}
