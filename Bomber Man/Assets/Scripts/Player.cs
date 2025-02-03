using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5.0f;
    private Vector2 moveDirection;
    public Transform startPosition;

    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;
    public KeyCode up = KeyCode.W;
    public KeyCode down = KeyCode.S;

    public SpriteAnimation spriteAnimationLeft;
    public SpriteAnimation spriteAnimationRight;
    public SpriteAnimation spriteAnimationUp;
    public SpriteAnimation spriteAnimationDown;
    public SpriteAnimation spriteAnimationDeath;
    private SpriteAnimation activeSpriteAnimation;

    private new Rigidbody2D rigidbody2D;

    private void Awake()
    {
        this.rigidbody2D = GetComponent<Rigidbody2D>();
        this.activeSpriteAnimation = spriteAnimationDown;
    }

    //NOTE: thiet lap cac nut dieu khien di chuyen cho player
    private void Update()
    {
        if (Input.GetKey(left))
        {
            setDirection(Vector2.left, spriteAnimationLeft);
        }
        else if (Input.GetKey(right))
        {
            setDirection(Vector2.right, spriteAnimationRight);
        }
        else if (Input.GetKey(up))
        {
            setDirection(Vector2.up, spriteAnimationUp);
        }
        else if (Input.GetKey(down))
        {
            setDirection(Vector2.down, spriteAnimationDown);
        }
        else
        {
            setDirection(Vector2.zero, activeSpriteAnimation);
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = rigidbody2D.position;
        Vector2 direction = moveDirection * speed * Time.fixedDeltaTime;
        this.rigidbody2D.MovePosition(position + direction);
    }

    //NOTE: thiet lap huong di chuyen
    private void setDirection(Vector2 newDirection, SpriteAnimation spriteAnimation)
    {
        this.moveDirection = newDirection;

        this.spriteAnimationLeft.enabled = spriteAnimation == this.spriteAnimationLeft;
        this.spriteAnimationRight.enabled = spriteAnimation == this.spriteAnimationRight;
        this.spriteAnimationUp.enabled = spriteAnimation == this.spriteAnimationUp;
        this.spriteAnimationDown.enabled = spriteAnimation == this.spriteAnimationDown;

        this.activeSpriteAnimation = spriteAnimation;
        this.activeSpriteAnimation.idle = this.moveDirection == Vector2.zero;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            StartCoroutine(PlayerDeath());
        }
    }

    //NOTE: Thiet lap trang thai chet cho player
    private IEnumerator PlayerDeath()
    {
        this.enabled = false;
        GetComponent<BombController>().place = KeyCode.A;

        this.spriteAnimationLeft.enabled = false;
        this.spriteAnimationRight.enabled = false;
        this.spriteAnimationUp.enabled = false;
        this.spriteAnimationDown.enabled = false;

        this.spriteAnimationDeath.enabled = true;

        yield return new WaitForSeconds(2.0f);

        this.gameObject.SetActive(false);

        //if(GameManager.instance.time >= 0)
        //{
        //    this.enabled = true;
        //    GetComponent<BombController>().place = KeyCode.Space;
        //    this.gameObject.SetActive(true);
        //    this.transform.position = startPosition.position;
        //    this.spriteAnimationDeath.enabled = false;
        //    this.activeSpriteAnimation = this.spriteAnimationDown;
        //}
    }
}
