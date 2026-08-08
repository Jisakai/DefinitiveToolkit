using System;
using DTK.Core.Services;
using UnityEngine;

namespace DTK.Core.SceneManagement
{
    [CreateAssetMenu(menuName = "DTK/Services/Scene Service Installer")]
    public class SceneServiceInstaller : ServiceInstaller
    {
        public override Type ServiceType => typeof(SceneService);

        public override IService CreateService()
        {
            return new SceneService(new SceneLoadManager());
        }
    }
}