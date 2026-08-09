using UnityEngine;
using DTK.Core.Services;

namespace DTK.Core.Pooling
{
    public class PoolService : IService
    {
        private readonly PoolManager _manager;

        public PoolService(PoolManager manager)
        {
            _manager = manager;
        }

        public GameObject Get(GameObject prefab, Vector3 position, Quaternion rotation)
            => _manager.Get(prefab, position, rotation);

        public GameObject Get(GameObject prefab, Vector3 position)
            => _manager.Get(prefab, position, Quaternion.identity);

        public void Prewarm(GameObject prefab, int count) => _manager.Prewarm(prefab, count);

        public void Release(GameObject instance) => _manager.Release(instance);
    }
}