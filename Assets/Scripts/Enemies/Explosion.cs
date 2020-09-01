using System.Collections;
using System.Collections.Generic;
using Scripts.Core;
using Scripts.Sound;
using SpawnSystem;
using UnityEngine;

public class Explosion : MonoBehaviour, IPooledObject
{
    public PoolObjectsTag Tag { get; set; }
    private Vector3 scaleChange;
    private void Update()
    {
        if (transform.localScale.x < 0.01f)
        {
            return;
        }

        transform.localScale += scaleChange;
        transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime);
    }

    public void OnObjectSpawn()
    {
        transform.localScale = Vector3.one;
        scaleChange = new Vector3(-0.01f, -0.01f, -0.01f);
        App.Instance.SoundManager.PlaySFX(SoundsEnum.Explosion.ToString());
    }

    public void OnReturnToPool()
    {
        App.Instance.ObjectPooler.ReturnToThePool(gameObject);
    }


}
