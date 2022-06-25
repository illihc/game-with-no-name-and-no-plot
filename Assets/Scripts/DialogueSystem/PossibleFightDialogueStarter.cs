using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossibleFightDialogueStarter : DialogueStarter
{
    public float NPCHealth;
    public float AggressionSensitivity, ThreatSensitivity;
    public float AggressionDamage, ThreatDamage;

    protected override void CheckForDialogueStart()
    {
        if (Input.GetKeyDown(KeyCode.I) && !DialogueIsActive)
        {
            DialogueManager.StartDialogue(Dialogue, VisualManager, this);
            DialogueIsActive = true;

            Debug.Log("Started Fight Dialogue");
        }
    }
}
