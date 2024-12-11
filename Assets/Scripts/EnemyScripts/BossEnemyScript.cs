using PlayerScript;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyScript : EnemyScript
{
    public GameObject EnemyProjectile;
    public int minTimeToShoot = 3;
    public int maxTimeToShoot = 6;

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
}
