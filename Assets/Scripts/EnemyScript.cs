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
    protected Animator animator;
    private SpriteRenderer spriteRenderer;  // Add a reference to the SpriteRenderer
    public Color flashColor = Color.red;    // Color to flash when hit
    public float flashDuration = 0.1f;      // How long to flash red

    private Color originalColor;            // Store the original color

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

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

    public virtual void TakeDamage(int damage)
    {
        health -= damage;

        // Trigger the "Hurt" animation
        if (animator != null)
        {
            animator.SetTrigger("hurt");
        }

        FlashRed();

        if (health <= 0)
        {
            Die();
        }
    }

    private void FlashRed()
    {
        spriteRenderer.color = flashColor;
        Invoke("ResetColor", flashDuration);
    }

    private void ResetColor()
    {
        spriteRenderer.color = originalColor;
    }

    private void Die()
    {
        Destroy(gameObject); // Deactivate the enemy object
    }

    // If enemy is able to be phased through
    virtual public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Health playerHealth = collision.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(strength);
            }
        }
    }

    // If enemy is solid
    virtual public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(strength);
            }
        }
    }
}
