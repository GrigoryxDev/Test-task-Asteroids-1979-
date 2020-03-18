using System.Collections;
using UnityEngine;

public class ShotController : MonoBehaviour
{
    [SerializeField] float shotSpeed = 400f;
    [SerializeField] float shotLifeTime = 3f;
    private Rigidbody2D m_Rigidbody2d;
    public Rigidbody2D Rigidbody2d { get => m_Rigidbody2d != null ? m_Rigidbody2d : m_Rigidbody2d = GetComponent<Rigidbody2D>(); }

    
    void Start()
    {
        StartCoroutine(Shoot(shotLifeTime)); 
    }

    /// <summary>
    /// Coroutine of player's shot.
    /// Add force to attack prefab and destroy it after time
    /// </summary>


    IEnumerator Shoot(float _time)
    {
        Rigidbody2d.AddForce(transform.up * shotSpeed);
        yield return new WaitForSeconds(_time);
        Destroy(gameObject);
    }

    
}
