using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ioni.Runtime.Utilities
{
    public static class TestUtilities
    {
        public static IEnumerator LoadScene(string sceneName, LoadSceneMode mode)
        {
            var loadSceneOperation = SceneManager.LoadSceneAsync(sceneName, mode);
            loadSceneOperation.allowSceneActivation = true;
 
            while (!loadSceneOperation.isDone)
                yield return null;
            
            var scene = SceneManager.GetSceneByName(sceneName);
            yield return new WaitUntil(() => scene.isLoaded);
        }
    }
}
