using System;
using UnityEngine;

namespace Ioni.Attributes
{
    /// <summary>
    /// Class <c>InjectAttribute</c> is an attribute used in DependencyInjection
    /// <example>
    /// For example:
    /// <code>
    /// [Inject]
    /// public void Init(ExampleService exampleService)
    /// {
    ///     this._exampleService = exampleService;
    /// };
    /// </code>
    /// </example>
    /// </summary>
    
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Method)]
    public sealed class InjectAttribute : Attribute { }
}
