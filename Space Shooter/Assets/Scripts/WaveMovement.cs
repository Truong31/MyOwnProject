using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMovement : MonoBehaviour
{
    private Vector3 direction = Vector3.right;
    private float speed;
    private MovementType movementType;

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
            case MovementType.Diagonal:
                Diagonal();
                break;
            case MovementType.ZigZag:
                ZigZag();
                break;

        }
    }
    private void Horizontal()
    {
        transform.position += direction * speed * Time.deltaTime;

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        foreach (Transform enemy in transform)
        {
            if (!enemy.gameObject.activeInHierarchy) // Kiem tra xem doi tuong co dang hoat dong
            {
                continue;
            }
            if (direction == Vector3.right && enemy.position.x >= (rightEdge.x - 1.0f) 
                || direction == Vector3.left && enemy.position.x <= (leftEdge.x + 1.0f))
            {
                direction.x *= -1.0f;
            }
        }
    }

    private void Diagonal()
    {
        Vector3 direction = new Vector3(1, -1, 0);
        transform.position += direction * speed * Time.deltaTime;

        
    }

    private void ZigZag()
    {

    }

    private void Circle()
    {

    }

    private void CheckScreenBounds(Transform enemyPosition)
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(enemyPosition.position);

        // Neu cham bien trai hoac phai, doi huong
        if (screenPoint.x < 0f || screenPoint.x > 1f)
        {
            //direction.x *= -1.0f; ; // Dao huong ZigZag
        }
    }
}
