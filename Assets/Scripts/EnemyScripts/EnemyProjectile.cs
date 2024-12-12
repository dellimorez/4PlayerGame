using PlayerScript;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile
{
    override public void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the projectile collides with an object tagged as "Player"
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Hit Player");
            // Get the player's script and apply damage
            Health playerHealth = collision.GetComponent<Health>();
            if (playerHealth)
            {
                playerHealth.TakeDamage(1); // Apply damage
            }

            Destroy(gameObject);
        } else if (!collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
