using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ioni.Runtime.Utilities
{
    public static class TestUtilities
    {
        ///<summary>
        ///Asynchronously loads the scene by its name. This function returns IEnumerator and should be used inside a coroutine.
        ///</summary>
        ///<param name="sceneName">The name of the scene to load.</param>
        ///<param name="mode">Determines how the scene should be loaded. Use 'LoadSceneMode.Single' to close all currently loaded scenes before loading the scene, or 'LoadSceneMode.Additive' to keep current scenes loaded and add the new scene.</param>
        ///<returns>An IEnumerator, suitable for using with Unity Coroutines, that represents the async loading operation.</returns>
        ///<remarks>
        ///The scene loading is done asynchronously - the function does not block the execution and returns immediately, but the scene loading is done over multiple frames.
        ///</remarks>
        ///<example>
        ///Here is an example of how to use the `LoadScene` method within a Coroutine:
        ///<code>
        ///using UnityEngine;
        ///using System.Collections;
        ///
        ///public class ExampleClass : MonoBehaviour {
        ///    void Start() {
        ///        StartCoroutine(YourClassName.LoadScene("YourSceneName", LoadSceneMode.Single));
        ///    }
        ///}
        ///</code>
        ///</example>
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
