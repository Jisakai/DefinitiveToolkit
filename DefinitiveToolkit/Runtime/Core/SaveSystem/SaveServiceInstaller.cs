using System;
using DTK.Core.Services;
using UnityEngine;

namespace DTK.Core.Save
{
    [CreateAssetMenu(menuName = "DTK/Services/Save Service Installer")]
    public class SaveServiceInstaller : ServiceInstaller
    {
        public override Type ServiceType => typeof(SaveService);

        public override IService CreateService()
        {
            return new SaveService(new SaveManager());
        }
    }
}