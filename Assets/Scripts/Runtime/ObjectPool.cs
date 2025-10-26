using System.Collections.Generic;
using UnityEngine;

namespace HandyScripts
{
    public class ObjectPool
    {
        private readonly Transform _parent;
        private readonly Queue<GameObject> _poolQueue;

        private readonly GameObject _prefab;

        public ObjectPool(GameObject prefab, int size, Transform parent = null)
        {
            _prefab = prefab;
            _parent = parent;

            _poolQueue = new Queue<GameObject>();

            for (int i = 0; i < size; i++)
            {
                GameObject obj = CreateNewObject();
                _poolQueue.Enqueue(obj);
            }
        }

        private GameObject CreateNewObject()
        {
            GameObject obj = Object.Instantiate(_prefab, _parent);
            obj.SetActive(false);
            return obj;
        }

        public GameObject Spawn()
        {
            if (_poolQueue.Count > 0)
            {
                GameObject obj = _poolQueue.Dequeue();
                obj.SetActive(true);
                return obj;
            }

            return CreateNewObject();
        }

        public GameObject SpawnInRandomPosition(Bounds bounds)
        {
            GameObject obj = Spawn();
            obj.transform.position = Randomizer.RandomPointInBounds(bounds);
            return obj;
        }

        public void DeSpawn(GameObject obj)
        {
            obj.SetActive(false);
            _poolQueue.Enqueue(obj);
        }
    }
}