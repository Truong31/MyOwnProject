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
        InvokeRepeating(nameof(FireRocket), 1.0f, Random.Range(2.5f, 5f));
        InvokeRepeating(nameof(DropBomb), 1.0f, 5);
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
