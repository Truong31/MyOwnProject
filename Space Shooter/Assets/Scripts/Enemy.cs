using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public System.Action killed;
    public GameObject explosionPrefab;
    public PowerUp[] powerUpPrefabs;

    private int timer;
    private bool isDead = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player Bullet"))
        {
            SoundManager.Instance.EnemyDeathSfx();

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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isDead) return;
        if (collision.gameObject.layer == LayerMask.NameToLayer("Background"))
        {
            isDead = true;
            this.killed.Invoke();
            Destroy(gameObject);
        }
    }

    private void SpawnPower(Transform enemy)
    {
        timer += 1;
        if (timer == Random.Range(10, 15))
        {
            timer = 0;
            int randomPower = Random.Range(0, powerUpPrefabs.Length);
            PowerUp power = Instantiate(powerUpPrefabs[randomPower], enemy.position, Quaternion.identity);
            Destroy(power.gameObject, 5.0f);
        }
    }

}
