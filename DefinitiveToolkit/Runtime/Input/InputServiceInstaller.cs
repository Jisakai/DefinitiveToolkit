using System;
using DTK.Core.Services;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DTK.Input
{
    [CreateAssetMenu(menuName = "DTK/Services/Input Service Installer")]
    public class InputServiceInstaller : ServiceInstaller
    {
        [SerializeField] private InputActionAsset actionAsset;

        public override Type ServiceType => typeof(InputService);

        public override IService CreateService()
        {
            if (actionAsset == null)
            {
                Debug.LogError("[InputServiceInstaller] No InputActionAsset assigned.");
                return null;
            }

            return new InputService(new InputManager(actionAsset));
        }
    }
}