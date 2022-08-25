using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ioni.Extensions
{
    /// <summary>
    /// Extension methods for GameObjects
    /// </summary>
    public static class GameObjectExtensions
    {
        /// <summary>
        /// Tries to find component belonging to GameObject then checks if its found.
        /// If the component is not present, then its added to gameobject.
        /// </summary>
        /// <param name="gameObject">this</param>
        /// <typeparam name="T">The type of the component to be looked at or created</typeparam>
        /// <returns>Component based on the type</returns>
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : MonoBehaviour
        {
            var component = gameObject.GetComponent<T>();
            if (component == null) component = gameObject.AddComponent<T>();
            return component;
        }
        
        /// <summary>
        /// Checks if the gameobject contains a component of wanted type
        /// </summary>
        /// <param name="gameObject">this</param>
        /// <typeparam name="T">Type to be looked at</typeparam>
        /// <returns>True if type was found, False if was not found</returns>
        public static bool HasComponent<T>(this GameObject gameObject) where T : MonoBehaviour
        {
            return gameObject.GetComponent<T>() != null;
        }
    }
}
