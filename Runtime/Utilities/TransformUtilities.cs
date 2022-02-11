using UnityEngine;

namespace Creator.Utilities
{
    /// <summary>
    /// Should contain common utilities to help with Unity.
    /// </summary>
    public class TransformUtilities : MonoBehaviour
    {
        /// <summary>
        /// Pass a collider and get a random point inside that colliders bounds
        /// </summary>
        /// <param name="collider">Collider whose bounds are used to return random point</param>
        /// <returns>Random point inside bounds</returns>
        public static Vector3 RandomPointInside(Collider collider)
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
        public static Vector3 RandomPointInside(Bounds bounds)
        {
            return new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y),
                Random.Range(bounds.min.z, bounds.max.z)
            );
        }
    }
}