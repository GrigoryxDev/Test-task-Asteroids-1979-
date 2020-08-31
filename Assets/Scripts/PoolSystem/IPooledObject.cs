using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpawnSystem
{
    public interface IPooledObject
    {
        PoolObjectsTag Tag { get; set; }
        
        void OnObjectSpawn();

        void OnReturnToPool();
    }
}