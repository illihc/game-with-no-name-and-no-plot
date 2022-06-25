using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueStarter : MonoBehaviour
{
    public DialogueContainer Dialogue;
    public VisualDialogueManager VisualManager;
    public DialogueManager DialogueManager;
    public bool DialogueIsActive = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        CheckForDialogueStart();
    }  

    protected virtual void CheckForDialogueStart()
    {
        if (Input.GetKeyDown(KeyCode.I) && !DialogueIsActive)
        {
            DialogueManager.StartDialogue(Dialogue, VisualManager, this);
            DialogueIsActive = true;

            Debug.Log("Started Dialogue");
        }
    }
}
