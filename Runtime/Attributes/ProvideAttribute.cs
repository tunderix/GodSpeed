using System;

namespace Ioni.Attributes
{
    /// <summary>
    /// Class <c>ProvideAttribute</c> is an attribute used in DependencyInjection
    /// <example>
    /// For example:
    /// <code>
    /// [Provide]
    /// public ExampleService ProvideExampleService()
    /// {
    ///     return new ExampleService();
    /// };
    /// </code>
    /// </example>
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class ProvideAttribute : Attribute { }
}