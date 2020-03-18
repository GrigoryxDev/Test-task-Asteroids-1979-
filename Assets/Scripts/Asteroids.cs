using UnityEngine;

public class Asteroids : EnemyControllerBase
{

    public enum AsteroidsType
    {
        Large,
        Med,
        Small
    }

    public AsteroidsType asteroidsType;

    private GameObject m_AsteroidMed;
    private GameObject m_AsteroidSmall;

    public GameObject AsteroidMed { get => m_AsteroidMed != null ? m_AsteroidMed : m_AsteroidMed = Resources.Load<GameObject>("AsteroidMed"); }
    public GameObject AsteroidSmall { get => m_AsteroidSmall != null ? m_AsteroidSmall : m_AsteroidSmall = Resources.Load<GameObject>("AsteroidSmall"); }

    void Start()
    {

        StartMoving();
    }

    /// <summary>
    ///Laser contact(player shot)
    ///We take away hp, reproduce the sound of the hit, destroy the laser.
    ///If the object's hp is less than 0, we check asteroid type 
    ///and if it large or medium with some chance spawn random amount of little asteroids.
    ///If the asteroid is small or the chance didn’t work, we destroy the asteroid
    /// </summary>

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Shot"))
        {
            Hp--;
            Audiosource.Play();
            Destroy(collision.gameObject);
            if (Hp <= 0)
            {
                var random = Random.Range(0.0f, 1.0f);
                switch (asteroidsType)
                {
                    case AsteroidsType.Large:
                        var chanceMed = 0.7f;
                        if (random < chanceMed)
                        {
                            var childNumber = Random.Range(1, 3);
                            for (int i = 0; i < childNumber; i++)
                            {
                                Instantiate(AsteroidMed, transform.position, transform.rotation);
                            }
                        }
                        break;
                    case AsteroidsType.Med:
                        var chanceSmall = 0.5f;
                        if (random < chanceSmall)
                        {
                            var childNumber = Random.Range(1, 4);
                            for (int i = 0; i < childNumber; i++)
                            {
                                Instantiate(AsteroidSmall, transform.position, transform.rotation);
                            }
                        }
                        break;

                }
                Die();
            }
        }
    }


}
