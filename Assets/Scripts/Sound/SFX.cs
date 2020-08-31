using System.Collections;
using System.Collections.Generic;
using Scripts.Core;
using SpawnSystem;
using UnityEngine;

public class SFX : MonoBehaviour, IPooledObject
{
    private AudioSource audioSource;
    public PoolObjectsTag Tag { get; set; }

    public void Play(AudioClip clip)
    {
        audioSource.clip = clip;

        audioSource.Play();
        Invoke("OnObjectDestroy", clip.length + 0.1f);
    }

    public void OnObjectSpawn()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnObjectDestroy()
    {
        OnReturnToPool();
    }

    public void OnReturnToPool()
    {
        App.Instance.ObjectPooler.ReturnToThePool(gameObject);
    }


}
