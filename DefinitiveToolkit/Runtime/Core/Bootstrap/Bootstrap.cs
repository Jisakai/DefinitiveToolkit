using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace DTK.Core.Services
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private List<ServiceInstaller> installers;

        private void Awake()
        {
            foreach (ServiceInstaller installer in installers)
            {
                if (installer == null)
                {
                    Debug.LogWarning("[Bootstrap] Skipped a null installer entry");
                    continue;
                }

                IService service = installer.CreateService();
                RegisterService(installer.ServiceType, service);
            }
        }

        private void RegisterService(System.Type serviceType, IService service)
        {
            MethodInfo registerMethod = typeof(ServiceRegistry)
                .GetMethod(nameof(ServiceRegistry.Register))
                .MakeGenericMethod(serviceType);

            registerMethod.Invoke(null, new object[] { service });

            Debug.Log($"[Bootstrap] Wired up service: {serviceType.Name}");
        }
    }
}
