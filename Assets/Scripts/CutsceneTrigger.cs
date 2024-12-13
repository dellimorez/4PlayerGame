using UnityEngine;
using UnityEngine.Playables;

public class CutsceneTrigger : MonoBehaviour
{
    public PlayableDirector timeline;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerRoomCollider"))
        {
            //StartCutscene();
            //gameObject.SetActive(false);  // Disable the trigger after activation
        }
    }

    private void StartCutscene()
    {
        timeline.Play();
        Time.timeScale = 1f;
        timeline.stopped += EndCutscene;  // Reactivate gameplay when done
    }

    private void EndCutscene(PlayableDirector pd)
    {
        GameManager.cutsceneActive = false;  // Re-enable enemy logic
        Time.timeScale = 1f;
    }
}
