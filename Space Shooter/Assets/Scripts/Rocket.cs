using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private Transform player;
    private new Rigidbody2D rigidbody2D;
    private int speed;
    private int maxHit = 3;
    private float moveDirection;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        RocketMovement();
    }

    private void RocketMovement()
    {
        StartCoroutine(RocketBehaviour());
    }

    private IEnumerator RocketBehaviour()
    {
        speed = Random.Range(8, 10);
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        player = GameManager.Instance.playerPosition;
        if(player.position.x > 0 && transform.position.x < rightEdge.x)
        {
            moveDirection = 1f;
        }
        else if(player.position.x < 0 && transform.position.x > leftEdge.x)
        {
            moveDirection = -1f;
        }

        rigidbody2D.velocity = new Vector2(speed * moveDirection, rigidbody2D.velocity.y);

        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, leftEdge.x + 0.5f, rightEdge.x - 0.5f);
        transform.position = clampedPosition;

        yield return new WaitForSeconds(1.0f);

        Vector3 direction = (player.position - transform.position).normalized;
        rigidbody2D.AddForce(direction * 15f);
        Destroy(gameObject, 3.0f);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player Bullet"))
        {
            maxHit--;
            if(maxHit <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
