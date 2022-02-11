using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Ioni
{
    /// <summary>
    /// D - Logging
    ///
    /// Custom wrapper for default unity debug console. 
    /// </summary>
    public static class D
    {
        
        private static string Color(this string myStr, string color)
        {
            return string.IsNullOrEmpty(myStr) ?
             "" : $"<color={color}>{myStr}</color>";
        }
        
        private static string Message(string prefix, Object obj, DebugLevel dLevel, params object[] message)
        {
            var objName = obj != null 
                ? $"<b>{obj.name.Color(dLevel.Color())}</b>" 
                : String.Empty;
            var prefixes = !String.IsNullOrEmpty(prefix) && obj != null
                ? $"{prefix.Color(dLevel.Color())}"
                : String.Empty;
            var separator = String.IsNullOrEmpty(objName) && String.IsNullOrEmpty(prefixes) ? String.Empty : ": ";
            var messages = string.Join(" | ", message);
            return $"{prefixes}{objName}{separator}{messages}";
        }
        
        /// <summary>
        ///   <para>Logs a message to the Unity Console.</para>
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        public static void Info(params object[] message)
        {
#if UNITY_EDITOR
            Debug.Log(Message("Info - ", null, DebugLevel.Info, message)); 
#endif
        }

        /// <summary>
        ///   <para>Logs a message to the Unity Console.</para>
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="context">Object to which the message applies.</param>
        public static void Info(Object context, params object[] message)
        {
#if UNITY_EDITOR
            Debug.Log(Message("Info - ", context, DebugLevel.Info, message)); 
#endif
        }
        
        /// <summary>
        ///   <para>A variant of Debug.Log that logs an error message to the console.</para>
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        public static void Err(params object[] message)
        {
#if UNITY_EDITOR
            Debug.LogError(Message("Error - ", null, DebugLevel.Err, message)); 
#endif
        }
        
        /// <summary>
        ///   <para>A variant of Debug.Log that logs an error message to the console.</para>
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="context">Object to which the message applies.</param>
        public static void Err(Object context, params object[] message)
        {
#if UNITY_EDITOR
            Debug.LogError(Message("Error - ", context, DebugLevel.Err, message)); 
#endif
        }
        
        /// <summary>
        ///   <para>A variant of Debug.Log that logs a warning message to the console.</para>
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        public static void Warn(params object[] message)
        {
#if UNITY_EDITOR
            Debug.LogWarning(Message("Warn - ", null, DebugLevel.Warn, message)); 
#endif
        }
        
        /// <summary>
        ///   <para>A variant of Debug.Log that logs a warning message to the console.</para>
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="context">Object to which the message applies.</param>
        public static void Warn(Object context, params object[] message)
        {
#if UNITY_EDITOR
            Debug.LogWarning(Message("Warn - ", context, DebugLevel.Warn, message)); 
#endif
        }
        
        /// <summary>
        ///   <para>A variant of Debug.Log that logs an error message to the console.</para>
        /// </summary>
        /// <param name="exception">Runtime Exception.</param>
        public static void Exception(Exception exception) => Debug.LogException(exception);

        /// <summary>
        ///   <para>A variant of Debug.Log that logs an error message to the console.</para>
        /// </summary>
        /// <param name="context">Object to which the message applies.</param>
        /// <param name="exception">Runtime Exception.</param>
        public static void Exception(Exception exception, Object context) => Debug.LogException(exception, context);
    }
    
    public enum DebugLevel
    {
        Info,
        Err,
        Warn,
        Exception
    }
    public static class DebugLevelExtensions
    {
        public static DebugLevel Debug(this DebugLevel debugLevel)
        {
            D.Info("-", debugLevel.ToString());
            return debugLevel;
        }
        public static string Color(this DebugLevel debugLevel)
        {
            return "lightblue";
        }
    }
}