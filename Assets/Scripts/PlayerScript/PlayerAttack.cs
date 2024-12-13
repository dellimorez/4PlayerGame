using UnityEngine;

namespace PlayerScript
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private GameObject firepoint;  // Reference to the firepoint object
        [SerializeField] private float attackRange = 1f; // The range for firepoint positioning
        [SerializeField] private GameObject projectilePrefab; // Reference to the projectile prefab
        [SerializeField] private float attackCooldown = 0.5f; // Cooldown for attacks
        [SerializeField] private Animator anim; // Reference to the Animator component
        [SerializeField] private AudioClip attackClip; // The sound clip for attack
        [SerializeField] private float attackClipVolume; // The sound clip volume

        private float lastAttackTime = 0f; // Time of the last attack
        private Vector3 originalScale; // Store the original scale of the player

        private void Start()
        {
            originalScale = transform.localScale; // Save the player's original scale for flipping
        }

        private void Update()
        {
            // Check for attack inputs (arrow keys) and cooldown
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    MoveFirepoint(Vector2.up);
                    TriggerAttack();
                    FireProjectile(Vector2.up);
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    MoveFirepoint(Vector2.down);
                    TriggerAttack();
                    FireProjectile(Vector2.down);
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    MoveFirepoint(Vector2.left);
                    TriggerAttack();
                    FireProjectile(Vector2.left);
                    FlipPlayerSprite(-1); // Face left
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    MoveFirepoint(Vector2.right);
                    TriggerAttack();
                    FireProjectile(Vector2.right);
                    FlipPlayerSprite(1); // Face right
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

        private void TriggerAttack()
        {
            // Trigger the attack animation if the animator is assigned
            if (anim != null)
            {
                anim.SetTrigger("attack");
            }

            // Play the attack sound using the SoundManager
            if (attackClip != null)
            {
                SoundManager.instance.PlaySound(attackClip, attackClipVolume); // 1.0f is full volume
            }
        }

        private void FlipPlayerSprite(int direction)
        {
            // Flip the player's sprite based on the attack direction (-1 for left, 1 for right)
            if (direction == -1) // Left
            {
                transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
            }
            else if (direction == 1) // Right
            {
                transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
            }
        }
    }
}
