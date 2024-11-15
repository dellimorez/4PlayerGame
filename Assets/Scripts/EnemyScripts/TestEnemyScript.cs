using PlayerScript;
using UnityEngine;

namespace EnemyScripts
{
    public class TestEnemyScript : EnemyScript
    {
        public int attackStrength = 10;  // Amount of damage the enemy deals (now an int)
        private Animator animator;        // To handle the attack animation trigger

        private void Start()
        {
            // Get the Animator component attached to this enemy object
            animator = GetComponent<Animator>();
        }

        // This method gets called when another collider enters this enemy's trigger zone
        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Check if the object that collided with the enemy is the player
            if (collision.gameObject.CompareTag("Player"))
            {
                // Log a message for debugging
                Debug.Log("Enemy collided with the player!");

                // Get the player's Health component
                Health playerHealth = collision.gameObject.GetComponent<Health>();

                // Check if the player has a Health component
                if (playerHealth != null)
                {
                    // Log to confirm the player has a Health component
                    Debug.Log("Player has a Health component. Applying damage...");

                    // Apply damage to the player (now passing an int)
                    playerHealth.TakeDamage(attackStrength);

                    // Log the player's new health after damage
                    Debug.Log("Player health after damage: " + playerHealth.currentHealth);
                }

                // Trigger the enemy's attack animation using a trigger
                if (animator != null)
                {
                    // Assuming your animator has an "Attack" trigger set up
                    animator.SetTrigger("Attack");
                }
                else
                {
                    Debug.Log("Enemy does not have an Animator component!");
                }
            }
        }
    }
}


