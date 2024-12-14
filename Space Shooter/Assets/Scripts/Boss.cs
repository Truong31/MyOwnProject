using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public System.Action killed;

    public GameObject enemyBullet;
    public HealthBar healthBar;
    public GameObject bossExplosion;

    private int bulletCount = 16;
    private float radius = 1.0f;
    public float speed = 8.0f;
    public int maxHealth = 10;
    public int currentHealth { get; private set; }

    public virtual void Start()
    {
        GameManager.Instance.isBossLive = true;

        healthBar.gameObject.SetActive(true);
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        InvokeRepeating(nameof(Movement), 1.0f, 3.0f);
        InvokeRepeating(nameof(Attack), 3.0f, 3.0f);
    }

    //Tieu huy tat ca Enemy Bullet neu Boss chet
    private void OnDestroy()
    {
        GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy Bullet");
        for(int i = 0; i < enemy.Length; i++)
        {
            Destroy(enemy[i]);
        }
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
        bullet.GetComponent<Rigidbody2D>().velocity = Vector2.down * 5.0f;

        yield return new WaitForSeconds(Random.Range(1, 3));

        Vector3 rodiatingPosition = bullet.transform.position;
        Destroy(bullet.gameObject);

        for (int i = 0; i < bulletCount; i++)
        {

            // Tinh goc cho tung vien dan (theo radian)
            float angle = i * Mathf.PI * 2 / bulletCount;

            // Tinh toan vi tri x và y dua tren ban kinh va goc
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;

            // Xac dinh vi tri vien dan so voi tam
            Vector3 bulletPosition = rodiatingPosition + new Vector3(x, y, 0);

            // Huong cua vien dan (Huong tu tam ra vi tri)
            Vector3 direction = (bulletPosition - rodiatingPosition).normalized;

            // Tao vien dan va thiet lap huong
            GameObject bullets = Instantiate(enemyBullet, bulletPosition, Quaternion.identity);
            bullets.GetComponent<Rigidbody2D>().velocity = direction * speed; // Toc do 5

            Destroy(bullets, 5.0f);

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
                GameManager.Instance.isBossLive = false;
                SoundManager.Instance.BossDeathSfx();
                killed.Invoke();

                healthBar.gameObject.SetActive(false);
                GameObject explosion = Instantiate(bossExplosion, transform.position, Quaternion.identity);
                CancelInvoke();
                Destroy(gameObject);
                Destroy(explosion, 3.0f);
            }
        }
    }
}
