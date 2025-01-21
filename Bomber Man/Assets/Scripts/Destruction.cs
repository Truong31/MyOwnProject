using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruction : MonoBehaviour
{
    private float spawnRate = 0.05f;
    public PowerUp[] powerUp;

    private void Start()
    {
        Destroy(this.gameObject, 1.25f);
    }

    //NOTE: Sinh ra cac power sau khi brick bi pha huy
    private void OnDestroy()
    {
        if(spawnRate > Random.value)
        {
            Instantiate(powerUp[Random.Range(0, powerUp.Length)], transform.position, Quaternion.identity);
        }

    }
}
