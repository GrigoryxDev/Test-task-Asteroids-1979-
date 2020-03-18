using UnityEngine;

public class UFO : EnemyControllerBase
{
    void Start()
    {
        StartMoving();
    }

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
