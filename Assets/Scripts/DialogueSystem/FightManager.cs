using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    public VisualDialogueManager VisualManager;
    Queue<NodeDataHolder> AllFightNodes;
    public void StartFightDialogue(Queue<NodeDataHolder> _AllFightNodes)
    {
        AllFightNodes = _AllFightNodes;

        //Display the Fight-Canvas
        VisualManager.LoadFightVisuals();

        //Activate the fighting system
        FightNextRound();
    }

    public void FightNextRound()
    {
        VisualManager.DisplayNextFightRound(AllFightNodes.Dequeue().DialogueText);
    }
}
