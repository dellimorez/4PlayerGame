using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableNoteTrigger : MonoBehaviour, IPointerClickHandler
{
    public int noteIndex;  // Assign this in the Inspector

    public void OnPointerClick(PointerEventData eventData)
    {
        NoteUIScript.obtainedNote(noteIndex);  // Call the function
    }
}
