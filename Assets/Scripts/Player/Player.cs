using System.Collections;
using System.Collections.Generic;
using Scripts.Core;
using SpawnSystem;
using UnityEngine;

public class Player : MonoBehaviour, IPooledObject, ITakeDMG
{
    [SerializeField] private PlayerMove playerMove;
    [SerializeField] private GameObject invincebleGO;
    private App app;
    public PoolObjectsTag Tag { get; set; }
    public int HP { get; set; }

    public void TakeDMG()
    {
        if (GetComponentInChildren<PlayerInvincible>() == null)
        {
            HP--;
            app.GameManager.OnDestroyPlayer();
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
        playerMove.RotateSpeed = app.GameInitSettings.RotateSpeed;
        playerMove.Speed = app.GameInitSettings.Speed;
        AddInvinceble();
    }


    public void AddInvinceble()
    {
        invincebleGO.SetActive(true);

        var invincible = invincebleGO.GetComponent<PlayerInvincible>();
        invincible.InvincibleTime = app.GameInitSettings.InvincebleTime;
        StartCoroutine(invincible.Countdown());
    }
    public void OnReturnToPool()
    {
        app.ObjectPooler.ReturnToThePool(gameObject);
    }


}
