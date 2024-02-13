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
        ///<summary>
        ///Logs an error message if a required object is not found on a specific object.
        ///</summary>
        ///<param name="obj">The Object to check for null-ness.</param>
        ///<param name="className">The class name of the expected object.</param>
        ///<param name="objectName">The name of the specific object where the required object should exist.</param>
        ///<remarks>
        ///This method is useful for debugging and ensuring the necessary components exist where expected.
        ///</remarks>
        ///<example>
        ///This is an example of how to use the `NotFound` function:
        ///<code>
        ///YourClassName.NotFound(requiredComponent, "ExpectedClassName", "SpecificObjectName");
        ///</code>
        ///</example>
        private static void NotFound(Object obj, string className, string objectName)
        {
            if (obj == null)
            {
                D.Err("No " + className + " found on " + objectName);
            }
        }

        ///<summary>
        ///Attempts to obtain a component of type T from the provided MonoBehaviour and logs an error if it's not found.
        ///</summary>
        ///<typeparam name="T">The type of component to retrieve from the MonoBehaviour. This must be a type that derives from Component.</typeparam>
        ///<param name="mono">The MonoBehaviour from which to retrieve the component.</param>
        ///<returns>Returns the component of type T, or null if it doesn't exist.</returns>
        ///<remarks>
        ///This method is useful for safely accessing components on a MonoBehaviour and ensuring they exist. It will also log an error message if the component is not found, which is useful for debugging.
        ///</remarks>
        ///<example>
        ///Here is an example of how to use the `SafeGetComponent` method:
        ///<code>
        /// YourComponentType component = YourClassName.SafeGetComponent<YourComponentType>(monoBehaviourInstance);
        ///</code>
        ///</example>
        public static T SafeGetComponent<T>(MonoBehaviour mono) where T : Component
        {
            T comp = mono.GetComponent<T>();
            NotFound(comp, typeof(T).Name, mono.name);
            return comp;
        }
        
        ///<summary>
        ///Attempts to obtain a component of type T in the children of the provided MonoBehaviour, and logs an error if it isn't found.
        ///</summary>
        ///<typeparam name="T">The type of component to retrieve. This must be a type that derives from Component.</typeparam>
        ///<param name="mono">The MonoBehaviour in whose children the component will be searched for.</param>
        ///<returns>Returns the component of type T from the children, or null if it doesn't exist.</returns>
        ///<remarks>
        ///This method helps with safely accessing components in a MonoBehaviour's children and makes sure they exist. Additionally, an error message will be logged if the component does not exist, aiding in debugging.
        ///</remarks>
        ///<example>
        ///Here is an example of how to use the `SafeGetComponentInChildren` method:
        ///<code>
        /// YourComponentType component = YourClassName.SafeGetComponentInChildren<YourComponentType>(monoBehaviourInstance);
        ///</code>
        ///</example>
        public static T SafeGetComponentInChildren<T>(MonoBehaviour mono) where T : Component
        {
            T comp = mono.GetComponentInChildren<T>();
            NotFound(comp, typeof(T).Name, mono.name);
            return comp;
        }
    }
}
