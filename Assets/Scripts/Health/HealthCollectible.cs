using PlayerScript;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public int healthRestoreAmount = 1; // Amount of health the collectible restores

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object that collided with the collectible is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get the Health component from the player
            Health playerHealth = collision.GetComponent<Health>();

            if (playerHealth != null && playerHealth.currentHealth < 3)
            {
                // Restore health to the player
                playerHealth.AddHealth(healthRestoreAmount);

                // Destroy the health collectible after it's used
                Destroy(gameObject);
            }
        }
    }
}
