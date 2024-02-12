
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ioni.DesignPatterns.Singleton;
using Ioni.Runtime.DesignPatterns.Singleton;
using Ioni.Runtime.Utilities;
using Ioni.SceneReferencing;
using Ioni.Tests.PlayMode.DesignPatterns;
using NUnit.Framework;
using NUnit.Framework.Internal;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Ioni.Tests.PlayMode.DesignPatternTests
{
    public class SingletonTests
    {
        private string[] scenes = new []{"SingletonTestScene", "APP"};
        
        [UnitySetUp]
        public IEnumerator SetUpTest()
        {
            yield return TestUtilities.LoadScene(scenes[0], LoadSceneMode.Single);
            yield return TestUtilities.LoadScene(scenes[1], LoadSceneMode.Additive);
        }
        
        [UnityTearDown]
        public IEnumerator TearDownTestScene()
        {
            yield return TestUtilities.LoadScene(scenes[0], LoadSceneMode.Single);
            yield return TestUtilities.LoadScene(scenes[1], LoadSceneMode.Additive);
        }
        
        [Test]
        public void Verify_application_is_running()
        {
            Assert.That(Application.isPlaying, Is.True);
        }
        
        [Test]
        public void Verify_scenes_are_running()
        {
            var testScene = SceneManager.GetSceneByName("SingletonTestScene");
            var appScene = SceneManager.GetSceneByName("APP");
            Assert.That(testScene, Is.Not.Null, "TestScene scene found at {0}", testScene.path);
            Assert.That(appScene, Is.Not.Null, "APPScene scene found at {0}", appScene.path);
        }

        [UnityTest]
        public IEnumerator Verify_GetComponent_is_working()
        {
            yield return null;
            var go = GameObject.Find("Non-Persistent");
            
            Assert.That(go, Is.Not.Null);
        }
        
        [UnityTest]
        public IEnumerator Verify_needed_singletons_exist()
        {
            var s_persistent = GameObject.Find("Persistent");
            var s_nonPersistent = GameObject.Find("Non-Persistent");
            var s_initializeIntoScene = GameObject.Find("InitializeIntoScene");
            var s_initializeIntoSceneWithSceneManagement = GameObject.Find("InitializeIntoSceneWithSceneManagement");
            var s_persistentWithSceneManagement = GameObject.Find("PersistentWithSceneManagement");
            var s_initializePersistentIntoSceneWithSceneManagement = GameObject.Find("InitializePersistentIntoSceneWithSceneManagement");
            yield return null;
            Assert.That(s_persistent, Is.Not.Null);
            Assert.That(s_nonPersistent, Is.Not.Null);
            Assert.That(s_initializeIntoScene, Is.Not.Null);
            Assert.That(s_initializeIntoSceneWithSceneManagement, Is.Not.Null);
            Assert.That(s_persistentWithSceneManagement, Is.Not.Null);
            Assert.That(s_initializePersistentIntoSceneWithSceneManagement, Is.Not.Null);
        }

        [Test]
        public void Verify_Persistent_initializes_in_ddol()
        {
            var singleton = GameObject.Find("Persistent");
            Assert.That(singleton.scene.name.Equals("DontDestroyOnLoad"));
        }

        [Test]
        public void Verify_NonPersistent_initializes_in_ddol()
        {
            var singleton = GameObject.Find("Non-Persistent");
            Assert.That(singleton.scene.name.Equals("SingletonTestScene"));
        }
        
        [Test]
        public void Verify_InitializeIntoScene_initializes_in_ddol()
        {
            var singleton = GameObject.Find("InitializeIntoScene");
            Assert.That(singleton.scene.name.Equals("SingletonTestScene"));
        }
        
        [Test]
        public void Verify_InitializeIntoSceneWithSceneManagement_initializes_in_ddol()
        {
            var singleton = GameObject.Find("InitializeIntoSceneWithSceneManagement");
            Assert.That(singleton.scene.name.Equals("APP"));
        }
        
        [Test]
        public void Verify_PersistentWithSceneManagement_initializes_in_ddol()
        {
            var singleton = GameObject.Find("PersistentWithSceneManagement");
            Assert.That(singleton.scene.name.Equals("DontDestroyOnLoad"));
        }
        
        [Test]
        public void Verify_InitializePersistentIntoSceneWithSceneManagement_initializes_in_ddol()
        {
            var singleton = GameObject.Find("InitializePersistentIntoSceneWithSceneManagement");
            Assert.That(singleton.scene.name.Equals("APP"));
        }
        
        [Test]
        public void No_duplicates_when_accessed()
        {
            var singleton = GameObject.Find("InitializeIntoSceneWithSceneManagement");
            InitializeIntoSceneWithSceneManagement.Instance.gameObject.name = "RENAMED";
            var singletons = GameObject.FindObjectsOfType<InitializeIntoSceneWithSceneManagement>();
            Assert.That(singleton.name == "RENAMED");
            Assert.That(singletons.Length == 1);
        }
        
        [UnityTest]
        public IEnumerator Make_sure_Initialization_works_through_instance()
        {
            var go = GameObject.Find("InitializeTest");
            Assert.That(go, Is.Null);
            
            var S_NAME = InitializeTest.Instance.S_NAME;
            yield return new WaitUntil(() => InitializeTest.Instance.IsInitialized);
            var singleton = GameObject.Find("InitializeTest - Generated");
            Assert.That(singleton, Is.Not.Null);
            Assert.That(S_NAME == "WORKS");
        }
    }
}
