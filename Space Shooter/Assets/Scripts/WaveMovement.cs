using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WaveMovement : MonoBehaviour
{
    private Vector3 horizontalDirection = Vector3.right;
    private Vector3 diagonalDirection;
    private float speed;
    private MovementType movementType;

    private void Awake()
    {
        diagonalDirection = new Vector3(Random.Range(-2f, 2f), -1, 0);
    }


    //Khoi tao
    public void Initialize(float speed, MovementType movementType)
    {
        this.speed = speed;
        this.movementType = movementType;
    }

    private void Update()
    {
        switch (movementType)
        {
            case MovementType.Horizontal:
                Horizontal();
                break;
            case MovementType.BouncePatrol:
                BouncePatrol();
                break;
            case MovementType.ZigZag:
                ZigZag();
                break;
            case MovementType.Default:
                break;

        }
    }

    //Di chuyen ngang
    private void Horizontal()
    {
        transform.position += horizontalDirection * speed * Time.deltaTime;

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        foreach (Transform enemy in transform)
        {
            if (!enemy.gameObject.activeInHierarchy) // Kiem tra xem doi tuong co dang hoat dong
            {
                continue;
            }
            if (horizontalDirection == Vector3.right && enemy.position.x >= (rightEdge.x - 1.0f) 
                || horizontalDirection == Vector3.left && enemy.position.x <= (leftEdge.x + 1.0f))
            {
                horizontalDirection.x *= -1.0f;
            }
        }
    }

    //Di chuyen kieu bat lai khi dap vao thanh
    private void BouncePatrol()
    {
        transform.position += diagonalDirection * speed * Time.deltaTime;

        Vector3 topEdge = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1, 0));
        Vector3 bottomEdge = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0, 0));
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
        foreach (Transform enemy in transform)
        {
            if (!enemy.gameObject.activeInHierarchy) // Kiem tra xem doi tuong co dang hoat dong
            {
                continue;
            }
            if (enemy.position.y >= (topEdge.y - 0.5f) || enemy.position.y <= (bottomEdge.y + 0.5f))
            {
                diagonalDirection.y *= -1;
                break;
            }
            if (enemy.position.x >= (rightEdge.x - 0.5f) || enemy.position.x <= (leftEdge.x + 0.5f))
            {
                diagonalDirection.x *= -1;
                break;
            }
        }
    }

    //Di chuyen hinh ZigZag
    private void ZigZag()
    {
        float amplitude = 10.0f; // Biên độ dao động
        float frequency = 2.0f; // Tần số dao động
        float verticalSpeed = 3.0f; // Tốc độ dọc

        float x = Mathf.Sin(Time.time * frequency) * amplitude; // Tính dao động ngang
        float y = transform.position.y - verticalSpeed * Time.deltaTime; // Di chuyển thẳng xuống

        transform.position = new Vector3(x, y, transform.position.z); // Cập nhật vị trí
    }

}
