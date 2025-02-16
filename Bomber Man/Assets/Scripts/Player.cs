using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    public GameManager gameManager;
    public bool inMultiplayer;

    [Header("Movement")]
    public float speed = 5.0f;
    private Vector2 moveDirection;

    [Header("Control")]
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;
    public KeyCode up = KeyCode.W;
    public KeyCode down = KeyCode.S;

    [Header("Animation")]
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

    private void Start()
    {
        GetComponent<CircleCollider2D>().isTrigger = false;
    }

    //NOTE: thiết lập các nút điều khiển di chuyển cho player
    private void Update()
    {
        if (Input.GetKey(left))
        {
            SetDirection(Vector2.left, spriteAnimationLeft);
        }
        else if (Input.GetKey(right))
        {
            SetDirection(Vector2.right, spriteAnimationRight);
        }
        else if (Input.GetKey(up))
        {
            SetDirection(Vector2.up, spriteAnimationUp);
        }
        else if (Input.GetKey(down))
        {
            SetDirection(Vector2.down, spriteAnimationDown);
        }
        else
        {
            SetDirection(Vector2.zero, activeSpriteAnimation);
        }
        
        if (!inMultiplayer)
        {
            if (gameManager.time <= 0)
            {
                StartCoroutine(PlayerDeath());
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = rigidbody2D.position;
        Vector2 direction = moveDirection * speed * Time.fixedDeltaTime;
        this.rigidbody2D.MovePosition(position + direction);
    }

    //NOTE: Thiết lập hướng di chuyển
    private void SetDirection(Vector2 newDirection, SpriteAnimation spriteAnimation)
    {
        this.moveDirection = newDirection;

        this.spriteAnimationLeft.enabled = spriteAnimation == this.spriteAnimationLeft;
        this.spriteAnimationRight.enabled = spriteAnimation == this.spriteAnimationRight;
        this.spriteAnimationUp.enabled = spriteAnimation == this.spriteAnimationUp;
        this.spriteAnimationDown.enabled = spriteAnimation == this.spriteAnimationDown;

        this.activeSpriteAnimation = spriteAnimation;
        this.activeSpriteAnimation.idle = this.moveDirection == Vector2.zero;

    }

    //Xu ly khi nguoi choi va cham voi vu no
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            StartCoroutine(PlayerDeath());
        }
    }

    //Xu ly khi nguoi choi va cham voi ke thu
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            StartCoroutine(PlayerDeath());
        }
    }

    //NOTE: Thiet lap trang thai chet cho player
    private IEnumerator PlayerDeath()
    {
        this.enabled = false;
        GetComponent<BombController>().enabled = false;
        GetComponent<CircleCollider2D>().isTrigger = true;
        SoundManager.instance.PlayerDeath();

        this.spriteAnimationLeft.enabled = false;
        this.spriteAnimationRight.enabled = false;
        this.spriteAnimationUp.enabled = false;
        this.spriteAnimationDown.enabled = false;

        this.spriteAnimationDeath.enabled = true;

        yield return new WaitForSeconds(2.0f);

        SceneManager.LoadScene("MainMenu");
    }
}
