using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private void Update()
    {
        if (!GameManager.Instance.isBossLive)
        {
            Destroy(gameObject);
        }
    }
}
