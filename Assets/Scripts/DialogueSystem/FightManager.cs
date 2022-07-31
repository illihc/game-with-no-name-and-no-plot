using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FightManager : MonoBehaviour
{
    public VisualDialogueManager VisualManager;
    Queue<NodeDataHolder> AllFightNodes;
    public PlayerHealth Playerhealth;
    private NodeDataHolder CurrentFightNode;
    private FightResult Fightresult;
    private PossibleActionDialogueStarter DialogueFightStarter;

    public float TimeBetweenAttacks = 1.5f;

    public void StartFightDialogue(Queue<NodeDataHolder> _AllFightNodes, PossibleActionDialogueStarter _Dialoguestarter)
    {
        AllFightNodes = _AllFightNodes;
        DialogueFightStarter = _Dialoguestarter;

        //Display the Fight-Canvas
        VisualManager.LoadFightVisuals();
        VisualManager.DisplayFightSentences();

        //Activate the fighting system
        FightNextRound();

        //Set the NPCHealth and Playerhealth
        VisualManager.MaximizeNPCHealth(DialogueFightStarter.NPCHealth);
        VisualManager.MaximizePlayerHealth(Playerhealth.MaxPlayerHealth);
    }

    public void FightNextRound()
    {
        //Let the Player take damage and update their Healthbar
        Playerhealth.TakeDamage(System.Convert.ToInt32(DialogueFightStarter.AggressionDamage));
        VisualManager.SetPlayerHealth(Playerhealth.CurrentPlayerhealth);

        //Check if Queue is empty,
        if (AllFightNodes.Count == 0)
        {
            EndFight();
            return;
        }

        CurrentFightNode = AllFightNodes.Dequeue();
        VisualManager.DisplayNextFightRound(CurrentFightNode.DialogueText);

        //Allow the player to answer again
        VisualManager.UsePlayerAnswerCoverUp(Activating: false);
    }

    public IEnumerator PlayerDealsDamage(float _Aggresion, float _Threat, float _Defense)
    {
        //Do some math calculation in which you use all the players answer values and the values the enemy NPC has.
        //The NPC values can be given by the FightDialogueStarter to the Dialoguemanager and then to the Fightmanager
        //For now, only aggression will be used and no NPC values will be used to make testing easier (and because I´m lazy)
        DialogueFightStarter.NPCHealth -= _Aggresion;
        VisualManager.SetNPCHealthGFX(DialogueFightStarter.NPCHealth);

        //deactivate the option to answer to prevent spamming the button to win
        VisualManager.UsePlayerAnswerCoverUp(Activating: true);
        yield return new WaitForSeconds(TimeBetweenAttacks);
        FightNextRound();
    }

    private void EndFight()
    {
        //Check if Player, or NPC has more CurrentHealth. Then Pass this information to the VisualManager to display it.
        if (Playerhealth.CurrentPlayerhealth > DialogueFightStarter.NPCHealth)
            Fightresult = FightResult.Victory;
        else
            Fightresult = FightResult.Defeat;

        StartCoroutine(VisualManager.DisplayConflictResult(Fightresult.ToString()));

        //Enable ingame mechanics, like moving
        DialogueFightStarter.DialogueIsActive = false;

        //Deactivate the FightScreen
        VisualManager.UsePlayerAnswerCoverUp(Activating: false);
        VisualManager.UnlaodFightVisuals();
        VisualManager.UnloadFightSentences();
    }
}

public enum FightResult
{
    FightIsRunning,
    Victory,
    Defeat
}
