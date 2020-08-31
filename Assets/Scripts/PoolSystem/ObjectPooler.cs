using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpawnSystem
{
    public class ObjectPooler : MonoBehaviour
    {
        [SerializeField] private Pool[] pools;
        private Dictionary<PoolObjectsTag, Queue<GameObject>> poolDictionary;

        private void Start()
        {
            poolDictionary = new Dictionary<PoolObjectsTag, Queue<GameObject>>();

            foreach (Pool pool in pools)
            {
                var objectPool = new Queue<GameObject>();

                for (int i = 0; i < pool.initSize; i++)
                {
                    GameObject obj = Instantiate(pool.prefab);
                    obj.transform.SetParent(pool.parentObject);
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);
                }

                poolDictionary.Add(pool.tag, objectPool);
            }
        }

        public void Reset()
        {
            foreach (var poledObject in transform.GetComponentsInChildren<IPooledObject>())
            {
                poledObject.OnReturnToPool();
            }
        }

        public GameObject SpawnFromPool(PoolObjectsTag tag)
        {
            if (!poolDictionary.ContainsKey(tag))
            {
                Debug.LogWarning($"Pool with tag {tag}, doesn't excists");
                return null;
            }

            //If the initialized size of the queue is not enough, creates another object
            var objectToSpawn = poolDictionary[tag].Count == 0 ? CreateInstance(tag) : poolDictionary[tag].Dequeue();

            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = transform.position;
            objectToSpawn.transform.rotation = Quaternion.identity;

            var pooledObj = objectToSpawn.GetComponent<IPooledObject>();

            if (pooledObj != null)
            {
                pooledObj.OnObjectSpawn();
                pooledObj.Tag = tag;
            }

            return objectToSpawn;
        }

        public void ReturnToThePool(GameObject obj)
        {
            var pooledObjectTag = obj.GetComponent<IPooledObject>().Tag;
            obj.SetActive(false);
            poolDictionary[pooledObjectTag].Enqueue(obj);

        }

        private GameObject CreateInstance(PoolObjectsTag tag)
        {
            Pool poolElement = GetPoolElement(tag);
            GameObject prefab = poolElement.prefab;

            if (prefab == null)
            {
                Debug.LogWarning($"Instantiation tag:{tag} error");
                return null;
            }

            GameObject obj = Instantiate(prefab);
            obj.transform.SetParent(poolElement.parentObject);

            return obj;
        }

        private Pool GetPoolElement(PoolObjectsTag tag)
        {
            Pool poolElement = new Pool();

            foreach (Pool pool in pools)
            {
                if (pool.tag == tag)
                {
                    poolElement = pool;
                    break;
                }
            }

            return poolElement;
        }
    }
}