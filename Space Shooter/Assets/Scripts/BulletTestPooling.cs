using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTestPooling : MonoBehaviour
{
    public float speed;
    private new Rigidbody2D rigidbody2D;
    private bool isHit = false;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (OutOfScreen(transform.position) || isHit)
        {
            ObjectPool.Instance.ReturnObjectToPool(this);
        }
    }

    private bool OutOfScreen(Vector2 position)
    {
        Vector3 topEdge = Camera.main.ViewportToWorldPoint(Vector3.up);
        if (position.y > topEdge.y)
        {
            return true;
        }

        return false;
    }

    public void Projectile(Vector2 direction)
    {
        rigidbody2D.AddForce(direction * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isHit = true;
    }
}
