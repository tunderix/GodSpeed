using System;
using Ioni.SceneReferencing;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ioni.DP.Singleton
{
    [Serializable]
    public class Configuration
    {
        [SerializeField] private SceneReference managerScene;
        [SerializeField] private CSceneManager sceneManager;
        [SerializeField] private bool scenePersistent;
        public bool ScenePersistent => scenePersistent;
        public SceneReference ManagerScene => managerScene;

        public CSceneManager Manager => this.sceneManager;
    }
}
