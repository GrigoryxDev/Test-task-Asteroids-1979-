using System.Collections;
using Scripts.Core;
using Scripts.Sound;
using SpawnSystem;
using UnityEngine;

namespace Scripts.PlayerSystem
{
    public class Shot : MonoBehaviour, IPooledObject
    {
        [SerializeField] float shotSpeed = 400f;
        private Rigidbody2D m_Rigidbody2d;
        public Rigidbody2D Rigidbody2d { get => m_Rigidbody2d ?? (m_Rigidbody2d = GetComponent<Rigidbody2D>()); }
        public PoolObjectsTag Tag { get; set; }
        private App app;
        public void MakeShot(Transform playerTransform)
        {
            app.SoundManager.PlaySFX(SoundsEnum.Laser.ToString());

            transform.rotation = playerTransform.rotation;
            transform.position = playerTransform.position;
            Rigidbody2d.AddForce(playerTransform.up * shotSpeed);

        }

        public void OnObjectSpawn()
        {
            app = App.Instance;
        }

        public void OnReturnToPool()
        {
            app.ObjectPooler.ReturnToThePool(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var damagebleObject = other.GetComponent<IEnemy>();
            if (damagebleObject != null)
            {
                damagebleObject.Enemy.GetComponent<ITakeDMG>().TakeDMG();
                OnReturnToPool();
            }
        }
    }
}