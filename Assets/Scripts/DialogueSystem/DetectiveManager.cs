using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectiveManager : MonoBehaviour
{
    public VisualDialogueManager VisualManager;
    private Queue<NodeDataHolder> AllDetectiveNodes;
    private PossibleActionDialogueStarter DialogueStarter;
    private NodeDataHolder CurrentNode;
    public float TimeBetweenQuestions = 0.5f;

    public void StartDetectiveDialogue(Queue<NodeDataHolder> _AllDetectiveNodes, PossibleActionDialogueStarter _Dialoguestarter)
    {
        AllDetectiveNodes = _AllDetectiveNodes;
        DialogueStarter = _Dialoguestarter;

        VisualManager.LoadDetectiveVisuals();
        InvestigateNextRound();

        //Set the Max and current Anxiety Level
        VisualManager.SetMaxAnxietyMeter(DialogueStarter.AnxietyMaxValue);
        DialogueStarter.AnxietyCurrentValue = DialogueStarter.AnxietyStartValue;
        VisualManager.SetAnxietyMeter(DialogueStarter.AnxietyCurrentValue);
        VisualManager.SetAnxietyCorrectMarker(DialogueStarter.AnxietyCorrectValue, DialogueStarter.AnxietyMaxValue);
    }

    public void InvestigateNextRound()
    {
        //Let the Anxiety-Meter sink and display it
        if(DialogueStarter.AnxietyCurrentValue >= DialogueStarter.AnxietyMaxValue)
        {
            DialogueStarter.AnxietyCurrentValue = DialogueStarter.AnxietyMaxValue;
            VisualManager.SetAnxietyMeter(DialogueStarter.AnxietyCurrentValue);
        }
        else
        {
            DialogueStarter.AnxietyCurrentValue -= DialogueStarter.Anxietydecrease;
            VisualManager.SetAnxietyMeter(DialogueStarter.AnxietyCurrentValue);
        }

        //Check if Queue is empty
        if (AllDetectiveNodes.Count == 0)
        {
            EndDetection(IsDefinetlyWon: false);
            return;
        }

        CurrentNode = AllDetectiveNodes.Dequeue();
        VisualManager.DisplayNextDetectiveRound(CurrentNode.DialogueText);

        //Allow the player to answer again
        VisualManager.UsePlayerAnswerCoverUp(Activating: false);
    }

    public IEnumerator PlayerAsksQuestion(float _Threat, float _Neutral, float _Understanding, float _Confront)
    {
        ////Do some math calculation in which you use all the players answer values and the values the enemy NPC has.
        //For now, only Threatening will be used and no NPC values will be used to make testing easier (and because I´m lazy)
        DialogueStarter.AnxietyCurrentValue += _Threat;
        VisualManager.SetAnxietyMeter(DialogueStarter.AnxietyCurrentValue);


        //Deactivate the option to answer to prevent spamming the button to win
        VisualManager.UsePlayerAnswerCoverUp(Activating: true);

        yield return new WaitForSeconds(TimeBetweenQuestions);

        //Check if the anxiety meter is right filled
        if(DialogueStarter.AnxietyCurrentValue == DialogueStarter.AnxietyCorrectValue)
        {
            EndDetection(IsDefinetlyWon: true);
        }
        else
            InvestigateNextRound();
    }

    private void EndDetection(bool IsDefinetlyWon)
    {
        //Check if player has won
        if(IsDefinetlyWon || DialogueStarter.AnxietyCurrentValue == DialogueStarter.AnxietyCorrectValue)
        {
            StartCoroutine(VisualManager.DisplayConflictResult("Convinced"));
            DialogueStarter.WasEarlierConvinced = true;
        }
        else
        {
            StartCoroutine(VisualManager.DisplayConflictResult("Not convinced"));
        }

        DialogueStarter.DialogueIsActive = false;

        //Deactivate the DetectiveScreen
        VisualManager.UsePlayerAnswerCoverUp(Activating: false);
        VisualManager.UnloadDetectiveVisuals();
    }
}
