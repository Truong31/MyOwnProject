using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    //public static WaveManager Instance { get; private set; }

    public List<WaveData> waves;
    public List<Enemy> enemyActive;
    public int waveId = 1;

    public float speed = 6f;
    public GameObject bulletPrefabs;

    private int currentWaveIndex = 0;
    private int totalEnemies;
    private int enemiesKilled = 0;

    //private void Awake()
    //{
    //    if(Instance == null)
    //    {
    //        Instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}

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
            waveId++;
            yield return new WaitForSeconds(3.0f);
        }
    }

    private void SpawnWave(WaveData wave)
    {
        switch (wave.pattern)
        {
            case Pattern.Circle:
                SpawnCircle(wave, 8.0f);
                break;
            case Pattern.Rectangle:
                SpawnRectangle(wave);
                break;
            case Pattern.Line:
                SpawnLine(wave);
                break;
            case Pattern.Asteroid:
                SpawnAsteroid(wave);
                break;
            case Pattern.Planet:
                SpawnAsteroid(wave);
                break;
            case Pattern.Boss:
                SpawnBoss(wave);
                break;
            case Pattern.BigBoss:
                SpawnBigBoss(wave);
                break;
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
                movement.Initialize(speed, wave.movementType);

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
            movement.Initialize(speed, wave.movementType);
            enemy.transform.SetParent(transform);
            enemy.killed += EnemyKilled;
        }
    }

    //Tao Wave dang Line
    private void SpawnLine(WaveData wave)
    {
        totalEnemies = wave.enemyCount;
        transform.position = wave.spawnPosition.position;

        float gap = 2.0f;
        float startX = -gap * (wave.enemyCount - 1) / 2;

        for (int i = 0; i < wave.enemyCount; i++)
        {
            Vector3 position = new Vector3(startX + gap * i, 0, 0);
            Enemy enemy = Instantiate(wave.enemy, transform.position + position, Quaternion.identity);

            enemy.transform.SetParent(transform);
            WaveMovement movement = GetComponent<WaveMovement>();
            movement.Initialize(speed, wave.movementType);

            enemy.killed += EnemyKilled;
        }

    }

    //Tao Wave cac Asteroid hoac cac Planet
    private void SpawnAsteroid(WaveData wave)
    {
        totalEnemies = wave.enemyCount;

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        int count = 0;
        while (wave.enemyCount > count)
        {
            count++;
            transform.position = wave.spawnPosition.position;
            Vector3 position = new Vector3(Random.Range(leftEdge.x + 2.0f, rightEdge.x - 2.0f), transform.position.y, transform.position.z);
            if(wave.pattern == Pattern.Asteroid)
            {
                Asteroid asteroid = Instantiate(wave.asteroid, position, Quaternion.identity);
                asteroid.killed += EnemyKilled;
            }
            else if(wave.pattern == Pattern.Planet)
            {
                int random = Random.Range(0, wave.planet.Length);
                Planet planet = Instantiate(wave.planet[random], position, Quaternion.identity);
                planet.killed += EnemyKilled;
            }
        }
    }

    private void SpawnBoss(WaveData wave)
    {
        totalEnemies = wave.enemyCount;

        int random = Random.Range(0, wave.boss.Length);
        wave.boss[random].gameObject.SetActive(true);

    }

    private void SpawnBigBoss(WaveData wave)
    {
        totalEnemies = wave.enemyCount;
        wave.bigBoss.gameObject.SetActive(true);
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
