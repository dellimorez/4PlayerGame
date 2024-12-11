using PlayerScript;
using UnityEngine;
using System.Collections;

namespace EnemyScripts
{
    public class BatScript : EnemyScript
    {
        public float timeToCircle = 0f;
        public float circleRadius = 10f;
        public AudioClip wingFlapSound;  // Bat wing flap sound
        public float flapSoundInterval = 1f;  // Time between wing flap sounds
        public float flapVolume = 0.5f;  // Volume of the wing flap sound

        private bool chasingPlayer = false;
        private bool circlingPlayer = false;
        private bool batDiving = false;
        private float startTimeCircling = 0f;
        private float currentCirclingAngle = 0f;
        private Vector2 desiredPosition;

        private bool isPlayingFlapSound = false; // To prevent multiple coroutines for the flap sound

        public override void Update()
        {
            Vector2 playerPosition = PlayerController.playerPosition;
            float distanceToDesiredPosition = Vector2.Distance(transform.position, desiredPosition);

            if (distanceToDesiredPosition <= 1f || (!chasingPlayer && !circlingPlayer && !batDiving)
                                               || (chasingPlayer && distanceToDesiredPosition <= circleRadius))
            {
                if (batDiving && distanceToDesiredPosition <= 1f)
                {
                    batDiving = false;
                }

                // Get Diving position
                if (circlingPlayer && Time.time > startTimeCircling + timeToCircle && !batDiving)
                {
                    batDiving = true;
                    circlingPlayer = false;
                    float angle = Mathf.Atan2(playerPosition.y - transform.position.y,
                        playerPosition.x - transform.position.x);
                    float r = circleRadius;

                    // Convert to Cartesian
                    float x = r * Mathf.Cos(angle);
                    float y = r * Mathf.Sin(angle);
                    desiredPosition = playerPosition + new Vector2(x, y);
                }

                // Get circling position
                else if (((chasingPlayer && distanceToDesiredPosition <= circleRadius) ||
                           (circlingPlayer && distanceToDesiredPosition <= 1f)) && !batDiving)
                {
                    if (chasingPlayer)
                    {
                        currentCirclingAngle = Mathf.Atan2(transform.position.y - playerPosition.y,
                            transform.position.x - playerPosition.x) * Mathf.Rad2Deg;
                        chasingPlayer = false;
                        startTimeCircling = Time.time;
                    }
                    else
                    {
                        currentCirclingAngle += 10;
                    }

                    circlingPlayer = true;
                    float r = circleRadius;
                    float x = r * Mathf.Cos(currentCirclingAngle * Mathf.Deg2Rad);
                    float y = r * Mathf.Sin(currentCirclingAngle * Mathf.Deg2Rad);
                    desiredPosition = playerPosition + new Vector2(x, y);
                }
                else if (!circlingPlayer)
                {
                    chasingPlayer = true;
                }
            }

            if (chasingPlayer) { desiredPosition = playerPosition; }

            float angleDiff = Mathf.Atan2(desiredPosition.y - transform.position.y,
                desiredPosition.x - transform.position.x);
            movement = new Vector2(Mathf.Cos(angleDiff), Mathf.Sin(angleDiff));
            movement.Normalize();

            // Start playing the wing flap sound when the bat starts moving
            if (!isPlayingFlapSound && (chasingPlayer || circlingPlayer || batDiving))
            {
                StartCoroutine(PlayWingFlapSound()); // Start the wing flap sound coroutine
            }
        }

        public override void FixedUpdate()
        {
            float speedModifier = 1f;
            if (batDiving || circlingPlayer)
            {
                speedModifier = 2.5f;
            }

            rb.velocity = movement * speed * speedModifier * Time.deltaTime;
        }

        // Coroutine to play the wing flap sound at intervals
        private IEnumerator PlayWingFlapSound()
        {
            isPlayingFlapSound = true;

            while (chasingPlayer || circlingPlayer || batDiving)  // Keep playing the sound while the bat is active
            {
                if (wingFlapSound != null)
                {
                    SoundManager.instance.PlaySound(wingFlapSound, flapVolume); // Adjusted to play with custom volume
                }
                yield return new WaitForSeconds(flapSoundInterval);  // Wait for the interval before playing again
            }

            // Stop playing sound when bat stops moving
            isPlayingFlapSound = false;
        }
    }
}
