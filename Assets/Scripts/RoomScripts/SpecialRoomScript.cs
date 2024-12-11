using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialRoomScript : RoomScript
{
    public GameObject note;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        if (roomToLeft) { leftDoor.SetActive(true); }
        if (roomToRight) { rightDoor.SetActive(true); }
        if (roomToTop) { topDoor.SetActive(true); }
        if (roomToBottom) { bottomDoor.SetActive(true); }

        leftDoor.GetComponent<SpecialRoomDoorScript>().roomType = roomType;
        rightDoor.GetComponent<SpecialRoomDoorScript>().roomType = roomType;
        topDoor.GetComponent<SpecialRoomDoorScript>().roomType = roomType;
        bottomDoor.GetComponent<SpecialRoomDoorScript>().roomType = roomType;
    }

    override protected void LockRoom()
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
    }

    override public void UnlockRoom()
    {
        leftDoor.SetActive(false);
        rightDoor.SetActive(false);
        topDoor.SetActive(false);
        bottomDoor.SetActive(false);

        // Spawn note after winning
        Instantiate(note, gameObject.transform);
        
        // Spawn a power up randomly
        int randomNumber = UnityEngine.Random.Range(0, 3);
        if (randomNumber == 0)
        {
            randomNumber = UnityEngine.Random.Range(0, LevelGenerator.staticPowerups.Length);
            Instantiate(LevelGenerator.staticPowerups[randomNumber], gameObject.transform);
        }
    }


}
