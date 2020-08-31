using System.Collections;
using System.Collections.Generic;
using Scripts.Core;
using Scripts.Sound;
using SpawnSystem;
using UnityEngine;

public class Explosion : MonoBehaviour, IPooledObject
{
    public PoolObjectsTag Tag { get; set; }

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime);
        transform.localScale -= Vector3.one * -0.1f;
    }

    public void OnObjectSpawn()
    {
        App.Instance.SoundManager.PlaySFX(SoundsEnum.Explosion.ToString());
    }

    public void OnReturnToPool()
    {
        App.Instance.ObjectPooler.ReturnToThePool(gameObject);
    }


}
