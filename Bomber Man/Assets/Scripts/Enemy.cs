using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D;
    private float speed = 2.0f;
    private Vector3 enemyDirection;
    private Vector3 nextDirection;
    public LayerMask obstacle;

    [Header("Sprite Animation")]
    public SpriteAnimation spriteAnimationUp;
    public SpriteAnimation spriteAnimationDown;
    public SpriteAnimation spriteAnimationLeft;
    public SpriteAnimation spriteAnimationRight;
    public SpriteAnimation spriteAnimationDeath;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        enemyDirection = SetDirection();
        Animation(enemyDirection);
    }

    private void FixedUpdate()
    {
        Vector2 position = rigidbody2D.position;
        Vector2 moveDirection = enemyDirection * speed * Time.fixedDeltaTime;
        rigidbody2D.MovePosition(position + moveDirection);

        if (HitObstacle(enemyDirection))
        {
            nextDirection = SetDirection();
        }
    }

    //NOTE: set animation dung voi huong di chuyen
    private void Animation(Vector2 direction)
    {
        spriteAnimationDown.enabled = direction == Vector2.down;
        spriteAnimationUp.enabled = direction == Vector2.up;
        spriteAnimationLeft.enabled = direction == Vector2.left;
        spriteAnimationRight.enabled = direction == Vector2.right;

    }

    //NOTE: doi huong khi va cham voi vat the
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(((1 << collision.gameObject.layer) & obstacle) != 0)
        {
            enemyDirection = nextDirection;
            Animation(enemyDirection);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            enemyDirection = Vector2.zero;
            spriteAnimationDown.enabled = false;
            spriteAnimationUp.enabled = false;
            spriteAnimationLeft.enabled = false;
            spriteAnimationRight.enabled = false;
            spriteAnimationDeath.enabled = true;

            Destroy(this.gameObject, 1);
        }
    }

    //NOTE: kiem tra cac huong di khong bi chan, tra ve random trong cac huong do
    private Vector3 SetDirection()
    {
        List<Vector2> availableDirection = new List<Vector2>()
        {
            Vector2.up,
            Vector2.down,
            Vector2.left,
            Vector2.right
        };

        for(int i = availableDirection.Count - 1; i >= 0; i--)
        {
            if (HitObstacle(availableDirection[i]))
            {
                availableDirection.RemoveAt(i);
            }
        }
        return availableDirection[Random.Range(0, availableDirection.Count)];

    }

    //NOTE: kiem tra huong di tiep theo co bi chan khong
    private bool HitObstacle(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1, obstacle);
        return (hit.collider != null);
    }

}
