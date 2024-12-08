using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimation : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    private int frame = 0;
    public float rate = 0.25f;

    private void Awake()
    {
        if(boxCollider == null)
        {
            boxCollider = GetComponent<BoxCollider2D>();
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(Animation), rate, rate);
    }

    private void Animation()
    {
        if(frame >= sprites.Length)
        {
            frame = 0;
        }
        else if(frame >= 0 && frame < sprites.Length)
        {
            spriteRenderer.sprite = sprites[frame];
            frame++;
            UpdateColliderSize();
        }
    }

    private void UpdateColliderSize()
    {
        if(spriteRenderer != null && boxCollider != null)
        {
            boxCollider.size = spriteRenderer.sprite.bounds.size;
        }
    }
}
