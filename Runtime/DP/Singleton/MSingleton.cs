using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ioni.DP.Singleton
{
    /// <summary>
    /// The basic MonoBehaviour singleton implementation, this singleton is destroyed after scene changes, use <see cref="PersistentMonoSingleton{T}"/> if you want a persistent and global singleton instance.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MSingleton<T> : MonoBehaviour, ISingleton where T : MSingleton<T>
    {

        #region Fields

        /// <summary>
        /// The instance.
        /// </summary>
        private static T _instance;

        /// <summary>
        /// The initialization status of the singleton's instance.
        /// </summary>
        private SingletonInitializationStatus _initializationStatus = SingletonInitializationStatus.None;

        [field: SerializeField] private Configuration configuration;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static T Instance
        {
            get
            {
                if (_instance != null) return _instance;
                _instance = FindObjectOfType<T>();
                if (_instance != null) return _instance;
                GameObject obj = new GameObject();
                obj.name = typeof(T).Name;
                _instance = obj.AddComponent<T>();
                _instance.OnMonoSingletonCreated();
                return _instance;
            }
        }

        /// <summary>
        /// Gets whether the singleton's instance is initialized.
        /// </summary>
        public virtual bool IsInitialized => _initializationStatus == SingletonInitializationStatus.Initialized;

        #endregion

        #region Unity Messages

        /// <summary>
        /// Use this for initialization.
        /// </summary>
        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;

                // Initialize existing instance
                InitializeSingleton();
            }
            else
            {
                // Destory duplicates
                if (Application.isPlaying)
                {
                    Destroy(gameObject);
                }
                else
                {
                    DestroyImmediate(gameObject);
                }
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// This gets called once the singleton's instance is created.
        /// </summary>
        protected virtual void OnMonoSingletonCreated()
        {
            
        }

        private void OnSceneLoaded(int sceneBuildIndex)
        {
            
        }
        
        protected virtual void OnInitializing()
        {
            // If scene is given, move the singleton to that scene
            if (Application.isPlaying && configuration.ManagerScene.IsSet && configuration.Manager != null)
            {
                var sceneBuildIndex = configuration.ManagerScene.BuildIndex;

                configuration.Manager.LoadSceneAsync(sceneBuildIndex, LoadSceneMode.Additive,
                    (int sceneBuildIndex) =>
                    {
                        SceneManager.MoveGameObjectToScene(this.gameObject,
                            SceneManager.GetSceneByBuildIndex(sceneBuildIndex));
                    });
            }
            
            // If scene is not given and the singleton is marked persistent, DDOL
            if (Application.isPlaying && configuration.ScenePersistent)
            {
                DontDestroyOnLoad(gameObject);
            }
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
            _initializationStatus = SingletonInitializationStatus.Initialized;
            OnInitialized();
        }

        public virtual void ClearSingleton() { }

        public static void CreateInstance()
        {
            DestroyInstance();
            _instance = Instance;
        }

        public static void DestroyInstance()
        {
            if (_instance == null)
            {
                return;
            }

            _instance.ClearSingleton();
            _instance = default(T);
        }

        #endregion

    }
}
