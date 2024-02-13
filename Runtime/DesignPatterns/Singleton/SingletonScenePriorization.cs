using System;

namespace Ioni.DesignPatterns.Singleton
{
    /// <summary>
    /// Represents the scene prioritization strategy for a Singleton.
    /// </summary>
    /// <example>
    /// This sample shows how to use the SingletonScenePriorization enum.
    /// <code>
    /// class ExampleClass
    /// {
    ///    SingletonScenePriorization strategy = SingletonScenePriorization.Persistance;
    /// }
    /// </code>
    /// </example>
    [Serializable]
    public enum SingletonScenePriorization
    {
        /// <summary>
        /// Indicates that the Singleton prioritizes persistence across different scenes.
        /// </summary>
        Persistance,
        
        /// <summary>
        /// Indicates that the Singleton prioritizes scene allocation or instantiation.
        /// </summary>
        SceneAllocation,
    }
}