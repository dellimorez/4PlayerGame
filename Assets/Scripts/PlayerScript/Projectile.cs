using UnityEngine;

namespace PlayerScript
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed; // Speed at which the fireball moves
        private Vector2 direction; // Direction the fireball will move in

        virtual public void FixedUpdate()
        {
            // Move the fireball in the specified direction
            transform.Translate(direction * speed * Time.deltaTime);

            // Destroy the fireball after 5 seconds to avoid it staying forever
            Destroy(gameObject, 5f);
        }

        virtual public void SetDirection(Vector2 dir)
        {
            // Set the direction of the fireball based on input
            direction = dir;
        }

        virtual public void OnTriggerEnter2D(Collider2D collision)
        {
            // Check if the projectile collides with an object tagged as "Enemy"
            if (collision.CompareTag("Enemy"))
            {
                // Get the enemy's script and apply damage
                EnemyScript enemy = collision.GetComponent<EnemyScript>();
                if (enemy != null)
                {
                    enemy.TakeDamage(1); // Apply damage
                }

                // Destroy the projectile on impact
                Destroy(gameObject);
            }
            Destroy(gameObject);
        }
    }
}
