using UnityEngine;

namespace PlayerScript
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed = 5f; // Speed at which the projectile moves
        private Vector2 direction; // Direction the projectile will move in
        private Vector3 defaultScale = new Vector3(0.15f, 0.15f, 0.15f); // Uniform scale

        private void Start()
        {
            AdjustProjectileOrientation();
        }

        virtual public void FixedUpdate()
        {
            // Ensure the projectile moves properly in its set direction
            transform.position += (Vector3)direction * speed * Time.deltaTime;

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
            // Check if the projectile collides with an object tagged as "Enemy"
            if (collision.CompareTag("Enemy"))
            {
                EnemyScript enemy = collision.GetComponent<EnemyScript>();
                if (enemy != null)
                {
                    enemy.TakeDamage(1); // Apply damage
                }
                Destroy(gameObject); // Destroy the projectile on impact
            }
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("StaticObject"))
            {
                Destroy(gameObject);
            }
        }
    }
}
