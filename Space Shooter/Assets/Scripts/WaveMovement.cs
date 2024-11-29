using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMovement : MonoBehaviour
{
    private float zigzagTimer = 3f;
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
            case MovementType.Circle:
                Circle();
                break;
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
        float speedZigZag = 5.0f;
        zigzagTimer += Time.deltaTime * speedZigZag;
        float x = Mathf.Sin(zigzagTimer) * 10; // Bien do dao dong
        transform.Translate(new Vector3(x, -1, 0) * Time.deltaTime);
    }

    //Di chuyen hinh tron
    private void Circle()
    {
        transform.RotateAround(new Vector3(1, 1, 1), new Vector3(0, 0, 1), speed * Time.deltaTime);
    }

}
