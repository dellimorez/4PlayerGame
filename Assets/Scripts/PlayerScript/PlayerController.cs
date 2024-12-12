using System;
using UnityEngine;

namespace PlayerScript
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] public float playerSpeed = 1f;
        public GameObject PlayerRoomCollider;

        public static Vector2 playerPosition = Vector2.zero;
        public static Tuple<int, int> currentRoom;
        public static float speedBonus = 1f;
        public static bool[] keysCollected;
        public static bool[] notesCollected;
        public static bool inCombat = false;

        private Vector2 movement;
        private Rigidbody2D rb;
        private Animator anim;
        private Vector3 originalScale; // Store the original scale


        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            currentRoom = new Tuple<int, int>(0, 0);
            keysCollected = new bool[LevelGenerator.staticKeys.Length];
            notesCollected = new bool[LevelGenerator.staticKeys.Length];
            anim = GetComponent<Animator>();
            originalScale = transform.localScale; // Save the original scale at the start
        }

        // Update is called once per frame
        private void Update()
        {
            // Get input from WASD keys
            movement.x = Input.GetAxisRaw("Horizontal"); // 'A' and 'D' keys
            movement.y = Input.GetAxisRaw("Vertical"); // 'W' and 'S' keys

            // Normalize the movement vector to ensure consistent speed in all directions
            movement = movement.normalized;

            // Flip player sprite based on movement direction
            if (movement.x < 0) // Moving left
            {
                transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
            }
            else if (movement.x > 0) // Moving right
            {
                transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
            }

            // Set animator parameters
            anim.SetBool("run", movement.x != 0 || movement.y != 0);
        }

        private void FixedUpdate()
        {
            rb.velocity = movement * (playerSpeed * speedBonus * Time.fixedDeltaTime);
            playerPosition = transform.position;
            PlayerRoomCollider.transform.position = playerPosition;
        }

        // Logic to determine if the player can attack in a direction
        public bool canAttack(Vector2 attackDirection)
        {
            // You can add conditions here based on attack cooldowns or other factors
            return attackDirection != Vector2.zero; // Ensures the player is pressing an attack direction
        }
    }
}
