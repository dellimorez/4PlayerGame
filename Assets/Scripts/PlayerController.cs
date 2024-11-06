using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float playerSpeed = 1f;

    public static Vector2 playerPosition = Vector2.zero;

    private Vector2 movement;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    // Update is called once per frame
    void Update()
    {
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), 
            Input.GetAxisRaw("Vertical"));
        movement.Normalize();
    }

    private void FixedUpdate()
    {
        rb.velocity = movement * playerSpeed * Time.fixedDeltaTime;
        playerPosition = rb.position;
    }
}
