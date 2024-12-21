using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public System.Action killed;
    public GameObject explosionPrefab;
    public PowerUp[] powerUpPrefabs;

    private int maxHit = 1;
    private bool isDead = false;

    [Range(0.03f, 0.06f)]
    private float itemSpawnChance = 0.04f;

    private void Start()
    {
        IncreaseMaxHit();
    }

    private void Update()
    {
        DestroyEnemy();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player Bullet"))
        {
            maxHit--;
            if(maxHit <= 0)
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
    }

    //Enemy ra ngoai man hinh se bi Destroy
    private void DestroyEnemy()
    {
        if (isDead) return;
        Vector3 enemyPosition = Camera.main.WorldToViewportPoint(transform.position);
        if(enemyPosition.y < -0.01)
        {
            isDead = true;
            this.killed.Invoke();
            Destroy(gameObject);
        }
    }

    //Tang so vien dan chiu duoc o cac Wave sau
    private void IncreaseMaxHit()
    {
        if(GameManager.Instance.waveID > 11 && GameManager.Instance.waveID < 17)
        {
            maxHit = 2;
        }
        else if(GameManager.Instance.waveID > 17 && GameManager.Instance.waveID < 23)
        {
            maxHit = 3;
        }
        else if (GameManager.Instance.waveID > 23 && GameManager.Instance.waveID < 30)
        {
            maxHit = 4;
        }
    }

    //Xuat hien cac PowerUp
    private void SpawnPower(Transform enemy)
    {
        if (Random.Range(0.0f, 1f) < itemSpawnChance)
        {
            int randomPower = Random.Range(0, powerUpPrefabs.Length);
            PowerUp power = Instantiate(powerUpPrefabs[randomPower], enemy.position, Quaternion.identity);
            Destroy(power.gameObject, 5.0f);
        }
    }

}
