using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Reflection;
using Ioni.Attributes;
using UnityEngine;
    
namespace Ioni.Extensions
{
    /// <summary>
    /// Common MonoBehaviour extensions
    /// </summary>
    public static class MonoBehaviourExtensions
    {
        /// <summary>
        /// SafeGetComponent makes a null check for monobehaviour, logs error if not found and 
        /// </summary>
        /// <param name="mono">Monobehaviour to check</param>
        /// <typeparam name="T">The type being looked at</typeparam>
        /// <returns></returns>
        public static T SafeGetComponent<T>(this MonoBehaviour mono) where T : Component
        {
            T comp = mono.GetComponent<T>();
            if (comp == null)
            {
                D.Err("No " + typeof(T).Name + " found on " + mono.name);
            }
            return comp;
        }
        
        /// <summary>
        /// SafeGetComponentInChildren makes a null check for MonoBehaviours as children. Logs Error if not found. 
        /// </summary>
        /// <param name="mono">Monobehaviour with children to check</param>
        /// <typeparam name="T">The type being looked at</typeparam>
        /// <returns></returns>
        public static T SafeGetComponentInChildren<T>(this MonoBehaviour mono) where T : Component
        {
            T comp = mono.GetComponentInChildren<T>();
            if (comp == null)
            {
                D.Err("No " + typeof(T).Name + " found on " + mono.name);
            }
            return comp;
        }
        
        private static bool IsInjectable(this MonoBehaviour mono)
        {
            var members = mono.GetType()
                .GetMembers(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            return members.Any(member => Attribute.IsDefined(member, typeof(InjectAttribute)));
        }
    }
}