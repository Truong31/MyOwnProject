using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public float speed = 20f;
    public int bulletCount = 1;

    private bool isHit;

    private void Start()
    {
        gameObject.SetActive(true);
    }

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
        BulletTestPooling bullet = ObjectPool.Instance.GetObjectPool();
        if(bullet != null)
        {
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;
            bullet.Projectile(transform.up * 5f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.layer == LayerMask.NameToLayer("Enemy")
            || collision.gameObject.layer == LayerMask.NameToLayer("Asteroid")
            || collision.gameObject.layer == LayerMask.NameToLayer("Enemy Bullet")
            || collision.gameObject.layer == LayerMask.NameToLayer("Boss")
            || collision.gameObject.layer == LayerMask.NameToLayer("Rocket")
            || collision.gameObject.layer == LayerMask.NameToLayer("Bomb")) && !isHit)
        {
            isHit = true;

        }

    }
}
