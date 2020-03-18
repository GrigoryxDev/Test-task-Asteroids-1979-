using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyControllerBase : MonoBehaviour
{

    private GameObject m_ExplosionPrefab;
    [SerializeField] private float speed;
    [SerializeField] private float hp;
    [SerializeField] private int enemyScore;
    private GameManager m_GameManager;

    public GameObject ExplosionPrefab { get => m_ExplosionPrefab != null ? m_ExplosionPrefab : m_ExplosionPrefab = Resources.Load<GameObject>("Explosion"); }
    public float Speed { get => speed; set => speed = value; }
    public float Hp { get => hp; set => hp = value; }
    public GameManager Gamemanager { get => m_GameManager != null ? m_GameManager : m_GameManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameManager>(); }

    public virtual void Update()
    {
        transform.Translate(transform.up * Speed * Time.deltaTime, Space.World);
    }

    public virtual void StartMoving(float direction = 0.0f)
    {
        if (direction == 0.0f)
        {
            // if no direction choose a random direction to move in.
            direction = Mathf.Floor(UnityEngine.Random.Range(0.0f, 360.0f));
        }
        Vector3 rotation = new Vector3(0.0f, 0.0f, direction);
        transform.rotation = Quaternion.Euler(rotation);
    }

    public virtual void Die()
    {
        Destroy(gameObject);
        Gamemanager.Score += enemyScore;
        Instantiate(ExplosionPrefab, transform.position, transform.rotation);
    }

}
