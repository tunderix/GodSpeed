using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ioni.Extensions
{
    /// <summary>
    /// Extension methods for Vectors
    /// </summary>
    public static class VectorExtensions
    {
        /// <summary>
        /// WithX
        /// </summary>
        /// <param name="vector">this</param>
        /// <param name="x">new</param>
        /// <returns>new</returns>
        public static Vector3 WithX(this Vector3 vector, float x) {
            return new Vector3(x, vector.y, vector.z);
        }

        /// <summary>
        /// WithY
        /// </summary>
        /// <param name="vector">this</param>
        /// <param name="y">new</param>
        /// <returns>new</returns>
        public static Vector3 WithY(this Vector3 vector, float y) {
            return new Vector3(vector.x, y, vector.z);
        }
        
        /// <summary>
        /// WithZ
        /// </summary>
        /// <param name="vector">this</param>
        /// <param name="z">new</param>
        /// <returns>new</returns>
        public static Vector3 WithZ(this Vector3 vector, float z) {
            return new Vector3(vector.x, vector.y, z);
        }
        
        /// <summary>
        /// WithZ
        /// </summary>
        /// <param name="vector">this</param>
        /// <param name="z">new</param>
        /// <returns>new</returns>
        public static Vector3 WithZ(this Vector2 vector, float z) {
            return new Vector3(vector.x, vector.y, z);
        }
        
        /// <summary>
        /// WithX
        /// </summary>
        /// <param name="vector">this</param>
        /// <param name="x">new</param>
        /// <returns>new</returns>
        public static Vector2 WithX(this Vector2 vector, float x)
        {
            return new Vector2(x, vector.y);
        }
        
        /// <summary>
        /// AddX
        /// </summary>
        /// <param name="vector">this</param>
        /// <param name="x">float to add</param>
        /// <returns>new</returns>
        public static Vector2 AddX(this Vector2 vector, float x)
        {
            return new Vector2(vector.x + x, vector.y);
        }
        
        /// <summary>
        /// WithY
        /// </summary>
        /// <param name="vector">this</param>
        /// <param name="y">new</param>
        /// <returns>new</returns>
        public static Vector2 WithY(this Vector2 vector, float y)
        {
            return new Vector2(vector.x, y);
        }
        
        /// <summary>
        /// AddY
        /// </summary>
        /// <param name="vector">this</param>
        /// <param name="y">float to add</param>
        /// <returns>new</returns>
        public static Vector2 AddY(this Vector2 vector, float y)
        {
            return new Vector2(vector.x, vector.y + y);
        }
    }
}

