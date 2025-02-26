using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private Sprite[] backgrounds;
    [SerializeField] private float scrollSpeed = 3.0f;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (gameObject.layer != LayerMask.NameToLayer("Main Background"))
        {
            spriteRenderer.sprite = backgrounds[Random.Range(0, backgrounds.Length)];
        }
        
    }

    private void Update()
    {
        transform.position += Vector3.down * scrollSpeed * Time.deltaTime;
        if (transform.position.y <= -45)
        {
            if (gameObject.layer == LayerMask.NameToLayer("Main Background"))
            {
                Destroy(gameObject);
            }
            else
            {
                transform.position = spawnPosition.position;
                spriteRenderer.sprite = backgrounds[Random.Range(0, backgrounds.Length)];
            }
        }
    }
}
