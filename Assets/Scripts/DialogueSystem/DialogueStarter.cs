using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueStarter : MonoBehaviour
{
    public DialogueContainer Dialogue;
    public VisualDialogueManager VisualManager;
    DialogueManager DialogueManager;
    private bool DialogueIsActive;

    private void Awake()
    {
        DialogueManager = new DialogueManager();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(Input.GetKeyDown(KeyCode.I) && !DialogueIsActive)
        {
            DialogueManager.StartDialogue(Dialogue, VisualManager);
            DialogueIsActive = true;
        }

    }


    //Debugging
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !DialogueIsActive)
        {
            DialogueManager.StartDialogue(Dialogue, VisualManager);
            DialogueIsActive = true;
        }
        else if(Input.GetKeyDown(KeyCode.I) && DialogueIsActive)
        {
            DialogueManager.LoadNextDialogueStage();
        }
    }

    
}
