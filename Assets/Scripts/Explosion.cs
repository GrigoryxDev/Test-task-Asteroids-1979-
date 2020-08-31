using System.Collections;
using System.Collections.Generic;
using Scripts.Core;
using Scripts.Sound;
using SpawnSystem;
using UnityEngine;

public class Explosion : MonoBehaviour, IPooledObject
{
    public PoolObjectsTag Tag { get; set; }
    private Vector3 currentScaleVector;
    private void Update()
    {
        transform.localScale = Vector3.Lerp(currentScaleVector, Vector3.zero, Time.deltaTime);
        transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime);
    }

    public void OnObjectSpawn()
    {
        currentScaleVector = Vector3.one;
        App.Instance.SoundManager.PlaySFX(SoundsEnum.Explosion.ToString());
    }

    public void OnReturnToPool()
    {
        App.Instance.ObjectPooler.ReturnToThePool(gameObject);
    }


}
