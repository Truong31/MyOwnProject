using UnityEngine;

public class Obstacle : MonoBehaviour
{
    //private new Rigidbody2D rigidbody2D;

    private void Update()
    {
        if (!Utility.IsInScreen(transform.position))
        {
            Destroy(gameObject, 3.0f);
        }

    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    rigidbody2D.isKinematic = false;
    //}

}
