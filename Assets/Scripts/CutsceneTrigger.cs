using UnityEngine;
using UnityEngine.Playables;

public class CutsceneTrigger : MonoBehaviour
{
    public PlayableDirector timeline;  // Assign Timeline asset here

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCutscene();
            gameObject.SetActive(false);  // Disable trigger after activation
        }
    }

    private void StartCutscene()
    {
        timeline.Play();  // Start Timeline animation
    }
}
