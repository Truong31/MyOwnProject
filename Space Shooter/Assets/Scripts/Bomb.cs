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
        Invoke(nameof(BombBehaviour), 5f);
    }

    //Xu ly hanh vi cho Bomb: sau 1 khoang thoi gian se phat no
    private void BombBehaviour()
    {
        if (gameObject.activeInHierarchy)
        {
            rigidbody2D.isKinematic = true;
            GameObject bombExplode = Instantiate(bombExplosion, transform.position, Quaternion.identity);
            Destroy(bombExplode, 1.5f);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player Bullet"))
        {
            maxHit--;
            if(maxHit <= 0)
            {
                Destroy(gameObject);
            }
        }
    }


}