using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource source;

    private void Awake()
    {
        // Ensure there's only one SoundManager instance
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return; // Prevent further execution
        }

        // Attach AudioSource component if not found
        source = GetComponent<AudioSource>();
        if (source == null)
        {
            source = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlaySound(AudioClip _sound, float volume)
    {
        source.PlayOneShot(_sound, volume); // Play with specified volume
    }
}
