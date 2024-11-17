using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public Enemy[] enemyPrefabs;
    public Vector3 direction = Vector2.right;
    public AnimationCurve speed;
    public GameObject bulletPrefabs;

    public float gap = 2.0f;
    public int rows = 4;
    public int columns = 10;

    private int totalEnemies => rows * columns;
    private int amountAlive => totalEnemies - enemiesKilled;
    public int enemiesKilled = 0;
    private float height => gap * (rows - 1);
    private float width => gap * (columns - 1);

    private void Awake()
    {
        InsertRectangle();
    }

    private void Start()
    {
        InvokeRepeating(nameof(Attack), 1.0f, 1.0f);
    }

    private void Update()
    {
        transform.position += direction * 5.0f * Time.deltaTime;
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        if (direction == Vector3.right && transform.position.x >= (rightEdge.x - width / 2)
            || direction == Vector3.left && transform.position.x <= (leftEdge.x + width / 2))
        {
            direction.x *= -1;
        }
    }

    public void InsertRectangle()
    {
        float startX = -width / 2;
        float startY = height / 2;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                float x = startX + gap * j;
                float y = startY - gap * i;
                Vector3 position = new Vector3(x, y, 0);
                Enemy enemy = Instantiate(enemyPrefabs[i], position + transform.position, Quaternion.identity);
                enemy.transform.SetParent(transform);
                enemy.killed += EnemyKilled;
            }
        }
    }

    private void EnemyKilled()
    {
        enemiesKilled++;
        //if(enemiesKilled >= totalEnemies)
        //{

        //}
    }

    private void Attack()
    {
        foreach(Transform enemies in transform)
        {
            if (Random.value < (1.0f / (float)this.amountAlive))
            {
                GameObject bullet = Instantiate(bulletPrefabs, enemies.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = Vector2.down * 5.0f;
                break;
            }

        }
    }

}
