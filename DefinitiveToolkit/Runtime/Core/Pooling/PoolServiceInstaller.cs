using System;
using DTK.Core.Services;
using UnityEngine;

namespace DTK.Core.Pooling
{
    [CreateAssetMenu(menuName = "DTK/Services/Pool Service Installer")]
    public class PoolServiceInstaller : ServiceInstaller
    {
        public override Type ServiceType => typeof(PoolService);

        public override IService CreateService()
        {
            return new PoolService(new PoolManager());
        }
    }
}