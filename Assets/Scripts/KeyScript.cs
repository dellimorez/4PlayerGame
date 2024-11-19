using PlayerScript;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public int keyType;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) { return; }

        PlayerController.keysCollected[keyType] = true;
        KeyUIScript.obtainedKey(keyType);
        Destroy(gameObject);
    }
}
