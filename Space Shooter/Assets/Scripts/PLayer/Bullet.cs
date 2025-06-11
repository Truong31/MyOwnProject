using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    private new Rigidbody2D rigidbody2D;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Vector3 topEdge = Camera.main.ViewportToWorldPoint(Vector3.up);
        if (transform.position.y > topEdge.y)
        {
            Destroy(gameObject);
        }
    }

    public void Projectile(Vector2 direction)
    {
        rigidbody2D.AddForce(direction * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
