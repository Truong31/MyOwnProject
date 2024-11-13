using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public float speedScroll = 0.5f;
    public Transform background1;     
    public Transform background2;     
    private float backgroundHeight;   

    private void Awake()
    {
        backgroundHeight = background1.GetComponent<SpriteRenderer>().bounds.size.y;
    }

    private void Update()
    {
        background1.position += Vector3.down * speedScroll * Time.deltaTime;
        background2.position += Vector3.down * speedScroll * Time.deltaTime;

        if (background1.position.y <= -backgroundHeight)
        {
            background1.position = new Vector3(background1.position.x, background2.position.y + backgroundHeight, background1.position.z);
        }

        if (background2.position.y <= -backgroundHeight)
        {
            background2.position = new Vector3(background2.position.x, background1.position.y + backgroundHeight, background2.position.z);
        }
    }

}
