using PlayerScript;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float speed = 1;
    public int health = 1;
    public int strength = 1; // Damage the enemy deals to the player
    public int maxEnemyCount = 10;

    public Vector2 movement;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        Vector2 playerPosition = PlayerController.playerPosition;
        float angleDiff = Mathf.Atan2(playerPosition.y - transform.position.y,
            playerPosition.x - transform.position.x);

        movement = new Vector2(Mathf.Cos(angleDiff), Mathf.Sin(angleDiff));
        movement.Normalize();
    }

    public virtual void FixedUpdate()
    {
        rb.velocity = movement * speed * Time.deltaTime;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Deactivate the enemy object
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get the player's Health component and apply damage
            Health playerHealth = collision.GetComponent<Health>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(strength); // Damage the player by the enemy's strength
            }
        }
    }
}
