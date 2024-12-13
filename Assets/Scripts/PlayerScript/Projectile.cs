using UnityEngine;

namespace PlayerScript
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed = 5f; // Speed at which the projectile moves
        private Vector2 direction; // Direction the projectile will move in
        private Vector3 defaultScale = new Vector3(0.15f, 0.15f, 0.15f); // Uniform scale
        private Animator anim;
        private bool hasExploded = false;  // Prevents multiple explosions

        private void Start()
        {
            anim = GetComponent<Animator>();
            AdjustProjectileOrientation();
        }

        virtual public void FixedUpdate()
        {
            if (hasExploded) return;

            // Ensure the projectile moves properly in its set direction
            transform.position += (Vector3)direction * speed * Time.deltaTime;

            float directionFacing = direction.x < 0 ? -1f : 1f;

            transform.localScale = new Vector3(directionFacing * defaultScale.x, defaultScale.y, defaultScale.z); // Flip X

            // Destroy the projectile after 5 seconds
            Destroy(gameObject, 5f);
        }

        virtual public void SetDirection(Vector2 dir)
        {
            // Set the direction of the projectile
            direction = dir.normalized;
            AdjustProjectileOrientation();
        }

        private void AdjustProjectileOrientation()
        {
            // Adjust rotation and scale based on the direction
            if (direction == Vector2.up)
            {
                transform.rotation = Quaternion.Euler(0, 0, 90);
                transform.localScale = defaultScale;
            }
            else if (direction == Vector2.down)
            {
                transform.rotation = Quaternion.Euler(0, 0, -90);
                transform.localScale = defaultScale;
            }
            else if (direction == Vector2.left)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                transform.localScale = new Vector3(-defaultScale.x, defaultScale.y, defaultScale.z); // Flip X
            }
            else if (direction == Vector2.right)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                transform.localScale = defaultScale;
            }
        }

        virtual public void OnTriggerEnter2D(Collider2D collision)
        {
            if (hasExploded) return;

            // Trigger explosion if projectile hits an enemy
            if (collision.CompareTag("Enemy"))
            {
                EnemyScript enemy = collision.GetComponent<EnemyScript>();
                if (enemy != null)
                {
                    enemy.TakeDamage(1); // Apply damage
                }
                TriggerExplosion();
            }
            else if (collision.CompareTag("StaticObject"))
            {
                TriggerExplosion();
            }
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (hasExploded) return;

            if (collision.collider.CompareTag("StaticObject"))
            {
                TriggerExplosion();
            }
        }

        private void TriggerExplosion()
        {
            if (anim != null)
            {
                anim.SetTrigger("explode");
            }
            hasExploded = true;
            Destroy(gameObject, 0.5f);  // Allow time for explosion animation
        }
    }
}
