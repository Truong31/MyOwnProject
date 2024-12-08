using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject bombExplosion;
    private int maxHit = 2;
    private new Rigidbody2D rigidbody2D;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Invoke(nameof(BombBehaviour), Random.Range(1.5f, 2f));
    }

    private void BombBehaviour()
    {
        rigidbody2D.isKinematic = true;
        GameObject bomb = Instantiate(bombExplosion, transform.position, Quaternion.identity);
        Destroy(bomb, 1.5f);
        Destroy(gameObject, 1.5f); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("PLayer Bullet"))
        {
            maxHit--;
            if(maxHit <= 0)
            {
                Destroy(gameObject);
            }
        }
    }


}
