using System;
using System.Collections.Generic;
using UnityEngine;

namespace DTK.Core.Services
{
    public static class EventBus
    {
        
        private static readonly Dictionary<Type, Delegate> subscribers = new();
        
        public static void Subscribe<T>(Action<T> handler)
        {
            subscribers[typeof(T)] = Delegate.Combine(subscribers.GetValueOrDefault(typeof(T)), handler);
        }
        
        public static void Unsubscribe<T>(Action<T> handler)
        {
            Delegate result = Delegate.Remove(subscribers.GetValueOrDefault(typeof(T)), handler);
    
            if (result == null)
                subscribers.Remove(typeof(T));
            else
                subscribers[typeof(T)] = result;
        }
    
        public static void Publish<T>(T eventData)
        {
            Delegate del = subscribers.GetValueOrDefault(typeof(T));
            if (del == null) return;
    
            foreach (Delegate d in del.GetInvocationList())
            {
                Action<T> action = (Action<T>)d;
                if (d.Target is UnityEngine.Object unityTarget)
                {
                    if (!unityTarget) 
                    { 
                        Unsubscribe<T>(action);
                        continue;
                    }
                }
                try
                {
                    action(eventData);
                }
                catch (Exception e)
                {
                    var context = d.Target as UnityEngine.Object;
                    Debug.LogException(
                        new Exception($"[EventSystem] Handler '{d.Method.Name}' on '{d.Target}' threw an exception during '{typeof(T).Name}'. Unsubscribing handler.", e), 
                        context
                    );
                }
            }
        }
    }
}
