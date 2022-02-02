using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueNode dialogue;
    [SerializeField] private DialogueManager Dialoguemanager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Dialoguemanager.StartDialogue(dialogue);
        }
    }
}
