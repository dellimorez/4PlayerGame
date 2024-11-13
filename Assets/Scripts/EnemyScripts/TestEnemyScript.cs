using PlayerScript;
using UnityEngine;

namespace EnemyScripts
{
    public class TestEnemyScript : EnemyScript
    {
        // Update is called once per frame
        public override void Update()
        {
            Vector2 playerPosition = PlayerController.playerPosition;
            float angleDiff = Mathf.Atan2(playerPosition.y - transform.position.y,
                playerPosition.x - transform.position.x);

            movement = new Vector2(Mathf.Cos(angleDiff), Mathf.Sin(angleDiff));
            movement.Normalize();
        }

        public override void FixedUpdate()
        {
            rb.velocity = movement * speed * Time.deltaTime;
        }

        // Temporary before weapons
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
        }
    }
}
