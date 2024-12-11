using PlayerScript;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteScript : MonoBehaviour
{
    public int noteType;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerController.notesCollected[noteType] = true;
            NoteUIScript.obtainedNote(noteType);
            Destroy(gameObject);
        }
    }
}
