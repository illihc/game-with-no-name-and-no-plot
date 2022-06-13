using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FightManager : MonoBehaviour
{
    public VisualDialogueManager VisualManager;
    Queue<NodeDataHolder> AllFightNodes;
    public float NPCHealth;
    public PlayerHealth Playerhealth;
    public GameObject FightResultCanvas;
    private NodeDataHolder CurrentFightNode;
    private FightResult Fightresult;

    //For Debugging (will be removed later and given to this script by the FightDialogueStarter)
    public int NPCDamage;

    public void StartFightDialogue(Queue<NodeDataHolder> _AllFightNodes)
    {
        AllFightNodes = _AllFightNodes;

        //Display the Fight-Canvas
        VisualManager.LoadFightVisuals();

        //Activate the fighting system
        FightNextRound();

        //Set the NPCHealth
        VisualManager.MaximizeNPCHealth(NPCHealth);
    }

    public void FightNextRound()
    {
        Playerhealth.TakeDamage(NPCDamage);

        //Check if Queue is empty,
        if (AllFightNodes.Count == 0)
        {
            EndFight();
            return;
        }

        CurrentFightNode = AllFightNodes.Dequeue();
        VisualManager.DisplayNextFightRound(CurrentFightNode.DialogueText); //Just try to debug.log the deque 
    }

    public void PlayerDealsDamage(float _Aggresion, float _Threat, float _Defense)
    {
        //Do some math calculation in which you use all the players answer values and the values the enemy NPC has.
        //The NPC values can be given by the FightDialogueStarter to the Dialoguemanager and then to the Fightmanager
        //For now, only aggression will be used and no NPC values will be used to make testing easier (and because I´m lazy)
        NPCHealth -= _Aggresion;
        VisualManager.SetNPCHealthGFX(NPCHealth);

        FightNextRound();
    }

    private void EndFight()
    {
        //Check if Player, or NPC has more CurrentHealth. Then Pass this information to the VisualManager to display it.
        if (Playerhealth.CurrentPlayerhealth > NPCHealth)
            Fightresult = FightResult.Victory;
        else
            Fightresult = FightResult.Defeat;

        StartCoroutine(DisplayFightResult());

        //Enable ingame mechanics, like moving

        //Deactivate the FightScreen after a while
        VisualManager.UnlaodFightVisuals();
    }

    private IEnumerator DisplayFightResult()
    {
        FightResultCanvas.SetActive(true);
        FightResultCanvas.GetComponentInChildren<TMP_Text>().text = Fightresult.ToString();
        yield return new WaitForSeconds(2f);
        FightResultCanvas.SetActive(false);
    }
}

public enum FightResult
{
    FightIsRunning,
    Victory,
    Defeat
}
