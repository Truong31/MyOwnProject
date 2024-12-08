using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBoss : Boss
{
    public Rocket rocketPrefabs;
    public Bomb bombPrefabs;

    public override void Start()
    {
        base.Start();
        InvokeRepeating(nameof(FireRocket), 5.0f, 5.0f);
        InvokeRepeating(nameof(DropBomb), 5.0f, 5.0f);
    }

    private void FireRocket()
    {
        if (currentHealth <= maxHealth)
        {
            Instantiate(rocketPrefabs, transform.position, Quaternion.identity);
        }
    }

    private void DropBomb()
    {
        if(currentHealth <= maxHealth)
        {
            Instantiate(bombPrefabs, transform.position, Quaternion.identity);
        }
    }
}
