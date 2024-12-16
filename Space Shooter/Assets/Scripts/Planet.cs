using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public System.Action killed;

    public GameObject explosionPrefabs;
    private new CircleCollider2D collider;

    private Vector3 direction;

    private int maxHit;
    public float speed = 20;

    private int size;
    public int maxSize = 5;
    public int minSize = 3;

    private void Awake()
    {
        collider = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        direction = new Vector3(Random.Range(-1f, 1f), -1, 0).normalized;

        size = Random.Range(minSize, maxSize);
        maxHit = size * 2;
        transform.localScale = Vector3.one * size;
        speed /= size;
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
        float radius = collider.radius;

        Vector3 topEdge = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1, 0));
        Vector3 bottomEdge = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0, 0));
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
        if (transform.position.x >= (rightEdge.x - radius - 0.25f) || transform.position.x <= (leftEdge.x + radius + 0.25f))
        {
            direction.x *= -1;
        }
        if(transform.position.y >= (topEdge.y - radius - 0.25f) || transform.position.y <= (bottomEdge.y + radius + 0.25f))
        {
            direction.y *= -1;
        }
    }

    //Kiem tra va cham voi bullet
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player Bullet"))
        {
            maxHit--;
            if (maxHit == 0)
            {
                if (size == 3)
                {
                    GameManager.Instance.AddScore(30);
                }
                else if (size == 4)
                {
                    GameManager.Instance.AddScore(40);
                }
                else if (size == 5)
                {
                    GameManager.Instance.AddScore(50);
                }

                GameObject explosion = Instantiate(explosionPrefabs, transform.position, Quaternion.identity);
                killed.Invoke();
                Destroy(gameObject);
                Destroy(explosion, 1.0f);
            }
        }
    }
}
