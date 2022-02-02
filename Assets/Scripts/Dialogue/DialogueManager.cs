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
    [Space]
    [SerializeField] private GameObject FightBackground;
    private FightDialogueNode Fightdialogue;
    public event EndedFight OnFightEnded;

    //All references to other scripts
    [Space]
    [SerializeField] private PlayerInput Playerinput;

    private void Awake()
    {
        NPCSentences = new Queue<string>();
        DialogBackground.SetActive(false);
        FightBackground.SetActive(false);
    }

    public void StartDialogue(DialogueNode dialogue)
    {
        //Deactivate Player movement
        Playerinput.CanMove = false;

        //Activating the dialogview
        DialogBackground.SetActive(true);

        NPCNameTextDialogue.text = dialogue.NPCName;
        PlayerNameTextDialogue.text = dialogue.PlayerName;

        //Clearing all loaded sentences
        NPCSentences.Clear();

        //Loading new sentences
        for(int i = 0; i < dialogue.NPCSentences.Length; i++)
        {
            NPCSentences.Enqueue(dialogue.NPCSentences[i]);
        }

        DisplayNextSentences();
    }

    public void StartFightDialogue(FightDialogueNode _Fightdialogue)
    {
        //Initialising the Fightdialogueobject
        Fightdialogue = _Fightdialogue;

        //Deactivate Player movement
        Playerinput.CanMove = false;

        //Activating the fightview
        FightBackground.SetActive(true);
        NPCNameTextFight.text = Fightdialogue.NPCName;
        PlayerNameTextFight.text = Fightdialogue.PlayerName;

        //Clearing all loaded sentences
        NPCSentences.Clear();

        //Loading new sentences
        for (int i = 0; i < Fightdialogue.NPCSentences.Length; i++)
        {
            NPCSentences.Enqueue(Fightdialogue.NPCSentences[i]);
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
        //Activate Player movement
        Playerinput.CanMove = true;

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
        //Activate Player movement
        Playerinput.CanMove = true;

        DialogBackground.SetActive(false);
        Debug.Log("Ended Dialog");
    }
}
