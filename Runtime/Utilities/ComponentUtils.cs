using UnityEngine;
using Object = UnityEngine.Object;

namespace Ioni.Utilities
{
    /// <summary>
    /// Component utilities
    /// Provides static helpers for components. For example safe and simple way to null check components
    /// </summary>
    public static class ComponentUtils
    {
        /// <summary>
        /// Null check for Unity Object
        /// </summary>
        /// <param name="obj">Object to check</param>
        /// <param name="className">Name of object class</param>
        /// <param name="objectName">Name of the game object</param>
        private static void NotFound(Object obj, string className, string objectName)
        {
            if (obj == null)
            {
                D.Err("No " + className + " found on " + objectName);
            }
        }

        /// <summary>
        /// SafeGetComponent makes a null check for monobehaviour, logs error if not found and 
        /// </summary>
        /// <param name="mono">Monobehaviour to check</param>
        /// <typeparam name="T">The type being looked at</typeparam>
        /// <returns></returns>
        public static T SafeGetComponent<T>(MonoBehaviour mono) where T : Component
        {
            T comp = mono.GetComponent<T>();
            NotFound(comp, typeof(T).Name, mono.name);
            return comp;
        }
        
        /// <summary>
        /// SafeGetComponentInChildren makes a null check for MonoBehaviours as children. Logs Error if not found. 
        /// </summary>
        /// <param name="mono">Monobehaviour with children to check</param>
        /// <typeparam name="T">The type being looked at</typeparam>
        /// <returns></returns>
        public static T SafeGetComponentInChildren<T>(MonoBehaviour mono) where T : Component
        {
            T comp = mono.GetComponentInChildren<T>();
            NotFound(comp, typeof(T).Name, mono.name);
            return comp;
        }
    }
}
