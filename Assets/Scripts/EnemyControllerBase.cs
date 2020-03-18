using UnityEngine;

/// <summary>
/// The base class of all enemy objects, with common functionality
/// </summary>
public abstract class EnemyControllerBase : MonoBehaviour
{
    private GameObject m_ExplosionPrefab;
    [SerializeField] private float speed;
    [SerializeField] private float hp;
    [SerializeField] private int enemyScore;
    private GameManager m_GameManager;
    private AudioSource m_AudioSource;

    public GameObject ExplosionPrefab { get => m_ExplosionPrefab != null ? m_ExplosionPrefab : m_ExplosionPrefab = Resources.Load<GameObject>("Explosion"); }
    public float Speed { get => speed; set => speed = value; }
    public float Hp { get => hp; set => hp = value; }
    public GameManager Gamemanager { get => m_GameManager != null ? m_GameManager : m_GameManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameManager>(); }
    public AudioSource Audiosource { get => m_AudioSource != null ? m_AudioSource : m_AudioSource = GetComponent<AudioSource>(); }
    public int EnemyScore { get => enemyScore; set => enemyScore = value; }

    public virtual void Update()
    {
        /// <summary>
        /// moving in front, in world coordinates
        /// </summary>
        transform.Translate(transform.up * Speed * Time.deltaTime, Space.World);
    }


    /// <summary>
    /// Indicate the direction of movement
    /// </summary>
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


    /// <summary>
    /// Called upon destruction. Adds points, destroys itself, 
    /// causes a prefab explosion
    /// </summary>
    public virtual void Die()
    {
        Destroy(gameObject);
        Gamemanager.Score += EnemyScore;
        Instantiate(ExplosionPrefab, transform.position, transform.rotation);
    }
}
