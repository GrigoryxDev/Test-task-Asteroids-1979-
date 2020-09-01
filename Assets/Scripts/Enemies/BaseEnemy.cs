using System.Collections;
using System.Collections.Generic;
using Scripts.Core;
using Scripts.Sound;
using SpawnSystem;
using UnityEngine;

namespace Scripts.Enemies
{

    /// <summary>
    /// Base class of all enemies in this project 
    /// </summary>
    public abstract class BaseEnemy : MonoBehaviour, IMovable, ITakeDMG, IPooledObject, IEnemy
    {
        protected App app;
        public EnemiesType type;
        public float Speed { get; set; }
        public int HP { get; set; }
        public int Score { get; set; }
        public PoolObjectsTag Tag { get; set; }
        public GameObject Enemy => gameObject;

        public virtual void Update()
        {
            Move();
        }

        public virtual void Move()
        {
            transform.Translate(transform.up * Speed * Time.deltaTime, Space.World);
        }

        public virtual void TakeDMG()
        {
            app.SoundManager.PlaySFX(SoundsEnum.Hit.ToString());

            HP--;

            if (HP <= 0)
            {
                OnObjectDestroy();
            }

        }

        public virtual void OnObjectSpawn()
        {
            app = App.Instance;

            var enemy = app.GameInitSettings.GetEnemyData(type);

            Speed = enemy.speed;
            Score = enemy.score;
            HP = enemy.hp;

            transform.localScale = new Vector3(enemy.scale, enemy.scale, enemy.scale);

            StartMoving();
        }

        public virtual void OnObjectDestroy()
        {
            var explosion = app.ObjectPooler.SpawnFromPool(PoolObjectsTag.Explosion);
            explosion.transform.position = transform.position;
            app.GameManager.OnKillEnemy(Score);
            OnReturnToPool();
        }

        public void OnReturnToPool()
        {
            app.ObjectPooler.ReturnToThePool(gameObject);
        }

        /// <summary>
        /// Indicate the direction of movement
        /// </summary>
        public virtual void StartMoving(float direction = 0.0f)
        {
            if (direction == 0.0f)
            {
                // if no direction choose a random direction to move in.
                direction = Mathf.Floor(Random.Range(0.0f, 360.0f));
            }
            Vector3 rotation = new Vector3(0.0f, 0.0f, direction);
            transform.rotation = Quaternion.Euler(rotation);
        }
        /// <summary>
        /// Defines interaction with touching objects.
        /// </summary>
        /// <param name="other"> Collision object</param>
        protected virtual void OnCollisionStay2D(Collision2D other)
        {
            var damagebleObject = other.gameObject.GetComponent<ITakeDMG>();
            if (damagebleObject != null && other.gameObject.CompareTag("Player"))
            {
                damagebleObject.TakeDMG();

                TakeDMG();
            }
        }
    }
}