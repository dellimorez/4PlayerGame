using UnityEngine;
using UnityEngine.Playables;  // Use this if using Timeline

public class CutsceneManager : MonoBehaviour
{
    public PlayableDirector timeline;  // Assign your Timeline asset here

    public static PlayableDirector staticTimeline;
    private static bool played = false;

    public void Awake()
    {
        staticTimeline = timeline;
    }

    public static void StartCutscene()
    {
        if (played) return;

        staticTimeline.Play();  // Start the Timeline animation
        Time.timeScale = 0f;
        staticTimeline.stopped += EndCutscene;
        played = true;
    }

    private static void EndCutscene(PlayableDirector pd)
    {
        GameManager.cutsceneActive = false;  // Re-enable enemy logic
        Time.timeScale = 1f;
    }
}
