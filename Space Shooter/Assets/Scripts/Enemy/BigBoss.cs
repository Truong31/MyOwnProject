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
        GameManager.Instance.isBigBoss = true;

        if (currentHealth <= maxHealth * 0.8f && currentHealth > maxHealth * 0.6f)
        {
            InvokeRepeating(nameof(FireRocket), 1.0f, Random.Range(2.5f, 5f));
        }
        else if(currentHealth <= maxHealth * 0.6f && currentHealth > maxHealth * 0.4f)
        {
            InvokeRepeating(nameof(DropBomb), 1.0f, 5);
        }
        else if(currentHealth <= maxHealth * 0.4f)
        {
            InvokeRepeating(nameof(FireRocket), 1.0f, Random.Range(2.5f, 5f));
            InvokeRepeating(nameof(DropBomb), 1.0f, 5);
        }
    }

    private void FireRocket()
    {
        Instantiate(rocketPrefabs, transform.position, Quaternion.identity);
    }

    private void DropBomb()
    {
        Instantiate(bombPrefabs, transform.position, Quaternion.identity);
    }
}
