using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMovement : MonoBehaviour
{
    private Vector3 direction = Vector3.right;

    private void Update()
    {
        Movement();
    }
    private void Movement()
    {
        transform.position += direction * 5.0f * Time.deltaTime;
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        foreach (Transform enemy in transform)
        {
            if (!enemy.gameObject.activeInHierarchy) // Kiem tra xem doi tuong co dang hoat dong
            {
                continue;
            }
            if (direction == Vector3.right && enemy.position.x >= (rightEdge.x - 1.0f))
            {
                direction.x *= -1.0f;
            }
            else if (direction == Vector3.left && enemy.position.x <= (leftEdge.x + 1.0f))
            {
                direction.x *= -1.0f;
            }
        }
    }

    private void ZigZagMovement(WaveData wave)
    {
        float amplitude = 3f;  // Bien do (Do rong cua ZigZag)
        float frequency = 2f; // Tan so (Toc do qua lai)
        float verticalSpeed = 2f; // Toc do di chuyen doc
        float time = Time.time;

        // Tinh toan vi tri moi
        float x = transform.position.x + Mathf.Sin(time * frequency) * amplitude;
        float y = transform.position.y - verticalSpeed * time;

        // Cap nhat vi tri
        Vector3 position = new Vector3(x, y, 0);

        Enemy enemy = Instantiate(wave.enemy, transform.position + position, Quaternion.identity);

        enemy.transform.SetParent(transform);

        // Xoa Enemy neu ra khoi man hinh
        if (transform.position.y < -Camera.main.orthographicSize)
        {
            Destroy(gameObject);
        }
    }
}
