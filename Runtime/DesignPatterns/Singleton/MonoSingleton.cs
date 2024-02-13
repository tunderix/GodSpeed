using Codice.Client.BaseCommands.Changelist;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ioni.DesignPatterns.Singleton
{
    public abstract class MonoSingleton<T> : MonoBehaviour, ISingleton where T : MonoSingleton<T>
    {

        #region Fields

        private static T _instance;

        private SingletonInitializationStatus _initializationStatus = SingletonInitializationStatus.None;

        [field: SerializeField] private Configuration configuration;

        #endregion

        #region Properties

        public static T Instance
        {
            get
            {
                if (_instance != null) return _instance;
                _instance = FindObjectOfType<T>();
                if (_instance != null) return _instance;
                GameObject obj = new GameObject();
                obj.name = typeof(T).Name + " - Generated";
                _instance = obj.AddComponent<T>();
                _instance.OnMonoSingletonCreated();
                return _instance;
            }
        }

        public virtual bool IsInitialized => _initializationStatus == SingletonInitializationStatus.Initialized;
        public virtual bool IsInitializing => _initializationStatus == SingletonInitializationStatus.Initializing;

        #endregion

        #region Unity Messages

        protected virtual void Awake()
        {
            if (_initializationStatus != SingletonInitializationStatus.Initialized)
            {
                InitializeSingleton();
            }
        }

        #endregion

        #region Protected Methods

        protected virtual void OnMonoSingletonCreated()
        {
            
        }

        public void ResetConfiguration(Configuration newConfiguration)
        {
            configuration = newConfiguration;
        }

        private void ConfigurationLoadScene()
        {
            if (configuration.InitializeInto == null || configuration.SceneManagement == null) return;
            if (configuration.InitializeInto.BuildIndex.Equals(-1)) return;
            configuration.SceneManagement.LoadSceneAsync(configuration.InitializeInto.BuildIndex, LoadSceneMode.Additive, OnManagementLoadScene);
        }
        
        protected virtual void OnInitializing()
        {
            if (!Application.isPlaying || IsInitialized) return;

            configuration ??= new Configuration();

            
            if (configuration.ShouldDirectToDdol)
            {
                if (!configuration.ScenePersistent)
                {
                    ConfigurationLoadScene();
                }
                else
                {
                    DontDestroyOnLoad(gameObject);
                }
            }
            else if (configuration.ShouldStayInScene)
            {
                ConfigurationLoadScene();
            }
            else if (configuration.ShouldDirectToInitializeInto)
            {
                ConfigurationLoadScene();
            }
        }

        private void OnManagementLoadScene(int sceneBuildIndex)
        {
            configuration.SceneManagement.MoveToScene(gameObject, sceneBuildIndex);
        }

        protected virtual void OnInitialized()
        {
            
        }
        #endregion

        #region Public Methods

        public virtual void InitializeSingleton()
        {
            if (_initializationStatus != SingletonInitializationStatus.None)
            {
                return;
            }

            _initializationStatus = SingletonInitializationStatus.Initializing;
            OnInitializing();
            
            if (_instance == null) _instance = this as T;
            else if (_instance != this)
            {
                Debug.LogWarning($"Singleton already exits of type {GetType().Name}. Destroying.");
                Destroy(this);
                return;
            }

            if (configuration.ScenePersistent) DontDestroyOnLoad(this);

            _initializationStatus = SingletonInitializationStatus.Initialized;
            OnInitialized();
        }
        
        #endregion

    }
}
