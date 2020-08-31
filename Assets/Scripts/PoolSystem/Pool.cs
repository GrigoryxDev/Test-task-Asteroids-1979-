using UnityEngine;

namespace SpawnSystem
{
    [System.Serializable]
    public struct Pool
    {
        public Transform parentObject;
        public PoolObjectsTag tag;

        public GameObject prefab;

        public int initSize;

    }
}
