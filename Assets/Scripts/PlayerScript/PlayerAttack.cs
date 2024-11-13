using UnityEngine;

namespace PlayerScript
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private GameObject firepoint;  // Reference to the firepoint object
        [SerializeField] private float attackRange = 1f; // The range for firepoint positioning
        [SerializeField] private GameObject projectilePrefab; // Reference to the projectile prefab
        [SerializeField] private float attackCooldown = 0.5f; // Cooldown for attacks

        private float lastAttackTime = 0f; // Time of the last attack
        private bool canAttack = true; // Can the player attack?

        private void Update()
        {
            // Check for attack inputs (arrow keys) and cooldown
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    MoveFirepoint(Vector2.up);
                    FireProjectile(Vector2.up);
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    MoveFirepoint(Vector2.down);
                    FireProjectile(Vector2.down);
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    MoveFirepoint(Vector2.left);
                    FireProjectile(Vector2.left);
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    MoveFirepoint(Vector2.right);
                    FireProjectile(Vector2.right);
                }
            }
        }

        private void MoveFirepoint(Vector2 direction)
        {
            // Update firepoint's position relative to the player's position
            firepoint.transform.position = (Vector2)transform.position + direction * attackRange;
        }

        private void FireProjectile(Vector2 direction)
        {
            // Check if the firepoint and projectilePrefab are assigned
            if (firepoint != null && projectilePrefab != null)
            {
                // Instantiate the projectile at the firepoint's position
                GameObject projectile = Instantiate(projectilePrefab, firepoint.transform.position, Quaternion.identity);

                // Make sure the fireball is active
                projectile.SetActive(true);

                // Set the direction of the fireball (it will move in the direction passed in the method)
                projectile.GetComponent<Projectile>().SetDirection(direction);

                lastAttackTime = Time.time; // Reset the attack cooldown
            }
        }
    }
}
