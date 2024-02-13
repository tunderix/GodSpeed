namespace Ioni.DesignPatterns.Singleton
{
    /// <summary>
    /// Represents the initialization status of a Singleton.
    /// </summary>
    /// <example>
    /// This sample shows how to use the SingletonInitializationStatus enum.
    /// <code>
    /// class ExampleClass
    /// {
    ///    SingletonInitializationStatus status = SingletonInitializationStatus.None;
    /// }
    /// </code>
    /// </example>
    public enum SingletonInitializationStatus
    {
        /// <summary>
        /// Indicates that the Singleton is not initialized.
        /// </summary>
        None,
        
        /// <summary>
        /// Indicates that the Singleton is in the process of initialization.
        /// </summary>
        Initializing,
        
        /// <summary>
        /// Indicates that the Singleton is successfully initialized.
        /// </summary>
        Initialized
    }
}