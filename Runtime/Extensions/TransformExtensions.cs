using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *
 * public class TransformUtilityTests
    {
        private Bounds _bounds;
        
        [SetUp]
        public void Setup()
        {
            _bounds = new Bounds(new Vector3(4, 4, 4), new Vector3(3, 3, 3));
        }
        
        [Test]
        public void RandomPointInsideReturnPointIsInsideBounds()
        {
            // Generate random point inside _bounds. Each of the values should be between 4 +- (3/2)
            Vector3 rndPoint = Vector3.back; //TransformUtilities.RandomPointInside(bounds: _bounds);
            // X is inside offered bounds
            Assert.LessOrEqual(rndPoint.x, 5.5);
            Assert.GreaterOrEqual(rndPoint.x, 2.5);
            // Y is inside offered bounds
            Assert.LessOrEqual(rndPoint.y, 5.5);
            Assert.GreaterOrEqual(rndPoint.y, 2.5);
            // Z is inside offered bounds
            Assert.LessOrEqual(rndPoint.z, 5.5);
            Assert.GreaterOrEqual(rndPoint.z, 2.5);
        }
    }
 */
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
