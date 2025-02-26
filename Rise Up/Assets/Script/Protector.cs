using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Protector : MonoBehaviour
{
    private bool isDragging = false;        //Kiem tra xem chuot co dang nhan va di chuyen khong
    private Vector2 offSet = Vector2.zero;

    //Gioi han cua man hinh
    private Vector2 maxBound;
    private Vector2 minBound;

    private void Start()
    {
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

            //Khong cho Protector di chuyen ra khoi gioi han
            float xBound = Mathf.Clamp(newPosition.x, minBound.x + 0.5f, maxBound.x - 0.5f);
            float yBound = Mathf.Clamp(newPosition.y, minBound.y + 0.5f, maxBound.y - 0.5f);

            transform.position = new Vector2(xBound, yBound);

        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

    }

}
