using System.Collections;
using System.Collections.Generic;
using Scripts.Core;
using SpawnSystem;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private App app;
    [SerializeField] private ParticleSystem shotParticles;

    private void Start()
    {
        app = App.Instance;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            shotParticles.Play();
            var shot = app.ObjectPooler.SpawnFromPool(PoolObjectsTag.Shoot);
            shot.GetComponent<Shot>().MakeShot(transform);

        }
    }
}
