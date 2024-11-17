using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public System.Action killed;
    public GameObject explosionPrefab;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player Bullet"))
        {
            Destroy(collision.gameObject);
            GameManager.Instance.AddScore(10);
            this.killed.Invoke();

            GameObject explode = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(explode, 1.0f);
            Destroy(gameObject);

        }
    }
}
