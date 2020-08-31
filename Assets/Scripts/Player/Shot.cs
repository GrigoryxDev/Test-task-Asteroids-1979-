using System.Collections;
using Scripts.Core;
using Scripts.Sound;
using SpawnSystem;
using UnityEngine;

public class Shot : MonoBehaviour, IPooledObject, IMovable
{
    [SerializeField] float shotSpeed = 400f;
    private Rigidbody2D m_Rigidbody2d;
    public Rigidbody2D Rigidbody2d { get => m_Rigidbody2d ?? (m_Rigidbody2d = GetComponent<Rigidbody2D>()); }
    public PoolObjectsTag Tag { get; set; }

    private App app;
    public float Speed
    {
        get => shotSpeed;
        set => shotSpeed = value;
    }

    public void Move()
    {
        Rigidbody2d.AddForce(transform.up * Speed);
    }

    public void OnObjectSpawn()
    {
        app = App.Instance;

        app.SoundManager.PlaySFX(SoundsEnum.Laser.ToString());
        Move();
    }

    public void OnReturnToPool()
    {
        app.ObjectPooler.ReturnToThePool(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var damagebleObject = other.GetComponent<ITakeDMG>();
        if (damagebleObject != null)
        {
            damagebleObject.TakeDMG();
        }
    }
}
