using System;
using System.Collections.Generic;
using UnityEngine;

namespace DTK.Core.Services
{
    public static class ServiceRegistry
    {
        // Holds the references to active game systems
        private static readonly Dictionary<Type, IService> Services = new Dictionary<Type, IService>();
        private static readonly Dictionary<Type, IService> NullServices = new Dictionary<Type, IService>();
        
        public static void Register<T>(T instance) where T : IService
            => RegisterInternal(Services, instance, throwOnDuplicate: false);

        public static void RegisterNull<T>(T instance) where T : IService
            => RegisterInternal(NullServices, instance, throwOnDuplicate: true);

        private static void RegisterInternal<T>(Dictionary<Type, IService> dict, T instance, bool throwOnDuplicate) where T : IService
        {
            if (!dict.TryAdd(typeof(T), instance))
            {
                if (throwOnDuplicate)
                    throw new Exception($"Service: {typeof(T).Name} already registered");

                Debug.LogWarning("Key: " + typeof(T) + " already registered");
                return;
            }

            Debug.Log($"[Registry] Successfully registered: {typeof(T).Name}");
        }
        
        public static void Unregister<T>() where T : IService
        {
            Type type = typeof(T);

            if (Services.Remove(type))
            {
                Debug.Log($"[Registry] Unregistered service: {type.Name}");
            }
            else
            {
                Debug.LogWarning($"[Registry] Attempted to unregister {type.Name}, but it wasn't registered");
            }
        }
        
        public static T Get<T>() where T : IService
        {
            Type type = typeof(T);

            if (Services.TryGetValue(type, out IService service))
            {
                return (T)service;
            }
            
            if (NullServices.TryGetValue(type, out service)) 
            {
                Debug.LogWarning("Service: " + type + " was not registered");
                return (T)service;
            }
            
            throw new Exception($"Null-Object for Service: {type} was not registered");
        }

        public static T Require<T>() where T : IService
        {
            Type type = typeof(T);

            if (Services.TryGetValue(type, out IService service))
            {
                return (T)service;
            }
            else 
            {
                throw new Exception($"Service: {type} was not registered");
            }
        }
    }
}