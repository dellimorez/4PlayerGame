using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUIScript : MonoBehaviour
{
    public static Health playerHealth;
    public Sprite FullHealthSprite;
    public Sprite HalfHealthSprite;
    public Sprite LowHealthSprite;

    private Image healthImage;

    private void Start()
    {
        healthImage = GetComponent<Image>();
    }

    void FixedUpdate()
    {
        if (!playerHealth) return;

        if (playerHealth.currentHealth >= 3)
        {
            healthImage.sprite = FullHealthSprite;
        }
        else if (playerHealth.currentHealth >= 2)
        {
            healthImage.sprite = HalfHealthSprite;
        }
        else
        {
            healthImage.sprite = LowHealthSprite;
        }
    }
}

