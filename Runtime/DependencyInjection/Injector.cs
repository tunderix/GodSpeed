using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ioni.DP.Singleton;
using Ioni.Utilities;
using UnityEngine;
using Ioni.Attributes;

namespace Ioni.DependencyInjection
{
    [DefaultExecutionOrder(-1000)]
    public class Injector : MSingleton<Injector>
    {
        private const BindingFlags k_bindingFlags =
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        
        readonly Dictionary<Type, object> registry = new ();

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
        
        private MonoBehaviour[] AllMonoBehaviours => FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.InstanceID);

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

        private static bool IsInjectable(MonoBehaviour obj)
        {
            var members = obj.GetType().GetMembers(k_bindingFlags);
            return members.Any(member => Attribute.IsDefined(member, typeof(InjectAttribute)));
        }

        private object Resolve(Type type)
        {
            registry.TryGetValue(type, out var resolvedInstance);
            return resolvedInstance;
        }

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
