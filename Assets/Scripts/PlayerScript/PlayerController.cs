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
        public static bool[] keysCollected;

        private Vector2 movement;
        private Rigidbody2D rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            currentRoom = new Tuple<int, int>(0, 0);
            keysCollected = new bool[LevelGenerator.staticKeys.Length];
        }

        // Update is called once per frame
        private void Update()
        {
            // Get input from WASD keys
            movement.x = Input.GetAxisRaw("Horizontal"); // 'A' and 'D' keys
            movement.y = Input.GetAxisRaw("Vertical"); // 'W' and 'S' keys

            // Normalize the movement vector to ensure consistent speed in all directions
            movement = movement.normalized;
        }

        private void FixedUpdate()
        {
            rb.velocity = movement * (playerSpeed * Time.fixedDeltaTime);
            playerPosition = transform.position;
            PlayerRoomCollider.transform.position = playerPosition;
        }

        // Logic to determine if the player can attack
        public bool canAttack()
        {
            return true;
        }
    }
}
