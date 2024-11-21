using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public System.Action killed;
    public GameObject explosionPrefab;
    public PowerUp[] powerUpPrefabs;

    private bool isDead = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player Bullet"))
        {
            isDead = true;
            Destroy(collision.gameObject);
            GameManager.Instance.AddScore(10);
            this.killed.Invoke();
            SpawnPower(transform);

            GameObject explode = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(explode, 1.0f);
            Destroy(gameObject);

        }
    }

    private void SpawnPower(Transform enemy)
    {
        if (Random.value < 0.1f)
        {
            int randomPower = Random.Range(0, powerUpPrefabs.Length);
            PowerUp power = Instantiate(powerUpPrefabs[randomPower], enemy.position, Quaternion.identity);
            Destroy(power, 6.0f);
        }
    }

}
