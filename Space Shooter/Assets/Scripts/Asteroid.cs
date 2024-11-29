using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public System.Action killed;

    private new Rigidbody2D rigidbody;
    private Vector3 direction;
    private Vector3 position;
    public GameObject explosionPrefabs;

    private int maxHit;
    public float speed = 40;

    private int size;
    public int maxSize = 4;
    public int minSize = 2;

    private void Awake()
    { 
        rigidbody = GetComponent<Rigidbody2D>(); 
    }

    private void Start()
    {
        direction = new Vector3(Random.Range(-1f, 1f), -1, 0).normalized;

        size = Random.Range(minSize, maxSize);
        rigidbody.mass = size;
        maxHit = size*2;
        transform.localScale = Vector3.one * size;

        Projectile();
    }

    private void Update()
    {
        position = Camera.main.WorldToViewportPoint(transform.position);
        if(position.x > 1.5f || position.x < -0.5f || position.y > 1.5f || position.y < -0.5f)
        {
            killed.Invoke();
            Destroy(gameObject);
        }
    }

    private void Projectile()
    {
        speed /= size;
        rigidbody.AddForce(direction * speed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rigidbody.velocity = Vector2.zero;
        rigidbody.AddForce(direction * speed, ForceMode2D.Impulse);

        if (collision.gameObject.layer == LayerMask.NameToLayer("Player Bullet") )
        {
            maxHit--;
            if(maxHit == 0)
            {
                GameObject explosion = Instantiate(explosionPrefabs, transform.position, Quaternion.identity);
                killed.Invoke();
                Destroy(gameObject);
                Destroy(explosion, 1.0f);
            }
        }
    }
}
