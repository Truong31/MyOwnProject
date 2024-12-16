using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public System.Action killed;

    private new Rigidbody2D rigidbody;
    public GameObject explosionPrefabs;

    private Vector3 direction;

    private int maxHit;
    public float speed = 80;

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
        maxHit = size;
        transform.localScale = Vector3.one * size;

        Attack();
    }

    private void Update()
    {
        Vector3 position = Camera.main.WorldToViewportPoint(transform.position);
        if(position.x > 1.01f || position.x < -0.01f || position.y < -0.01f)
        {
            killed.Invoke();
            Destroy(gameObject);
        }
    }

    private void Attack()
    {
        speed /= size;
        rigidbody.AddForce(direction * speed, ForceMode2D.Impulse);
    }

    //Kiem tra va cham voi bullet
    private void OnCollisionEnter2D(Collision2D collision)
    {
        rigidbody.velocity = Vector2.zero;
        rigidbody.AddForce(direction * speed, ForceMode2D.Impulse);

        if (collision.gameObject.layer == LayerMask.NameToLayer("Player Bullet") )
        {
            maxHit--;
            if (maxHit == 0)
            {
                if (size == 2)
                {
                    GameManager.Instance.AddScore(20);
                }
                else if (size == 3)
                {
                    GameManager.Instance.AddScore(30);
                }
                else if(size == 4)
                {
                    GameManager.Instance.AddScore(40);
                }
                
                SoundManager.Instance.EnemyDeathSfx();
                GameObject explosion = Instantiate(explosionPrefabs, transform.position, Quaternion.identity);
                killed.Invoke();
                Destroy(gameObject);
                Destroy(explosion, 1.0f);
            }
        }
    }
}
