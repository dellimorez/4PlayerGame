using PlayerScript;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossRoomDoorScript : MonoBehaviour
{
    public bool finalDoor = false;

    private bool opened;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (opened || PlayerController.inCombat) return;
        if(collision.gameObject.CompareTag("Player"))
        {
            foreach (var key in PlayerController.keysCollected) 
            {
                if (key == false) return;
            }

            gameObject.SetActive(false);
            opened = true;

            if(finalDoor)
            {
                SceneManager.LoadScene("WinScene");
            }
        }
    }
}
