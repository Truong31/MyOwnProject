using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private Transform playerPosition;
    private new Rigidbody2D rigidbody2D;
    public GameObject explosionPrefabs;

    private float speed;
    private Vector3 moveDirection = Vector3.left;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        speed = Random.Range(8f, 10f);
        Invoke(nameof(RocketBehaviour), 5.0f);
    }

    private void Update()
    {
        if (!GameManager.Instance.isBossLive)
        {
            Destroy(gameObject);
        }

        if(FindObjectOfType<PLayer>() != null)
        {
            playerPosition = FindObjectOfType<PLayer>().transform;
        }

        transform.position += moveDirection * speed * Time.deltaTime;

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        if (transform.position.x >= (rightEdge.x - 0.5f) || transform.position.x <= (leftEdge.x + 0.5f))
        {
            moveDirection.x *= -1;
        }

    }

    private void RocketBehaviour()
    {
        StartCoroutine(Projectile());
    }

    //Rocket phong theo huong den Player sau 5s
    private IEnumerator Projectile()
    {
        //player = GameManager.Instance.playerPosition;

        moveDirection = Vector3.zero;

        Vector3 direction = (playerPosition.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);

        yield return new WaitForSeconds(1.0f);

        rigidbody2D.AddForce(direction * speed * speed * 0.5f, ForceMode2D.Impulse);

        Destroy(gameObject, 2.0f);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player Bullet"))
        {
            SoundManager.Instance.EnemyDeathSfx();
            Destroy(gameObject);
            GameObject explosion = Instantiate(explosionPrefabs, transform.position, Quaternion.identity);
            Destroy(explosion, 1.0f);
        }
    }
}
