using UnityEngine;
using UnityEngine.Playables;  // Use this if using Timeline

public class CutsceneManager : MonoBehaviour
{
    public PlayableDirector timeline;  // Assign your Timeline asset here

    public void StartCutscene()
    {
        timeline.Play();  // Start the Timeline animation
    }
}
