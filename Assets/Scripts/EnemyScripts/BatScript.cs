using PlayerScript;
using UnityEngine;
using System.Collections;

namespace EnemyScripts
{
    public class BatScript : EnemyScript
    {
        public float timeToCircle = 0f;
        public float circleRadius = 10f;
        public AudioClip wingFlapSound;
        public float flapSoundInterval = 1f;
        public float flapVolume = 0.5f;

        private bool chasingPlayer = false;
        private bool circlingPlayer = false;
        private bool batDiving = false;
        private float startTimeCircling = 0f;
        private float currentCirclingAngle = 0f;
        private Vector2 desiredPosition;

        private bool isPlayingFlapSound = false;

        public override void Update()
        {
            if (CutsceneManager.staticTimeline.state == UnityEngine.Playables.PlayState.Playing) return;

            Vector2 playerPosition = PlayerController.playerPosition;
            float distanceToDesiredPosition = Vector2.Distance(transform.position, desiredPosition);

            if (distanceToDesiredPosition <= 1f || (!chasingPlayer && !circlingPlayer && !batDiving)
                || (chasingPlayer && distanceToDesiredPosition <= circleRadius))
            {
                if (batDiving && distanceToDesiredPosition <= 1f)
                {
                    batDiving = false;
                }

                if (circlingPlayer && Time.time > startTimeCircling + timeToCircle && !batDiving)
                {
                    batDiving = true;
                    circlingPlayer = false;
                    float angle = Mathf.Atan2(playerPosition.y - transform.position.y,
                        playerPosition.x - transform.position.x);
                    float r = circleRadius;

                    float x = r * Mathf.Cos(angle);
                    float y = r * Mathf.Sin(angle);
                    desiredPosition = playerPosition + new Vector2(x, y);
                }
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

            if (chasingPlayer)
            {
                desiredPosition = playerPosition;
            }

            float angleDiff = Mathf.Atan2(desiredPosition.y - transform.position.y,
                desiredPosition.x - transform.position.x);
            movement = new Vector2(Mathf.Cos(angleDiff), Mathf.Sin(angleDiff));
            movement.Normalize();

            if (!isPlayingFlapSound && (chasingPlayer || circlingPlayer || batDiving))
            {
                StartCoroutine(PlayWingFlapSound());
            }
        }

        public override void FixedUpdate()
        {
            if (CutsceneManager.staticTimeline.state == UnityEngine.Playables.PlayState.Playing) return;

            float speedModifier = 1f;
            if (batDiving || circlingPlayer)
            {
                speedModifier = 2.5f;
            }

            rb.velocity = movement * speed * speedModifier * Time.deltaTime;
        }

        private IEnumerator PlayWingFlapSound()
        {
            isPlayingFlapSound = true;

            while (chasingPlayer || circlingPlayer || batDiving)
            {
                if (wingFlapSound != null)
                {
                    SoundManager.instance.PlaySound(wingFlapSound, flapVolume);
                }
                yield return new WaitForSeconds(flapSoundInterval);
            }

            isPlayingFlapSound = false;
        }
    }
}
