using PlayerScript;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossRoomScript : RoomScript
{
    // Start is called before the first frame update
    override protected void Start()
    {
        leftDoor.SetActive(true);
        rightDoor.SetActive(true);
    }

    override protected void LockRoom()
    {
        spawnedEnemies = true;

        if (roomToLeft) { leftDoor.SetActive(true); }
        if (roomToRight) { rightDoor.SetActive(true); }

        GameObject spawner = Instantiate(EnemySpawner);
        EnemySpawnerScript script = spawner.GetComponent<EnemySpawnerScript>();

        script.rs = this;
    }

    // Only happens after the player beats the room
    override public void UnlockRoom()
    {
        leftDoor.SetActive(false);
        rightDoor.SetActive(false);

        // TODO: End game
    }

    override protected void OnTriggerEnter2D(Collider2D collision)
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

        if (!spawnedEnemies)
        {
            LockRoom();
        }
    }
}
