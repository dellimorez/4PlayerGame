using PlayerScript;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerup : MonoBehaviour
{
    public float speedIncreaseAmount = 0.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object that collided with the collectible is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController.speedBonus += speedIncreaseAmount;
            Destroy(gameObject);
        }
    }
}
