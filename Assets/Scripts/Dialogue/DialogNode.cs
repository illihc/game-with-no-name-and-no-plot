using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;

public class DialogueNodeTest : Node
{
    public string ObjectID;
    public bool IsEntryPoint;
    public string DialogueText;
}


[System.Serializable]
public class DialogueNode 
{
    public string NodeName;
    [Space]
    public DialogueNode NextNode;
    public DialogueNode PreviousNode;
    [Space]
    public string NPCName, PlayerName;

    [Space]
    [TextArea(3, 20)]
    public string[] NPCSentences;
    [TextArea(3, 20)]
    public string[] PlayerSentences;
}

[System.Serializable]
//A Dialognode for questioning the NPC. The player can get necessary info here, if he asks the right questions. Otherwise he will
//be guided to a answer, which doesn´t get him, what he wants
public class DetectiveDialogNode : DialogueNode
{
    public DialogueNode[] WrongDecisionNodes;
}

[System.Serializable]
public class FightDialogueNode : DialogueNode
{
    public int MaxPlayerMoves;
    public int CurrentPlayerMoves;

    public int CurrentEnemyHealth;
    public int MaxEnemyHealth;

    public FightDialogResult FightResult;

    public FightDialogResult CalculateWinner()
    {
        if (CurrentEnemyHealth > 0)
            return FightDialogResult.Defeat;
        else
            return FightDialogResult.Victory;
    }
}

[System.Serializable]
//A Dialognode, which unlockes a new option - in dialog and in the world (for example a key can be given through this)
public class InfoDialogNode : DialogueNode
{
    public GameObject Key;

    public void ActivateKey()
    {
        Key.SetActive(true);
    }
}

public enum FightDialogResult
{
    Victory,
    Defeat,
}
