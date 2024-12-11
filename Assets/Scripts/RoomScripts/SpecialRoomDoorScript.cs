using PlayerScript;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpecialRoomDoorScript : MonoBehaviour
{
    public LevelGenerator.RoomTypes roomType;

    private bool opened;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (opened || PlayerController.inCombat) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            int keyIndex = (int)roomType - (int)LevelGenerator.RoomTypes.Red;
            if (!PlayerController.keysCollected[keyIndex]) { return; }

            gameObject.SetActive(false);
        }
    }
}
