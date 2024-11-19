using PlayerScript;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyUIScript : MonoBehaviour
{
    public Image[] keyUIImages;

    public static Image[] staticKeyUIImages;

    private void Start()
    {
        staticKeyUIImages = new Image[keyUIImages.Length];
        for (int i = 0; i < keyUIImages.Length; i++)
        {
            keyUIImages[i].gameObject.SetActive(false);
            staticKeyUIImages[i] = keyUIImages[i];
        }
    }


    public static void obtainedKey(int i)
    {
        staticKeyUIImages[i].gameObject.SetActive(true);
    }
}
