using PlayerScript;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteUIScript : MonoBehaviour
{
    public Image[] noteUIImages;
    public NoteUIVisualScript[] visualNoteImages;

    public static Image[] staticNoteUIImages;
    public static NoteUIVisualScript[] staticVisualNoteImages;

    private void Start()
    {
        staticNoteUIImages = new Image[noteUIImages.Length];
        for (int i = 0; i < noteUIImages.Length; i++)
        {
            noteUIImages[i].gameObject.SetActive(false);
            staticNoteUIImages[i] = noteUIImages[i];
        }

        staticVisualNoteImages = new NoteUIVisualScript[visualNoteImages.Length];
        for (int i = 0;i < visualNoteImages.Length; i++)
        {
            visualNoteImages[i].noteUI.SetActive(false);
            staticVisualNoteImages[i] = visualNoteImages[i];
        }
    }


    public static void obtainedNote(int i)
    {
        staticNoteUIImages[i].gameObject.SetActive(true);
        staticVisualNoteImages[i].view();
    }
}
