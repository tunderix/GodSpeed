using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ioni.Extensions
{
    /// <summary>
    /// Extension methods for Transforms
    /// </summary>
    public static class TransformExtensions 
    {
        /// <summary>
        /// Destroy all children objects belonging to transforms game object
        /// </summary>
        /// <param name="transform">this</param>
        public static void DestroyChildren(this Transform transform)
        {
            for (var i = transform.childCount - 1; i >= 0; i--)
            {
                Object.Destroy(transform.GetChild(i).gameObject);
            }
        }
        
        /// <summary>
        /// Reset transform to default configurations
        /// </summary>
        /// <param name="transform">this</param>
        public static void ResetTransformation(this Transform transform)
        {
            transform.position = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }
        
        /// <summary>
        /// Pass a collider and get a random point inside that colliders bounds
        /// </summary>
        /// <param name="collider">Collider whose bounds are used to return random point</param>
        /// <returns>Random point inside bounds</returns>
        public static Vector3 RandomPointInside(this Collider collider)
        {
            var bounds = collider.bounds;
            return new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y),
                Random.Range(bounds.min.z, bounds.max.z)
            );
        }
        
        /// <summary>
        /// Pass a collider and get a random point inside bounds
        /// </summary>
        /// <param name="bounds">Bounds for random point</param>
        /// <returns>Random 3D coordinate inside offered bounds</returns>
        public static Vector3 RandomPointInside(this Bounds bounds)
        {
            return new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y),
                Random.Range(bounds.min.z, bounds.max.z)
            );
        }
    }
}
