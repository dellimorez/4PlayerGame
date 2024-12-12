using PlayerScript;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    public GameObject[] EnemyPrefabs;
    public RoomScript rs;

    protected GameObject[] enemies;

    // Start is called before the first frame update
    virtual protected void Start()
    {
        PlayerController.inCombat = true;

        if (EnemyPrefabs.Length == 0) { return; }
        int numEnemies = UnityEngine.Random.Range(2, 6);
        enemies = new GameObject[numEnemies];

        int randomEnemy;
        BoxCollider2D col = rs.gameObject.GetComponent<BoxCollider2D>();
        int xScale = (int)col.size.x - 5;
        int yScale = (int)col.size.y - 5;

        Vector3 pos;

        for (int i = 0; i < numEnemies; i++)
        {
            randomEnemy = UnityEngine.Random.Range(0, EnemyPrefabs.Length);

            enemies[i] = Instantiate(EnemyPrefabs[randomEnemy]);

            int xOffset = UnityEngine.Random.Range(0, xScale) - (xScale / 2);
            int yOffset = UnityEngine.Random.Range(0, yScale) - (yScale / 2);

            pos = rs.transform.position;
            pos.x += xOffset;
            pos.y += yOffset;

            enemies[i].transform.position = pos;
        }
    }

    virtual protected void FixedUpdate()
    {
        int enemiesAlive = 0;
        foreach (var enemy in enemies)
        {
            if (enemy) { enemiesAlive++; }
        }

        if (enemiesAlive == 0)
        {
            PlayerController.inCombat = false;
            rs.UnlockRoom();
            Destroy(gameObject);
        }
    }
}
