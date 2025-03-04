using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Protector : MonoBehaviour
{
    private bool isDragging = false;        //Kiểm tra chuột có đang nhấn và di chuyển không
    private Vector2 offSet = Vector2.zero;
    private new Rigidbody2D rigidbody2D;

    //Giới hạn màn hình
    private Vector2 maxBound;
    private Vector2 minBound;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.interpolation = RigidbodyInterpolation2D.Interpolate;       //Tạo chuyển động mượt hơn
        rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;       //Tránh xuyên qua các vật thể khác Collider2D

        maxBound = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        minBound = maxBound * -1;

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            offSet = (Vector2)transform.position - mousePosition;
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 newPosition = mousePosition + offSet;

            //Không cho Protector di chuyển ra khỏi giới hạn
            float xBound = Mathf.Clamp(newPosition.x, minBound.x + 0.5f, maxBound.x - 0.5f);
            float yBound = Mathf.Clamp(newPosition.y, minBound.y + 0.5f, maxBound.y - 0.5f);

            rigidbody2D.MovePosition(new Vector2(xBound, yBound));

        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

    }

}
