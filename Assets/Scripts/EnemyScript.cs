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

    // Start is called before the first frame update
    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();  // Get the SpriteRenderer component
        originalColor = spriteRenderer.color;  // Store the original color of the sprite
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
        FlashRed();  // Call the function to flash red

        if (health <= 0)
        {
            Die();
        }
    }

    private void FlashRed()
    {
        spriteRenderer.color = flashColor;  // Change the sprite color to red
        Invoke("ResetColor", flashDuration); // Reset the color after a delay
    }

    private void ResetColor()
    {
        spriteRenderer.color = originalColor;  // Revert the sprite color to its original
    }

    private void Die()
    {
        // Deactivate the enemy object
        Destroy(gameObject);
    }

    // If enemy is able to be phased through
    virtual public void OnTriggerEnter2D(Collider2D collision)
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

    // If enemy is solid
    virtual public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get the player's Health component and apply damage
            Health playerHealth = collision.gameObject.GetComponent<Health>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(strength); // Damage the player by the enemy's strength
            }
        }
    }
}
