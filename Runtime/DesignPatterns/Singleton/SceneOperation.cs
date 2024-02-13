using UnityEngine;

namespace Ioni.DesignPatterns.Singleton
{
    /// <summary>
    /// Represents scene operation including the scene index and its associated async operation.
    /// </summary>
    public struct SceneOperation
    {
        /// <summary>
        /// Gets or sets the build index of the scene this operation is associated with.
        /// </summary>
        /// <returns>The build index of the scene.</returns>
        public int SceneIndex;

        /// <summary>
        /// Gets or sets the Asynchronous operation associated with the scene.
        /// </summary>
        /// <returns>The Asynchronous operation associated with the scene.</returns>
        public AsyncOperation Operation;

        /// <summary>
        /// Construct a new SceneOperation.
        /// </summary>
        /// <param name="sceneBuildIndex">The build index of the scene this operation is associated with.</param>
        /// <param name="operation">The Asynchronous operation of the scene.</param>
        public SceneOperation(int sceneBuildIndex, AsyncOperation operation)
        {
            SceneIndex = sceneBuildIndex;
            Operation = operation;
        }
    }
}