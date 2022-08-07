using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueStarter : Interactable
{
    public DialogueContainer Dialogue;
    public VisualDialogueManager VisualManager;
    public DialogueManager DialogueManager;
    public bool DialogueIsActive = false;

    public override void StartAction()
    {
        DialogueManager.StartDialogue(Dialogue, VisualManager, this);
        DialogueIsActive = true;
    }
    public override void EndAction() { }
}
