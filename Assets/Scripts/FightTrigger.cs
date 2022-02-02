using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightTrigger : MonoBehaviour
{
    public FightDialogueNode Fightdialogue;
    [SerializeField] private DialogueManager Dialoguemanager;

    private void Awake()
    {
        Dialoguemanager.OnFightEnded += EndFight;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Dialoguemanager.StartFightDialogue(Fightdialogue);
        }
    }

    private void EndFight()
    {
        if(Fightdialogue.CurrentEnemyHealth <= 0)
            transform.parent.gameObject.SetActive(false);
    }
}
