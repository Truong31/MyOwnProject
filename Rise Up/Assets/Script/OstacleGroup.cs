using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OstacleGroup : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D[] rigidbody2Ds;

    private void Start()
    {
        rigidbody2Ds = GetComponentsInChildren<Rigidbody2D>();
        foreach(Rigidbody2D body in rigidbody2Ds)
        {
            body.isKinematic = true;
        }
    }

    private void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
        if (!Utility.IsInScreen(transform.position))
        {
            Destroy(gameObject, 3.0f);
        }
        else
        {
            Falling();
        }
    }

    private void Falling()
    {
        if (gameObject.layer == LayerMask.NameToLayer("Falling"))
        {
            foreach (Rigidbody2D body in rigidbody2Ds)
            {
                body.isKinematic = false;
                body.gravityScale = 0.2f;
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(gameObject.layer == LayerMask.NameToLayer("Non State"))
        {
            foreach (Rigidbody2D body in rigidbody2Ds)
            {
                body.isKinematic = false;

            }
        }
    }

}
