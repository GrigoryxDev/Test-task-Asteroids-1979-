using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour, IMovable
{
    [SerializeField] private ParticleSystem particles;
    public float RotateSpeed { get; set; }
    public float Speed { get; set; }

    private Rigidbody2D rigidbody2d;

    public Rigidbody2D Rigidbody2d => rigidbody2d ?? (rigidbody2d = GetComponent<Rigidbody2D>());

    private void FixedUpdate()
    {
        Move();

        if (Rigidbody2d.velocity.sqrMagnitude > 0.1f && !particles.isPlaying)
        {
            particles.Play();
        }
        else if (Rigidbody2d.velocity.sqrMagnitude < 0.1f && particles.isPlaying)
        {
            particles.Stop();
        }
    }

    public void Move()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Rigidbody2d.angularVelocity = RotateSpeed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Rigidbody2d.angularVelocity = -RotateSpeed;
        }
        else
        {
            Rigidbody2d.angularVelocity = 0;
        }

        if (Input.GetKey(KeyCode.W))
        {
            Rigidbody2d.AddForce(transform.up * Speed);
        }
    }
}
