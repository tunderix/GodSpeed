using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ioni.DesignPatterns.Singleton
{
    /// <summary>
    /// Represents a singleton for MonoBehaviour classes.
    /// </summary>
    /// <typeparam name="T">Type of MonoSingleton</typeparam>
    public abstract class MonoSingleton<T> : MonoBehaviour, ISingleton where T : MonoSingleton<T>
    {

        #region Fields

        private static T _instance;

        /// <summary>
        /// Status representing the initialization status of the Singleton.
        /// </summary>
        private SingletonInitializationStatus _initializationStatus = SingletonInitializationStatus.None;
        
        /// <summary>
        /// Configuration for the Singleton.
        /// </summary>
        [field: SerializeField] private Configuration configuration;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the singleton instance of T.
        /// If an instance already exists, the existing instance will be returned.
        /// If an instance does not already exist, it will attempt to find T attached to an object in the scene. 
        /// If not found, a new GameObject will be created and T will be added as a component and returned.
        /// </summary>
        /// <returns>The singleton instance of T.</returns>
        /// <example>
        /// <code>
        /// public class MyClass : MonoSingleton&lt;MyClass&gt;
        /// {
        ///     void SomeMethod()
        ///     {
        ///         Debug.Log(Instance.transform.position);
        ///     }
        /// }
        /// </code>
        /// </example>
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

        /// <summary>
        /// Gets a value indicating whether the singleton is initialized.
        /// </summary>
        /// <returns>
        /// true if the singleton is initialized; otherwise, false.
        /// </returns>
        public virtual bool IsInitialized => _initializationStatus == SingletonInitializationStatus.Initialized;

        /// <summary>
        /// Gets a value indicating whether the singleton is currently initializing.
        /// </summary>
        /// <returns>
        /// true if the singleton is currently initializing; otherwise, false.
        /// </returns>
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

        /// <summary>
        /// Method that is called when the Singleton is created.
        /// Overridable for custom behaviour.
        /// </summary>
        protected virtual void OnMonoSingletonCreated()
        {
            
        }
        
        /// <summary>
        /// Resets the current configuration of the singleton with a new provided configuration.
        /// </summary>
        /// <param name="newConfiguration">The new configuration that will be used to reset the singleton's configuration.</param>
        public void ResetConfiguration(Configuration newConfiguration)
        {
            configuration = newConfiguration;
        }
        
        /// <summary>
        /// This method is called when the singleton is initializing.
        /// It's responsible for setting up the configuration of the singleton according to the application's play status and the current configuration state.
        /// </summary>
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
            else if (configuration.ShouldStayInScene || configuration.ShouldDirectToInitializeInto)
            {
                ConfigurationLoadScene();
            }
        }

        /// <summary>
        /// Loads the scene specified in the configuration asynchronously and additively.
        /// The load operation is only performed if the 'InitializeInto' and 'SceneManagement' configuration properties are not null 
        /// and the 'BuildIndex' of 'InitializeInto' is not equal to -1.
        /// </summary>
        private void ConfigurationLoadScene()
        {
            if (configuration.InitializeInto == null || configuration.SceneManagement == null) return;
            if (configuration.InitializeInto.BuildIndex.Equals(-1)) return;
            configuration.SceneManagement.LoadSceneAsync(configuration.InitializeInto.BuildIndex, LoadSceneMode.Additive, OnManagementLoadScene);
        }
        
        /// <summary>
        /// Moves the current GameObject to the specified scene.
        /// </summary>
        /// <param name="sceneBuildIndex">The build index of the scene to which the GameObject should be moved.</param>
        private void OnManagementLoadScene(int sceneBuildIndex)
        {
            configuration.SceneManagement.MoveToScene(gameObject, sceneBuildIndex);
        }

        /// <summary>
        /// This optional method is called after the singleton has been initialized.
        /// It can be overridden in derived classes to perform custom behavior upon initialization.
        /// </summary>
        protected virtual void OnInitialized()
        {
            
        }
        
        #endregion

        #region Public Methods
        
        /// <summary>
        /// Initializes the singleton if it has not already been initialized.
        /// This method sets the initialization status, calls the <see cref="OnInitializing"/> method, checks if a singleton instance already exists, 
        /// handles the case if another instance tries to initialize, sets singleton instance and its persistence and finally calls the <see cref="OnInitialized"/> method.
        /// </summary>
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
