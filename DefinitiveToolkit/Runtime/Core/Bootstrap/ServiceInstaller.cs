using System;
using UnityEngine;

namespace DTK.Core.Services
{
    public abstract class ServiceInstaller : ScriptableObject
    {
        /// <summary>
        /// The interface type this service should be registered under in the ServiceRegistry.
        /// e.g. typeof(IAudioService), not the concrete implementation type.
        /// </summary>
        public abstract Type ServiceType { get; }

        /// <summary>
        /// Produces a live IService instance. For plain C# services this is a direct
        /// "new". For MonoBehaviour-backed services, this should Instantiate a prefab
        /// (and DontDestroyOnLoad it if it needs to survive scene loads).
        /// </summary>
        public abstract IService CreateService();
    }
}
