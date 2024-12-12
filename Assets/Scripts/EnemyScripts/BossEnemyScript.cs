using PlayerScript;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyScript : EnemyScript
{
    public GameObject EnemyProjectile;
    public int meleeDamage = 2;        // Damage the boss deals on melee attack
    public float attackCooldown = 2f; // Cooldown between attacks
    public int minTimeToShoot = 3;
    public int maxTimeToShoot = 6;

    private bool canAttack = true;    // Tracks if the boss can attack
    private float timeSinceLastShot;
    private int timeToShootNext;
    public override void Start()
    {
        base.Start();

        timeToShootNext = Random.Range(minTimeToShoot, maxTimeToShoot + 1);
        timeSinceLastShot = Time.time;
    }

    public override void Update()
    {
        base.Update();

        if (timeToShootNext + (int)timeSinceLastShot <= Time.time)
        {
            Shoot();
            timeToShootNext = Random.Range(minTimeToShoot, maxTimeToShoot + 1);
            timeSinceLastShot = Time.time;
        }
    }

    private void Shoot()
    {
        GameObject obj = Instantiate(EnemyProjectile, gameObject.transform.position, Quaternion.identity);
        EnemyProjectile projectile = obj.GetComponent<EnemyProjectile>();
        projectile.SetDirection(movement);
        obj.SetActive(true);
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && canAttack)
        {
            Attack(collision.gameObject);
        }
        else
        {
            // Call base behavior for other triggers
            base.OnTriggerEnter2D(collision);
        }
    }

    private void Attack(GameObject player)
    {
        // Trigger the attack animation
        if (animator != null)
        {
            animator.SetTrigger("attack");
        }

        // Deal damage to the player
        Health playerHealth = player.GetComponent<Health>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(meleeDamage);
        }

        // Start cooldown timer
        StartCoroutine(AttackCooldown());
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);  // Apply base damage logic

        // Trigger hurt animation
        if (animator != null)
        {
            animator.SetTrigger("hurt");
        }
    }
}
