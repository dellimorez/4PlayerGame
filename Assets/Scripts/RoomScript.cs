using System;
using PlayerScript;
using UnityEngine;
using UnityEngine.UI;

public class RoomScript : MonoBehaviour
{
    public bool roomToLeft;
    public bool roomToRight;
    public bool roomToTop;
    public bool roomToBottom;
    public bool isStartingRoom;
    public bool spawnedEnemies;
    public bool spawnedHealthCollectibles;
    public bool visited = false;
    public LevelGenerator.RoomTypes roomType;
    public Tuple<int, int> pos = new Tuple<int, int>(0, 0);
    public GameObject leftDoor;
    public GameObject rightDoor;
    public GameObject topDoor;
    public GameObject bottomDoor;
    public GameObject leftWall1;
    public GameObject leftWall2;
    public GameObject rightWall1;
    public GameObject rightWall2;
    public GameObject topWall1;
    public GameObject topWall2;
    public GameObject bottomWall1;
    public GameObject bottomWall2;
    public GameObject EnemySpawner;
    public GameObject HealthCollectibleSpawner; // Added health collectible spawner
    public Color activeRoomColor;
    public Color inactiveRoomColor;

    // Start is called before the first frame update
    virtual protected void Start()
    {
        if (!roomToLeft)
        {
            leftWall1.transform.localScale = new Vector3(1, 15, 0);
            Vector3 newPosition = leftWall1.transform.localPosition;
            newPosition.y = 0;
            leftWall1.transform.localPosition = newPosition;
        }
        if (!roomToRight)
        {
            rightWall1.transform.localScale = new Vector3(1, 15, 0);
            Vector3 newPosition = rightWall1.transform.localPosition;
            newPosition.y = 0;
            rightWall1.transform.localPosition = newPosition;
        }
        if (!roomToTop)
        {
            topWall1.transform.localScale = new Vector3(1, 17, 0);
            Vector3 newPosition = topWall1.transform.localPosition;
            newPosition.x = 0;
            topWall1.transform.localPosition = newPosition;
        }
        if (!roomToBottom)
        {
            bottomWall1.transform.localScale = new Vector3(1, 17, 0);
            Vector3 newPosition = bottomWall1.transform.localPosition;
            newPosition.x = 0;
            bottomWall1.transform.localPosition = newPosition;
        }

        leftDoor.SetActive(false);
        rightDoor.SetActive(false);
        topDoor.SetActive(false);
        bottomDoor.SetActive(false);
    }

    virtual protected void LockRoom()
    {
        spawnedEnemies = true;

        if (roomToLeft) { leftDoor.SetActive(true); }
        if (roomToRight) { rightDoor.SetActive(true); }
        if (roomToTop) { topDoor.SetActive(true); }
        if (roomToBottom) { bottomDoor.SetActive(true); }

        // Instantiate the enemy spawner
        GameObject spawner = Instantiate(EnemySpawner);
        EnemySpawnerScript script = spawner.GetComponent<EnemySpawnerScript>();

        script.rs = this;

        // Instantiate health collectible spawner after enemies are spawned
        //SpawnHealthCollectibles();
    }

    // Spawn health collectibles logic
    virtual protected void SpawnHealthCollectibles()
    {
        if (!spawnedHealthCollectibles && HealthCollectibleSpawner != null)
        {
            spawnedHealthCollectibles = true;
            GameObject healthSpawner = Instantiate(HealthCollectibleSpawner);
            HealthCollectibleSpawner healthScript = healthSpawner.GetComponent<HealthCollectibleSpawner>();

            healthScript.rs = this; // Assign the room to the health spawner script
        }
    }

    // Only happens after the player beats the room
    virtual public void UnlockRoom()
    {
        leftDoor.SetActive(false);
        rightDoor.SetActive(false);
        topDoor.SetActive(false);
        bottomDoor.SetActive(false);

        // TODO: Spawn keys if a spawn room
        switch (roomType)
        {
            case LevelGenerator.RoomTypes.RedSpawn:
                Instantiate(LevelGenerator.staticKeys[0], gameObject.transform);
                break;
            case LevelGenerator.RoomTypes.YellowSpawn:
                Instantiate(LevelGenerator.staticKeys[1], gameObject.transform);
                break;
            case LevelGenerator.RoomTypes.GreenSpawn:
                Instantiate(LevelGenerator.staticKeys[2], gameObject.transform);
                break;
            case LevelGenerator.RoomTypes.BlueSpawn:
                Instantiate(LevelGenerator.staticKeys[3], gameObject.transform);
                break;
        }

        // Spawn a power up randomly
        int randomNumber = UnityEngine.Random.Range(0, 3);
        if (randomNumber == 0)
        {
            randomNumber = UnityEngine.Random.Range(0, LevelGenerator.staticPowerups.Length);
            Instantiate(LevelGenerator.staticPowerups[randomNumber], gameObject.transform);
        }
    }

    virtual protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("PlayerRoomCollider")) return;

        if (!visited)
        {
            visited = true;
            MapScript.NewRoomVisited(pos, roomType);
        }
        MapScript.roomDictionary[PlayerController.currentRoom].GetComponent<Image>().color = inactiveRoomColor;
        MapScript.roomDictionary[pos].GetComponent<Image>().color = activeRoomColor;
        PlayerController.currentRoom = pos;

        if (!isStartingRoom && !spawnedEnemies)
        {
            LockRoom(); // Lock the room and spawn enemies and health collectibles
        }
    }
}
