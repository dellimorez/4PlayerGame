using PlayerScript;
using UnityEngine;

public class BossRoomDoorScript : MonoBehaviour
{
    private bool opened;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (opened) return;
        if(collision.gameObject.CompareTag("Player"))
        {
            foreach (var key in PlayerController.keysCollected) 
            {
                if (key == false) return;
            }

            gameObject.SetActive(false);
            opened = true;
        }
    }
}
