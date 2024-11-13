using PlayerScript;
using UnityEngine;

namespace EnemyScripts
{
    public class BatScript : EnemyScript
    {
        public float timeToCircle = 0f;
        public float circleRadius = 10f;
        //public GameObject target;

        private bool chasingPlayer = false;
        private bool circlingPlayer = false;
        private bool batDiving = false;
        private float startTimeCircling = 0f;
        private float currentCirclingAngle = 0f;
        private Vector2 desiredPosition;

        public override void Start()
        {
            base.Start();

            health = 2;
            strength = 1;
        }

        public override void Update()
        {
            // Check if close to player, if not go to them
            // If you are circle around them
            Vector2 playerPosition = PlayerController.playerPosition;
            float distanceToDesiredPosition = Vector2.Distance(transform.position, desiredPosition);

            if(distanceToDesiredPosition <= 1f || (!chasingPlayer && !circlingPlayer && !batDiving) 
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
                else if ( ((chasingPlayer && distanceToDesiredPosition <= circleRadius) ||
                           (circlingPlayer && distanceToDesiredPosition <= 1f)) && !batDiving )
                {
                    // If you weren't previously circling the player
                    if (chasingPlayer)
                    {
                        currentCirclingAngle = Mathf.Atan2(transform.position.y - playerPosition.y,
                            transform.position.x - playerPosition.x) * Mathf.Rad2Deg;
                        chasingPlayer = false;
                        startTimeCircling = Time.time;
                    } else
                    {
                        currentCirclingAngle += 10;
                    }

                    circlingPlayer = true;
                    // Get desired position around player
                    float r = circleRadius;

                    // Convert to Cartesian
                    float x = r * Mathf.Cos(currentCirclingAngle * Mathf.Deg2Rad);
                    float y = r * Mathf.Sin(currentCirclingAngle * Mathf.Deg2Rad);
                    desiredPosition = playerPosition + new Vector2(x, y);
                }

                // Get chasing position
                else if (!circlingPlayer)
                {
                    chasingPlayer = true;
                }

            }

            if(chasingPlayer) { desiredPosition = playerPosition; }

            // Get angle diff between desired position and self
            float angleDiff = Mathf.Atan2(desiredPosition.y - transform.position.y,
                desiredPosition.x - transform.position.x);

            // Move to new position
            movement = new Vector2(Mathf.Cos(angleDiff), Mathf.Sin(angleDiff));
            movement.Normalize();
        }

        public override void FixedUpdate()
        {
            float speedModifier = 1f;
            if(batDiving || circlingPlayer)
            {
                speedModifier = 2.5f;
            }

            rb.velocity = movement * speed * speedModifier * Time.deltaTime;

            //target.transform.position = desiredPosition;
        }

        // Temporary before weapons
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
        }
    }
}
