using PlayerScript;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomEnemySpawner : EnemySpawnerScript
{
    public GameObject BossPrefab;

    private const int numEnemies = 4;

    // Start is called before the first frame update
    override protected void Start()
    {
        PlayerController.inCombat = true;

        if (EnemyPrefabs.Length == 0) { return; }
        enemies = new GameObject[numEnemies];

        enemies[0] = Instantiate(BossPrefab);
        enemies[0].transform.position = rs.transform.position;

        for (int i = 1; i < numEnemies; i++)
        {
            int randomEnemy = UnityEngine.Random.Range(0, EnemyPrefabs.Length);
            enemies[i] = Instantiate(EnemyPrefabs[randomEnemy]);

            Vector3 pos = rs.transform.position;
            BoxCollider2D col = rs.gameObject.GetComponent<BoxCollider2D>();
            int xScale = (int)col.size.x;
            int yScale = (int)col.size.y;
            int xOffset = UnityEngine.Random.Range(0, xScale) - (xScale / 2);
            int yOffset = UnityEngine.Random.Range(0, yScale) - (yScale / 2);

            pos.x += xOffset;
            pos.y += yOffset;

            enemies[i].transform.position = pos;
        }
    }
}
