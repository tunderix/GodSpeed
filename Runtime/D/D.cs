using System;
using System.ComponentModel;
using Ioni.Data;
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
        
        
        /// <summary>
        /// Color tags around your string
        /// Private because of circular dependency with Colors from Data. 
        /// </summary>
        /// <param name="text">the string you are extending</param>
        /// <param name="color">Color you want for the text. Go To https://docs.unity3d.com/Packages/com.unity.ugui@1.0/manual/StyledText.html#ColorNames to find possibilities</param>
        /// <returns>Colored text</returns>
        private static string Color(this string text, string color)
        {
            return string.IsNullOrEmpty(text) ?
                "" : $"<color={color}>{text}</color>";
        }
    }
    
    /// <summary>
    /// Debug Levels for D Library
    /// </summary>
    public enum DebugLevel
    {
        Info,
        Err,
        Warn,
        Exception
    }
    public static class DebugLevelExtensions
    {
        /// <summary>
        /// Color for debug level
        /// </summary>
        /// <param name="debugLevel">this</param>
        /// <returns>String Color</returns>
        public static string Color(this DebugLevel debugLevel)
        {
            return debugLevel switch
            {
                DebugLevel.Info => StringColor.Lightblue.HexValue(),
                DebugLevel.Err => StringColor.Red.HexValue(),
                DebugLevel.Warn => StringColor.Yellow.HexValue(),
                DebugLevel.Exception => StringColor.Red.HexValue(),
                _ => StringColor.Lightblue.HexValue(),
            };
        }
        
        /// <summary>
        /// Get hex color value for String Color
        /// First gets the field, then makes sure it has a custom attribute.
        /// Next reads the Description attribute and returns that attribute as string.
        /// Private because of circular dependency with Extensions.  
        /// </summary>
        /// <param name="environment"></param>
        /// <returns>Hex color value</returns>
        private static string HexValue(this StringColor environment) 
        {
            // get the field 
            var field = environment.GetType().GetField(environment.ToString());
            var customAttributes = field.GetCustomAttributes(typeof (DescriptionAttribute), false);

            if(customAttributes.Length > 0 
               && customAttributes[0] is DescriptionAttribute colorDescription)
            {
                return colorDescription.Description;
            }
            else
            {
                return environment.ToString(); 
            }
        }
    }
}