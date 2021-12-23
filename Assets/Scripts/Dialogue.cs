using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string NPCName;
    public string PlayerName;

    [TextArea(3, 20)]
    public string[] Sentences;
}

[System.Serializable]
public class FightDialogue : Dialogue
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

public enum FightDialogResult
{
    Victory,
    Defeat,
}
