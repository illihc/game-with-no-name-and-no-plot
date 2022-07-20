using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossibleActionDialogueStarter : DialogueStarter
{
    public bool WasEarlierConvinced;

    [Header("Fighting Values")]
    public float NPCHealth;
    public float AggressionSensitivity, ThreatSensitivity;
    public float AggressionDamage, ThreatDamage;

    [Space(10)]
    [Header("DetectiveValues")]
    public float ThreateningSensitivity;
    public float NeutralSensitivity;
    public float ConfrontingSensitivity;

    public float Anxietydecrease;
    public float AnxietyMaxValue;
    public float AnxietyStartValue;
    public float AnxietyCurrentValue;
    public float AnxietyCorrectValue;

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
