using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float speed = 1;
    public int health = 1;
    public int strength = 1;

    public Vector2 movement;
    public Rigidbody2D rb;

    // for when we create animations
    // private Animator anim;

    // Start is called before the first frame update
    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public virtual void FixedUpdate()
    {

    }

    public void TakeDamage(int  damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Death Animation
        // anim.SetTrigger("Die");

        // Deactivate the enemy object
        Destroy(gameObject);
    }
}
