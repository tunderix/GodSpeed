using System.ComponentModel;
using Ioni.Data;
using UnityEngine;
    
namespace Ioni.Extensions
{
    /// <summary>
    /// Common string extensions
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Bold tags around your string
        /// </summary>
        /// <param name="text">the string you are extending</param>
        /// <returns>Bold text</returns>
        public static string B(this string text)
        {
            return string.IsNullOrEmpty(text) ?
                "" : $"<b>{text}</b>";
        }
        
        /// <summary>
        /// Bold tags around your string
        /// </summary>
        /// <param name="text">the string you are extending</param>
        /// <returns>Bold text</returns>
        public static string Bold(this string text)
        {
            return B(text);
        }
        
        /// <summary>
        /// Italic tags around your string
        /// </summary>
        /// <param name="text">the string you are extending</param>
        /// <returns>Italic text</returns>
        public static string I(this string text)
        {
            return string.IsNullOrEmpty(text) ?
                "" : $"<i>{text}</i>";
        }
        
        /// <summary>
        /// Italic tags around your string
        /// </summary>
        /// <param name="text">the string you are extending</param>
        /// <returns>Italic text</returns>
        public static string Italic(this string text)
        {
            return I(text);
        }

        /// <summary>
        /// Color tags around your string
        /// </summary>
        /// <param name="text">the string you are extending</param>
        /// <param name="color">Color you want for the text. Go To https://docs.unity3d.com/Packages/com.unity.ugui@1.0/manual/StyledText.html#ColorNames to find possibilities</param>
        /// <returns>Colored text</returns>
        public static string Color(this string text, string color)
        {
            return string.IsNullOrEmpty(text) ?
                "" : $"<color={color}>{text}</color>";
        }
        
        /// <summary>
        /// Color tags around your string
        /// </summary>
        /// <param name="text">the string you are extending</param>
        /// <param name="color">Color you want for the text</param>
        /// <returns>Colored text</returns>
        public static string Color(this string text, StringColor color)
        {
            return string.IsNullOrEmpty(text) ?
                "" : $"<color={color.HexValue()}>{text}</color>";
        }

        /// <summary>
        /// Size tags around your string
        /// </summary>
        /// <param name="text">the string you are extending</param>
        /// <param name="size">Size you want for the text</param>
        /// <returns>Text with particular size</returns>
        public static string Size(this string text, int size)
        {
            return string.IsNullOrEmpty(text) ?
                "" : $"<size={size}>{text}</size>";
        }
        
        /// <summary>
        /// Get hex color value for String Color
        /// First gets the field, then makes sure it has a custom attribute.
        /// Next reads the Description attribute and returns that attribute as string. 
        /// </summary>
        /// <param name="environment"></param>
        /// <returns>Hex color value</returns>
        public static string HexValue(this StringColor environment) 
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
        
        /// <summary>
        /// Info logging from this string
        /// </summary>
        /// <param name="text">The string you just typed</param>
        public static void Log(this string text)
        {
            D.Info(text);
        }

        /// <summary>
        /// Info logging from this string
        /// </summary>
        /// <param name="text">The string you just typed</param>
        /// <param name="context">Context you are in</param>
        public static void Log(this string text, Object context)
        {
            D.Info(context, text);
        }
        
        /// <summary>
        /// Info logging from this string
        /// </summary>
        /// <param name="text">The string you just typed</param>
        public static void Info(this string text)
        {
            D.Info(text);
        }

        /// <summary>
        /// Info logging from this string
        /// </summary>
        /// <param name="text">The string you just typed</param>
        /// <param name="context">Context you are in</param>
        public static void Info(this string text, Object context)
        {
            D.Info(context, text);
        }
        
        /// <summary>
        /// Error logging from this string
        /// </summary>
        /// <param name="text">The string you just typed</param>
        public static void Err(this string text)
        {
            D.Err(text);
        }
        
        /// <summary>
        /// Error logging from this string
        /// </summary>
        /// <param name="text">The string you just typed</param>
        /// <param name="context">Context you are in</param>
        public static void Err(this string text, Object context)
        {
            D.Err(context, text);
        }
        
        /// <summary>
        /// Warning logging from this string
        /// </summary>
        /// <param name="text">The string you just typed</param>
        public static void Warn(this string text)
        {
            D.Warn(text);
        }
        
        /// <summary>
        /// Warning logging from this string
        /// </summary>
        /// <param name="text">The string you just typed</param>
        /// <param name="context">Context you are in</param>
        public static void Warn(this string text, Object context)
        {
            D.Warn(context, text);
        }
    }
}