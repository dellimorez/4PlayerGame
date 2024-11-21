using UnityEngine;

public class HealthCollectibleSpawner : MonoBehaviour
{
    public GameObject healthCollectiblePrefab;  // The health collectible prefab to spawn
    public RoomScript rs;  // Reference to the room to spawn the collectible in
    public int minCollectibles = 1;  // Minimum number of health collectibles to spawn
    public int maxCollectibles = 3;  // Maximum number of health collectibles to spawn

    private GameObject[] healthCollectibles;

    // Start is called before the first frame update
    void Start()
    {
        SpawnHealthCollectibles();
    }

    // Spawns health collectibles at random positions within the room
    void SpawnHealthCollectibles()
    {
        if (healthCollectiblePrefab == null || rs == null)
        {
            Debug.LogError("HealthCollectiblePrefab or RoomScript reference is missing!");
            return;
        }

        BoxCollider2D roomCollider = rs.gameObject.GetComponent<BoxCollider2D>();
        if (roomCollider == null)
        {
            Debug.LogError("RoomScript object does not have a BoxCollider2D component.");
            return;
        }

        // Determine how many collectibles to spawn
        int numCollectibles = UnityEngine.Random.Range(minCollectibles, maxCollectibles + 1);
        healthCollectibles = new GameObject[numCollectibles];

        int xScale = (int)roomCollider.size.x - 5;
        int yScale = (int)roomCollider.size.y - 5;

        Vector3 spawnPosition;

        for (int i = 0; i < numCollectibles; i++)
        {
            // Randomize spawn position within the room's boundaries
            int xOffset = UnityEngine.Random.Range(0, xScale) - (xScale / 2);
            int yOffset = UnityEngine.Random.Range(0, yScale) - (yScale / 2);

            spawnPosition = rs.transform.position;
            spawnPosition.x += xOffset;
            spawnPosition.y += yOffset;

            // Spawn the health collectible at the randomized position
            healthCollectibles[i] = Instantiate(healthCollectiblePrefab, spawnPosition, Quaternion.identity);
        }
    }
}
