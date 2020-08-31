using System.Collections;
using System.Collections.Generic;
using Scripts.Core;
using SpawnSystem;
using UnityEngine;

public class Player : MonoBehaviour, IPooledObject, ITakeDMG
{
    [SerializeField] private PlayerMove playerMove;
    private App app;
    public PoolObjectsTag Tag { get; set; }
    public int HP { get; set; }

    public void TakeDMG()
    {
        if (GetComponent<PlayerInvincible>() == null)
        {
            HP--;
            app.GameManager.DestroyPlayer();
            OnObjectDestroy();
        }

    }

    public void OnObjectDestroy()
    {
        var explosion = app.ObjectPooler.SpawnFromPool(PoolObjectsTag.Explosion);
        explosion.transform.position = transform.position;

        OnReturnToPool();
    }

    public void OnObjectSpawn()
    {
        app = App.Instance;

        playerMove.gameObject.AddComponent<PlayerInvincible>();
        GetComponent<PlayerInvincible>().invincibleTime = app.GameInitSettings.InvincebleTime;
        playerMove.RotateSpeed = app.GameInitSettings.RotateSpeed;
        playerMove.Speed = app.GameInitSettings.Speed;
    }

    public void OnReturnToPool()
    {
        app.ObjectPooler.ReturnToThePool(gameObject);
    }


}
