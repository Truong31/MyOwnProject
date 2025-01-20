using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimation : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    public Sprite idleSprite;
    private int frame = 0;

    public bool idle = true;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(Animation), 0.25f, 0.25f);
    }

    private void OnEnable()
    {
        this.spriteRenderer.enabled = true;
    }

    private void OnDisable()
    {
        this.spriteRenderer.enabled = false;
    }

    private void Animation()
    {
        frame++;
        if(frame < sprites.Length && frame >= 0)
        {
            spriteRenderer.sprite = sprites[frame];
        }
        if (idle)
        {
            spriteRenderer.sprite = idleSprite;
        }
        else if (frame > sprites.Length)
        {
            frame = 0;
        }
    }
}
