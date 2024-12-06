using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBoss : Boss
{
    public Rocket rocketPrefabs;

    public override void Start()
    {
        base.Start();
        InvokeRepeating(nameof(FireRocket), 5.0f, 5.0f);
    }

    private void FireRocket()
    {
        if (currentHealth <= maxHealth)
        {
            Instantiate(rocketPrefabs, transform.position, Quaternion.identity);
        }
    }
}
