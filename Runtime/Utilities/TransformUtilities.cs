using UnityEngine;

namespace Ioni.Utilities
{
    /// <summary>
    /// Should contain common utilities to help with Unity.
    /// </summary>
    public class TransformUtilities : MonoBehaviour
    {
        ///<summary>
        ///Generates a random point inside the specified Collider's bounds.
        ///</summary>
        ///<param name="collider">The Collider within which to generate a random point.</param>
        ///<returns>Returns a Vector3 representing a random point within the provided Collider's bounds.</returns>
        ///<remarks>
        ///This method is useful for generating random positions within a defined space, particularly useful in game development tasks such as spawning enemies or items within a specific area.
        ///</remarks>
        ///<example>
        ///This is an example of how to use the `RandomPointInside` function:
        ///<code>
        ///Vector3 randomPosition = YourClassName.RandomPointInside(colliderInstance);
        ///</code>
        ///</example>
        public static Vector3 RandomPointInside(Collider collider)
        {
            var bounds = collider.bounds;
            return new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y),
                Random.Range(bounds.min.z, bounds.max.z)
            );
        }
        
        ///<summary>
        ///Generates a random point inside the specified Bounds.
        ///</summary>
        ///<param name="bounds">The Bounds within which to generate a random point.</param>
        ///<returns>Returns a Vector3 representing a random point within the provided Bounds.</returns>
        ///<remarks>
        ///This method is useful for generating random positions within a defined space, particularly useful in game development tasks such as spawning enemies or items within a specific area.
        ///</remarks>
        ///<example>
        ///Here is an example of how to use the `RandomPointInside` method:
        ///<code>
        ///Vector3 randomPosition = YourClassName.RandomPointInside(boundsInstance);
        ///</code>
        ///</example>
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