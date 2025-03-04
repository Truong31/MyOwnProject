using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.isKinematic = true;
    }

    private void Update()
    {
        if (!Utility.IsInScreen(transform.position))
        {
            Destroy(gameObject, 3.0f);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rigidbody2D.isKinematic = false;
    }

}
