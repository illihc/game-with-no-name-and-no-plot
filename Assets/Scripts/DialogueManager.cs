using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void EndedFight();
public class DialogueManager : MonoBehaviour
{
    private Queue<string> NPCSentences;

    //All texfields
    [SerializeField] private Text NPCNameTextDialogue;
    [SerializeField] private Text PlayerNameTextDialogue;
    [SerializeField] private Text NPCSentenceTextDialogue;

    [SerializeField] private Text NPCNameTextFight;
    [SerializeField] private Text PlayerNameTextFight;
    [SerializeField] private Text NPCSentenceTextFight;

    [SerializeField] private GameObject DialogBackground;

    //All things for fighting
    [SerializeField] private GameObject FightBackground;
    private FightDialogue Fightdialogue;
    public event EndedFight OnFightEnded;


    private void Awake()
    {
        NPCSentences = new Queue<string>();
        DialogBackground.SetActive(false);
        FightBackground.SetActive(false);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        //Activating the dialogview
        DialogBackground.SetActive(true);

        NPCNameTextDialogue.text = dialogue.NPCName;
        PlayerNameTextDialogue.text = dialogue.PlayerName;

        //Clearing all loaded sentences
        NPCSentences.Clear();

        //Loading new sentences
        for(int i = 0; i < dialogue.Sentences.Length; i++)
        {
            NPCSentences.Enqueue(dialogue.Sentences[i]);
        }

        DisplayNextSentences();
    }

    public void StartFightDialogue(FightDialogue _Fightdialogue)
    {
        //Initialising the Fightdialogueobject
        Fightdialogue = _Fightdialogue;

        //Activating the fightview
        FightBackground.SetActive(true);
        NPCNameTextFight.text = Fightdialogue.NPCName;
        PlayerNameTextFight.text = Fightdialogue.PlayerName;

        //Clearing all loaded sentences
        NPCSentences.Clear();

        //Loading new sentences
        for (int i = 0; i < Fightdialogue.Sentences.Length; i++)
        {
            NPCSentences.Enqueue(Fightdialogue.Sentences[i]);
        }

        //Starting a new fight
        Fightdialogue.CurrentPlayerMoves = -1;
        Fightdialogue.CurrentEnemyHealth = Fightdialogue.MaxEnemyHealth;
        FightNextRound(0);
    }

    public void FightNextRound(int Damage)
    {
        //Adding one round
        Fightdialogue.CurrentPlayerMoves++;
        //Doing player damage
        Fightdialogue.CurrentEnemyHealth -= Damage;


        //Check if fight is over
        if(Fightdialogue.CurrentPlayerMoves == Fightdialogue.MaxPlayerMoves || Fightdialogue.CurrentEnemyHealth <= 0)
        {
            EndFight();
            return;
        }

        //Displaying the next NPC sentence
        NPCSentenceTextFight.text = NPCSentences.Dequeue();
    }

    public void EndFight()
    {
        Debug.Log("Fight is over with " + Fightdialogue.CalculateWinner());
        OnFightEnded();
        FightBackground.SetActive(false);
    }

    public void DisplayNextSentences()
    {
        //Check if everything is said
        if(NPCSentences.Count == 0)
        {
            EndDialog();
            return;
        }

        //Displaying the next NPC sentence
        NPCSentenceTextDialogue.text = NPCSentences.Dequeue();
    }

    public void EndDialog()
    {
        DialogBackground.SetActive(false);
        Debug.Log("Ended Dialog");
    }
}
