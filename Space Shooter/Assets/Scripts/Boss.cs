using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public HealthBar healthBar;
    private int maxHealth = 10;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            currentHealth -= 1;
            healthBar.SetHealth(currentHealth);
            if(currentHealth <= 0)
            {
                healthBar.gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
    }
}
