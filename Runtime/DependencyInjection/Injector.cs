using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ioni.DesignPatterns.Singleton;
using Ioni.Utilities;
using UnityEngine;
using Ioni.Attributes;

namespace Ioni.DependencyInjection
{
    ///<summary>
    ///Implements the Singleton pattern and is responsible for dependency injection within the application.
    ///It inherits from a generic MonoSingleton base class.
    ///</summary>
    ///<example>
    ///This is a way to instantiate and use the Injector class.
    ///<code>
    ///class SomeClass : MonoBehaviour
    ///{
    ///    void Start()
    ///    {
    ///        // Access instance of Singleton
    ///        var injector = Injector.Instance;
    ///    }
    ///}
    ///</code>
    ///</example>
    /// <remarks>
    /// <para>Assigns a default execution order to the script to ensure it's one of the first scripts to be executed.</para>
    /// <para>Singleton pattern ensures that only one instance of this class exists, and can be accessed globally.</para>
    /// </remarks>
    [DefaultExecutionOrder(-1000)]
    public class Injector : MonoSingleton<Injector>
    {
        /// <summary>
        /// Defines the flags to search for methods, constructors, fields etc. in the Injector class.
        /// This includes non-public and public instance members.
        /// </summary>
        private const BindingFlags k_bindingFlags =
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        
        /// <summary>
        /// Stores a dictionary of registered dependencies for the Injector.
        /// The key is the Type of the dependency and the value is the instance of the dependency.
        /// </summary>
        readonly Dictionary<Type, object> registry = new ();

        ///<summary>
        ///Called when the script instance is being loaded.
        ///Performs tasks related to dependency registration and injection
        ///</summary>
        ///<remarks>
        ///This method overrides the base class' Awake method.
        ///It first gathers all MonoBehaviour types implementing 'IDependencyProvider' using Linq 'OfType'.
        ///These providers are then registered.
        ///Next, it finds all MonoBehaviour types marked as 'Injectable' and injects the necessary dependencies into them.
        ///</remarks>
        protected override void Awake()
        {
            base.Awake();
            
            var providers = AllMonoBehaviours.OfType<IDependencyProvider>();

            foreach (var provider in providers)
            {
                RegisterProvider(provider);
            }

            var injectables = AllMonoBehaviours.Where(IsInjectable);
            foreach (var injectable in injectables)
            {
                Inject(injectable);
            }
        }
        
        ///<summary>
        ///Represents all MonoBehaviour instances in the current context (Scene or Project).
        ///</summary>
        ///<value>
        ///An array of MonoBehaviour instances sorted by their InstanceID.
        ///</value>
        ///<remarks>
        ///This property uses the 'FindObjectsByType' method to search for all objects of MonoBehaviour type.
        ///The search is sorted by the InstanceID of the objects.
        ///</remarks>
        private MonoBehaviour[] AllMonoBehaviours => FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.InstanceID);

        ///<summary>
        ///Performs dependency injection into the provided instance.
        ///</summary>
        ///<param name="instance">The object instance to inject into.</param>
        ///<remarks>
        ///The 'Inject' method takes an object instance and injects dependencies into fields and methods marked with the 'Inject' attribute.
        ///The dependencies are resolved based on their registered types in the DI container.
        ///An exception is thrown if a dependency can't be resolved.
        ///This method first injects into fields and then into methods.
        ///</remarks>
        private void Inject(object instance)
        {
            var type = instance.GetType();
            var injectableFields = type.GetFields(k_bindingFlags)
                .Where(member => Attribute.IsDefined(member, typeof(InjectAttribute)));

            foreach (var injectableField in injectableFields)
            {
                var fieldType = injectableField.FieldType;
                var resolvedInstance = Resolve(fieldType);
                if (resolvedInstance == null)
                {
                    throw new Exception($"Failed to resolve {fieldType.Name} for {type.Name}");
                }
                
                injectableField.SetValue(instance, resolvedInstance);
                D.Info($"Field injected {fieldType.Name} into {type.Name}");
            }
            
            var injectableMethods = type.GetMethods(k_bindingFlags)
                .Where(member => Attribute.IsDefined(member, typeof(InjectAttribute)));

            foreach (var injectableMethod in injectableMethods)
            {
                var requiredParameters = injectableMethod.GetParameters().Select(p => p.ParameterType).ToArray();
                var resolvedInstances = requiredParameters.Select(Resolve).ToArray();

                if (resolvedInstances.Any(resolvedInstance => resolvedInstance == null))
                {
                    throw new Exception($"Failed to inject {type.Name}.{injectableMethod.Name}");
                }

                injectableMethod.Invoke(instance, resolvedInstances);
                D.Info($"Method injected {type.Name}.{injectableMethod.Name}");
            }
        }

        ///<summary>
        ///Determines whether a MonoBehaviour instance is marked with the 'Inject' attribute.
        ///</summary>
        ///<param name="obj">The MonoBehaviour instance to check.</param>
        ///<returns>true if any member of the MonoBehaviour instance is marked with the 'Inject' attribute; false otherwise.</returns>
        private static bool IsInjectable(MonoBehaviour obj)
        {
            var members = obj.GetType().GetMembers(k_bindingFlags);
            return members.Any(member => Attribute.IsDefined(member, typeof(InjectAttribute)));
        }

        ///<summary>
        ///Resolves a dependency based on its Type.
        ///</summary>
        ///<param name="type">The Type of the dependency to resolve.</param>
        ///<returns>The registered instance for the provided Type or null if the Type is not registered.</returns>
        private object Resolve(Type type)
        {
            registry.TryGetValue(type, out var resolvedInstance);
            return resolvedInstance;
        }

        ///<summary>
        ///Registers the provided instance as a dependency in the Dependency Injection container.
        ///</summary>
        ///<param name="provider">The IDependencyProvider instance which provides the dependencies.</param>
        ///<remarks>
        ///The 'RegisterProvider' method goes through the methods of the provided instance, and for each method that has the 'Provide' attribute, 
        ///it registers the return value of the method as a dependency in the DI container. 
        ///The return type of the method is used as the key in the DI container.
        ///</remarks>
        private void RegisterProvider(IDependencyProvider provider)
        {
            var methods = provider.GetType().GetMethods(k_bindingFlags);

            foreach (var method in methods)
            {
                if (!Attribute.IsDefined(method, typeof(ProvideAttribute))) continue;

                var returnType = method.ReturnType;
                var providedInstance = method.Invoke(provider, null);

                if (providedInstance != null)
                {
                    registry.Add(returnType, providedInstance);
                    D.Info($"Registered {returnType.Name} from {provider.GetType().Name}");
                }
                else
                {
                    throw new Exception($"Provider {provider.GetType().Name} returned null for {returnType.Name}");
                }
            }
        }
    }
}
