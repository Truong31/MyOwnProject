using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public List<WaveData> waves;
    public Vector3 direction = Vector2.right;
    public AnimationCurve speed;
    public GameObject bulletPrefabs;
    private int currentWaveIndex = 0;

    private float widthWave;
    private float heightWave;
    private int totalEnemies;
    private int enemiesKilled = 0;

    private void Start()
    {
        InvokeRepeating(nameof(Attack), 1.0f, 1.0f);
        StartCoroutine(SpawnWaves());
    }

    private void Update()
    {
        transform.position += direction * 5.0f * Time.deltaTime; Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
        if (direction == Vector3.right && transform.position.x >= (rightEdge.x - widthWave / 2)
            || direction == Vector3.left && transform.position.x <= (leftEdge.x + widthWave / 2))
        {
            direction.x = -direction.x;
        }

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
                SpawnZigZag();
                break;
            case WaveData.Pattern.Line:
                SpawnLine();
                break;
        }
    }

    private void SpawnRectangle(WaveData wave)
    {
        totalEnemies = wave.enemyCount;
        float gap = 2.0f;
        int rows = 4;
        int columns = Mathf.CeilToInt((float)wave.enemyCount / rows);

        float height = gap * (rows - 1);
        float width = gap * (columns - 1);

        widthWave = width;
        heightWave = height;

        float startX = -width / 2;
        float startY = height / 2;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                float x = startX + gap * j;
                float y = startY - gap * i;
                Vector3 position = new Vector3(x, y, 0);
                Enemy enemy = Instantiate(wave.enemy, position + transform.position, Quaternion.identity);
                enemy.transform.SetParent(transform);
                enemy.killed += EnemyKilled;
            }
        }
    }

    private void SpawnCircle(WaveData wave, float radius)
    {
        for(int i = 0; i < wave.enemyCount; i++)
        {
            float angle = (i * Mathf.PI * 2) / wave.enemyCount;

            float x = Mathf.Sin(angle) * radius;
            float y = Mathf.Cos(angle) * radius;

            Vector3 position = new Vector3(x, y, 0);
            Enemy enemy = Instantiate(wave.enemy, position + transform.position, Quaternion.identity);
            enemy.transform.SetParent(transform);
            enemy.killed += EnemyKilled;
        }
    }

    private void SpawnZigZag()
    {

    }

    private void SpawnLine()
    {

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
        foreach(Transform enemies in transform)
        {
            if (Random.value < 0.8f)
            {
                GameObject bullet = Instantiate(bulletPrefabs, enemies.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = Vector2.down * 5.0f;
                Destroy(bullet, 6.0f);
                break;
            }

        }
    }

    

}
