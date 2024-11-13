using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    public GameObject[] EnemyPrefabs;
    public RoomScript rs;

    private GameObject[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        if (EnemyPrefabs.Length == 0) { return; }
        int numEnemies = UnityEngine.Random.Range(2, 6);
        enemies = new GameObject[numEnemies];

        for (int i = 0; i < numEnemies; i++)
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

    private void FixedUpdate()
    {
        int enemiesAlive = 0;
        foreach (var enemy in enemies)
        {
            if (enemy) { enemiesAlive++; }
        }

        if (enemiesAlive == 0)
        {
            rs.UnlockRoom();
            Destroy(gameObject);
        }
    }
}
