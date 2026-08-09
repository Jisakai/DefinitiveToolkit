using System;
using DTK.Core.Services;
using UnityEngine;

namespace DTK.UI
{
    [CreateAssetMenu(menuName = "DTK/Services/UI Service Installer")]
    public class UIServiceInstaller : ServiceInstaller
    {
        public override Type ServiceType => typeof(UIService);

        public override IService CreateService()
        {
            return new UIService(new UIManager());
        }
    }
}