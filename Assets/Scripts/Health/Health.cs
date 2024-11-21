using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] public float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] private Behaviour[] components; // Components to disable on death
    private bool invulnerable;

    [Header("Death Sound")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;

    [Header("UI Elements")]
    [SerializeField] private GameObject gameOverScreen; // The UI panel that contains restart and quit buttons
    [SerializeField] private GameObject menu;           // The menu screen 
    [SerializeField] private GameObject youDiedText;    // The "You Died" text GameObject
    [SerializeField] private GameObject restartButton;  // The restart button in the game over menu
    [SerializeField] private GameObject quitButton;     // The quit button in the game over menu

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        PlayerHealthUIScript.playerHealth = this;
    }

    public void TakeDamage(int _damage)
    {
        if (invulnerable || dead) return;

        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt"); // Play hurt animation
            StartCoroutine(Invunerability()); // Activate invulnerability frames
        }
        else
        {
            if (!dead)
            {
                // Disable all component scripts attached to the object
                foreach (Behaviour component in components)
                    component.enabled = false;

                anim.SetTrigger("die"); // Trigger death animation
                dead = true;

                // If this is the player, show the game over screen
                if (CompareTag("Player"))
                {
                    ShowGameOverScreen(); // Display UI elements for Game Over
                }
                else
                {
                    // Handle enemy death (additional logic can be added here if needed)
                }
            }
        }
    }


    // Add health to the object
    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    // Invulnerability frames after getting hit
    private IEnumerator Invunerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(10, 11, true); // Prevent further collisions (adjust if needed)
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f); // Flash the sprite
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false); // Re-enable collisions
        invulnerable = false;
    }

    // Display the game over screen if the player dies
    private void ShowGameOverScreen()
    {
        if(gameOverScreen)
        {
            gameOverScreen.SetActive(true);
            youDiedText.SetActive(true);
            restartButton.SetActive(true);
            quitButton.SetActive(true);
        }
        else // Temporary
        {
            SceneManager.LoadScene("DeathScene");
        }
    }

    // Respawn logic for the player
    public void Respawn()
    {
        AddHealth(startingHealth); // Restore health
        anim.ResetTrigger("die");  // Reset death trigger
        anim.Play("idle-Animation");  // Play idle animation
        StartCoroutine(Invunerability());
        dead = false;

        // Re-enable all attached components
        foreach (Behaviour component in components)
            component.enabled = true;

        // Hide the GameOver screen upon respawn
        gameOverScreen.SetActive(false);
        youDiedText.SetActive(false);
        restartButton.SetActive(false);
        quitButton.SetActive(false);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}

