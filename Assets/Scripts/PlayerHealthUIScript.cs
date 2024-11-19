using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUIScript : MonoBehaviour
{
    public static Health playerHealth;
    public Color FullHealthColor;
    public Color HalfHealthColor;
    public Color LowHealthColor;

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
            healthImage.color = FullHealthColor;
        } 
        else if (playerHealth.currentHealth >= 2)
        {
            healthImage.color = HalfHealthColor;
        }
        else
        {
            healthImage.color = LowHealthColor;
        }

    }
}
