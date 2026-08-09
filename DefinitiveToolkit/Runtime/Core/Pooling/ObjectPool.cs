using System.Collections.Generic;
using UnityEngine;

namespace DTK.Core.Pooling
{
    public class ObjectPool
    {
        private readonly GameObject _prefab;
        private readonly Transform _parent;
        private readonly Stack<GameObject> _inactive = new Stack<GameObject>();

        public ObjectPool(GameObject prefab, Transform parent, int prewarmCount = 0)
        {
            _prefab = prefab;
            _parent = parent;

            for (int i = 0; i < prewarmCount; i++)
            {
                GameObject instance = CreateNew();
                instance.SetActive(false);
                _inactive.Push(instance);
            }
        }

        private GameObject CreateNew()
        {
            return Object.Instantiate(_prefab, _parent);
        }

        public GameObject Get(Vector3 position, Quaternion rotation)
        {
            GameObject instance = _inactive.Count > 0 ? _inactive.Pop() : CreateNew();

            instance.transform.SetPositionAndRotation(position, rotation);
            instance.SetActive(true);

            if (instance.TryGetComponent(out IPoolable poolable))
                poolable.OnSpawn();

            return instance;
        }

        public void Release(GameObject instance)
        {
            if (instance.TryGetComponent(out IPoolable poolable))
                poolable.OnDespawn();

            instance.SetActive(false);
            _inactive.Push(instance);
        }
    }
}