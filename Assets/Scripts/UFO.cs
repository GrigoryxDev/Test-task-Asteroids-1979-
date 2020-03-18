using UnityEngine;

public class UFO : EnemyControllerBase
{
    void Start()
    {
        StartMoving();
    }

    /// <summary>
    /// Laser contact(player shot)
    /// We take away hp, reproduce the sound of the hit, destroy the laser.
    /// If the object's hp is less than 0, we call the method of death
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
                Die();
            }
        }
    }


}
