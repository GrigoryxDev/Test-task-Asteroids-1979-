using System.Collections;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] private float rotateSpeed = 256f;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float hp = 3f;
    [SerializeField] private float playerDestroyTime = 0.7f;
    [SerializeField] private float playerInvincibleyTime = 2f;
    [SerializeField] private bool isInvincible;
    [SerializeField] private bool isDestroyed;
    private GameObject m_ShootPrefab;
    private Rigidbody2D m_Rigidbody2d;
    private Animator m_Animator;
    private SpriteRenderer m_SpriteRenderer;
    private AudioSource m_AudioSource;


    public GameObject ShootPrefab { get => m_ShootPrefab != null ? m_ShootPrefab : m_ShootPrefab = Resources.Load<GameObject>("ShootPrefab"); }
    public Rigidbody2D Rigidbody2d { get => m_Rigidbody2d != null ? m_Rigidbody2d : m_Rigidbody2d = GetComponent<Rigidbody2D>(); }
    public Animator Animator { get => m_Animator != null ? m_Animator : m_Animator = GetComponent<Animator>(); }
    public SpriteRenderer SpriteRenderer { get => m_SpriteRenderer != null ? m_SpriteRenderer : m_SpriteRenderer = GetComponent<SpriteRenderer>(); }
    public float Hp { get => hp; private set => hp = value; }
    public bool IsInvincible { get => isInvincible; set => isInvincible = value; }
    public AudioSource Audiosource { get => m_AudioSource != null ? m_AudioSource : m_AudioSource = GetComponent<AudioSource>(); }

    private void Start()
    {
        IsInvincible = true;
    }
    private void Update()
    {

        if (IsInvincible)
        {
            StartCoroutine(Invincible(playerInvincibleyTime));
        }
    }

    private void FixedUpdate()
    {
        /// <summary>
        /// Control buttons, for mobile platforms we can implement a joystick 
        /// and tap for attack
        /// </summary>

        if (!isDestroyed)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Instantiate(ShootPrefab, transform.position, transform.rotation);
            }

            if (Input.GetKey(KeyCode.A))
            {
                Rigidbody2d.angularVelocity = rotateSpeed;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                Rigidbody2d.angularVelocity = -rotateSpeed;
            }
            else
            {
                Rigidbody2d.angularVelocity = 0;
            }

            if (Input.GetKey(KeyCode.W))
            {
                Rigidbody2d.AddForce(transform.up * speed);

            }
        }
    }

    /// <summary>
    /// Contact with the enemy, if the player is not destroyed and not invulnerable, 
    /// run the coroutine
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!isDestroyed && Hp > 0 && !IsInvincible)
            {
                StartCoroutine(GetDamage(playerDestroyTime));
            }
        }
    }

    /// <summary>
    /// Coroutine for taking damage. We Play explosion sound, reduce hp,
    /// activate destroyed status for freeze player input,play explosion animation, 
    /// if our hp more than zero, play default animation, 
    /// start coroutine for status of invincble, teleport player to 0.0 coordinates,
    /// and turn off destroyed status
    /// </summary>

    IEnumerator GetDamage(float _time)
    {
        Audiosource.Play();
        Hp--;
        isDestroyed = true;
        Animator.Play("ExplosionAnimation");
        yield return new WaitForSeconds(_time);
        if (Hp > 0)
        {
            Animator.Play("PlayerIdleAnim");
            StartCoroutine(Invincible(playerInvincibleyTime));
            transform.position = new Vector2(0, 0);
            isDestroyed = false;
        }
        else //if our hp equal zero destroy player
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Coroutine of player  invincible.
    /// Change player color and disable invincible after indicated time
    /// </summary>
    IEnumerator Invincible(float _time)
    {
        SpriteRenderer.color = Color.green;
        yield return new WaitForSeconds(_time);
        SpriteRenderer.color = Color.white;
        IsInvincible = false;
    }
}
