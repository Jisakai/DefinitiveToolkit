using System.Collections.Generic;
using UnityEngine;

namespace DTK.Core.Pooling
{
    public class PoolManager
    {
        private readonly Dictionary<GameObject, ObjectPool> _pools = new Dictionary<GameObject, ObjectPool>();
        private readonly Dictionary<GameObject, ObjectPool> _activeInstances = new Dictionary<GameObject, ObjectPool>();
        private readonly Transform _root;

        public PoolManager()
        {
            GameObject rootObject = new GameObject("DTK Pool Root");
            Object.DontDestroyOnLoad(rootObject);
            _root = rootObject.transform;
        }

        public GameObject Get(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            if (!_pools.TryGetValue(prefab, out ObjectPool pool))
            {
                pool = new ObjectPool(prefab, _root);
                _pools[prefab] = pool;
            }

            GameObject instance = pool.Get(position, rotation);
            _activeInstances[instance] = pool;
            return instance;
        }

        public void Prewarm(GameObject prefab, int count)
        {
            if (!_pools.ContainsKey(prefab))
            {
                _pools[prefab] = new ObjectPool(prefab, _root, count);
            }
            else
            {
                Debug.LogWarning($"[PoolManager] Prefab '{prefab.name}' already has a pool; skipping prewarm.");
            }
        }

        public void Release(GameObject instance)
        {
            if (_activeInstances.TryGetValue(instance, out ObjectPool pool))
            {
                pool.Release(instance);
                _activeInstances.Remove(instance);
            }
            else
            {
                Debug.LogWarning($"[PoolManager] Tried to release '{instance.name}', but it wasn't tracked as active.");
            }
        }
    }
}