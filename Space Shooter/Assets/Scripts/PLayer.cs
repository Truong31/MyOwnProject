using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayer : MonoBehaviour
{
    public float speed = 20f;
    public int bulletCount = 1;

    public Bullet bulletPrefabs;
    public GameObject shield;
    public GameObject explodePrefabs;

    void Update()
    {
        if (isInScreen())
        {
            // Lấy vị trí của con trỏ chuột và chuyển đổi sang tọa độ thế giới
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Đặt z của máy bay là 0 để tránh di chuyển ra ngoài trục z
            mousePosition.z = 0f;

            // Di chuyển máy bay về phía vị trí chuột với tốc độ đã định
            transform.position = Vector3.Lerp(transform.position, mousePosition, speed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    //Ngan khong cho Player di ra ngoai man hinh
    private bool isInScreen()
    {
        Vector3 mousePosition = Input.mousePosition;
        return mousePosition.x > 0 && mousePosition.x <= Screen.width
                && mousePosition.y > 0 && mousePosition.y <= Screen.height;
    }

    //Tạo khả năng bắn đạn cho người chơi. Mỗi khi ăn 1 Power thì sẽ tăng số viên đạn bắn ra
    private void Shoot()
    {
        float spacing = 0.4f;

        /*Tính toán vị trí viên đạn ở ngoài cùng. Trong đó:
         *      + (Player BulletCount - 1) * spacing: tính tổng khoảng cách giữa các viên đạn
         *      + /2 để lấy vị trí ở giữa
         *      + Dấu "-" để lấy giá trị ngoài cùng
         *Các viên đạn tiếp theo sẽ được sinh ra với gốc là viên đạn đầu tiên
         */
        if(bulletCount > 5)
        {
            bulletCount = 5;
        }
        float startX = -((bulletCount - 1) * spacing) / 2;
        for (int i = 0; i < bulletCount; i++)
        {
            Vector3 position = new Vector3(startX + spacing * i, 0, 0);

            Bullet bullet = Instantiate(bulletPrefabs, position + transform.position, transform.rotation);
            bullet.Projectile(transform.up * 5f);
        }
    }

    private void Killed()
    {
        GameObject explode = Instantiate(explodePrefabs, transform.position, Quaternion.identity);
        Destroy(explode, 1.0f);

        gameObject.SetActive(false);

        Invoke(nameof(ActiveShieldPower), 1.5f);
    }

    public void ActiveShieldPower()
    {
        gameObject.SetActive(true);
        StartCoroutine(ShieldPower());
    }

    private IEnumerator ShieldPower()
    {
        gameObject.layer = LayerMask.NameToLayer("Shield");
        shield.SetActive(true);

        yield return new WaitForSeconds(5.0f);

        shield.SetActive(false);
        gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy") 
            || collision.gameObject.layer == LayerMask.NameToLayer("Asteroid")
            || collision.gameObject.layer == LayerMask.NameToLayer("Enemy Bullet")
            || collision.gameObject.layer == LayerMask.NameToLayer("Boss"))
        {
            Killed();
            
        }

    }

}
