using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject enemyBullet;
    public HealthBar healthBar;

    private int bulletCount = 6;
    private float radius = 1.0f;
    public float speed = 2.0f;
    private int maxHealth = 10;
    private int currentHealth;


    private void Start()
    {
        healthBar.gameObject.SetActive(true);
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        InvokeRepeating(nameof(Movement), 5.0f, 5.0f);
        InvokeRepeating(nameof(Attack), 3.0f, 3.0f);
    }

    private void Movement()
    {
        float height = Camera.main.orthographicSize * 2;
        float width = height * Camera.main.aspect;

        Vector3 position = new Vector3(Random.Range(-width/2 + 1.25f, width/2 - 1.25f), Random.Range(0, height / 2 - 1.25f), transform.position.z);
        StartCoroutine(MoveTo(position));
    }

    private IEnumerator MoveTo(Vector3 endPosition)
    {
        Vector3 currentPosition = transform.position;

        float elapsed = 0f;
        float duration = 2.0f;

        while(elapsed < duration)
        {
            float t = elapsed / duration;

            transform.position = Vector3.Lerp(currentPosition, endPosition, t);
            elapsed += Time.deltaTime;

            yield return null;
        }
        transform.position = endPosition;
    }

    private void Attack()
    {
        StartCoroutine(FireBullets());
        
    }

    private IEnumerator FireBullets()
    {
        GameObject bullet = Instantiate(enemyBullet, transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = Vector2.down * 3.0f;

        yield return new WaitForSeconds(Random.Range(1, 3));

        Vector3 rodiatingPosition = bullet.transform.position;
        Destroy(bullet);

        for (int i = 0; i < bulletCount; i++)
        {

            // Tinh goc cho tung vien dan (theo radian)
            float angle = i * Mathf.PI * 2 / bulletCount;

            // Tinh toan vi tri x v� y dua tren ban kinh va goc
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;

            // Xac dinh vi tri vien dan so voi tam
            Vector3 bulletPosition = rodiatingPosition + new Vector3(x, y, 0);

            // Huong cua vien dan (Huong tu tam ra vi tri)
            Vector3 direction = (bulletPosition - rodiatingPosition).normalized;

            // Tao vien dan va thiet lap huong
            GameObject bullets = Instantiate(enemyBullet, bulletPosition, Quaternion.identity);
            bullets.GetComponent<Rigidbody2D>().velocity = direction * 5f; // Toc do 5

            Destroy(bullets, 6.0f);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player Bullet"))
        {
            currentHealth -= 1;
            healthBar.SetHealth(currentHealth);
            if(currentHealth <= 0)
            {
                healthBar.gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
    }
}
