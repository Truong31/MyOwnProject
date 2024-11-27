using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public List<WaveData> waves;
    public List<Enemy> enemyActive;

    public AnimationCurve speed;
    public GameObject bulletPrefabs;

    private int currentWaveIndex = 0;
    private int totalEnemies;
    private int enemiesKilled = 0;

    private void Start()
    {
        InvokeRepeating(nameof(Attack), 1.0f, 1.0f);
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        while(currentWaveIndex < waves.Count)
        {
            WaveData currentWave = waves[currentWaveIndex];
            SpawnWave(currentWave);

            currentWaveIndex++;
            yield return new WaitUntil(KillAllEnemies);
            enemiesKilled = 0;
            yield return new WaitForSeconds(3.0f);
        }
    }

    private void SpawnWave(WaveData wave)
    {
        switch (wave.pattern)
        {
            case WaveData.Pattern.Circle:
                SpawnCircle(wave, 8.0f);
                break;
            case WaveData.Pattern.Rectangle:
                SpawnRectangle(wave);
                break;
            case WaveData.Pattern.ZigZag:
                SpawnZigZag(wave);
                break;
            case WaveData.Pattern.Line:
                SpawnLine(wave);
                break;
        }
    }

    //Tao Wave hinh Line
    private void SpawnLine(WaveData wave)
    {
        totalEnemies = wave.enemyCount;
        transform.position = wave.spawnPosition.position;

        float gap = 2.0f;
        float startX = gap * (wave.enemyCount - 1)/2;
        
        for(int i = 0; i < wave.enemyCount; i++)
        {
            Vector3 position = new Vector3(startX + gap * i, 0, 0);
            Enemy enemy = Instantiate(wave.enemy, transform.position + position, Quaternion.identity);
            
            enemy.transform.SetParent(transform);
            WaveMovement movement = GetComponent<WaveMovement>();
            movement.Initialize(5.0f, wave.movementType);

            //Vector3 enemyPosition = Camera.main.WorldToViewportPoint(enemy.transform.localPosition);

            //if (!enemy.gameObject.activeInHierarchy) // Kiem tra xem doi tuong co dang hoat dong
            //{
            //    continue;
            //}
            //if (enemyPosition.x > 1 || enemyPosition.x < 0 || enemyPosition.y < 0)
            //{
            //    EnemyKilled();
            //    Destroy(enemy);
            //}

            enemy.killed += EnemyKilled;
        }
        
    }

    //Tao Wave hinh chu nhat
    private void SpawnRectangle(WaveData wave)
    {
        totalEnemies = wave.enemyCount;
        transform.position = wave.spawnPosition.position;

        float gap = 2.0f;
        int rows = 4;
        int columns = Mathf.CeilToInt((float)wave.enemyCount / rows);

        for (int i = 0; i < rows; i++)
        {
            float height = gap * (rows - 1);
            float width = gap * (columns - 1);

            Vector2 centering = new Vector2(-width / 2, height / 2);

            for (int j = 0; j < columns; j++)
            {
                Vector3 position = new Vector3(centering.x + gap * j, centering.y - gap * i, 0);

                Enemy enemy = Instantiate(wave.enemy, position + transform.position, Quaternion.identity);
                enemy.transform.SetParent(transform);
                WaveMovement movement = GetComponent<WaveMovement>();
                movement.Initialize(5.0f, wave.movementType);

                enemy.killed += EnemyKilled;
            }
        }
    }

    //Tao Wave hinh tron
    private void SpawnCircle(WaveData wave, float radius)
    {
        transform.position = wave.spawnPosition.position;
        totalEnemies = wave.enemyCount;

        for(int i = 0; i < wave.enemyCount; i++)
        {
            float angle = (i * Mathf.PI * 2) / wave.enemyCount;

            float x = Mathf.Sin(angle) * radius;
            float y = Mathf.Cos(angle) * radius;

            Vector3 position = new Vector3(x, y, 0);
            Enemy enemy = Instantiate(wave.enemy, position + transform.position, Quaternion.identity);
            WaveMovement movement = GetComponent<WaveMovement>();
            movement.Initialize(5.0f, wave.movementType);
            enemy.transform.SetParent(transform);
            enemy.killed += EnemyKilled;
        }
    }

    //Tao Wave hinh ZigZag
    private void SpawnZigZag(WaveData wave)
    {
        totalEnemies = wave.enemyCount;

        Enemy enemy = Instantiate(wave.enemy, transform.position, Quaternion.identity);

        enemy.transform.SetParent(transform);
        enemy.killed += EnemyKilled;

        // Xoa Enemy neu ra khoi man hinh
        //if (enemy.transform.position.y < -Camera.main.orthographicSize)
        //{
        //    Destroy(enemy);
        //}
    }

    private void EnemyKilled()
    {
        enemiesKilled++;
    }

    private bool KillAllEnemies()
    {
        return enemiesKilled == totalEnemies;
    }

    private void Attack()
    {
        foreach(Transform enemy in transform)
        {
            if (!enemy.gameObject.activeInHierarchy)
            {
                continue;
            }

            if (Random.value < 0.1f)
            {
                GameObject bullet = Instantiate(bulletPrefabs, enemy.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = Vector2.down * 8.0f;
                Destroy(bullet, 6.0f);
                break;
            }

        }
    }

    

}
