using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private Transform spawnPosition;
    private float scrollSpeed = 3.0f;

    private void Update()
    {
        transform.position += Vector3.down * scrollSpeed * Time.deltaTime;
        if(transform.position.y <= -spawnPosition.position.y)
        {
            if(gameObject.layer == LayerMask.NameToLayer("Main Background"))
            {
                Destroy(gameObject);
            }
            else
            {
                transform.position = spawnPosition.position;
            }
        }
    }
}
