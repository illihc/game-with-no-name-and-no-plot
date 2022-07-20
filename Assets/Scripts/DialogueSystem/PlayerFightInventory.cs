using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSentencesInventory", menuName = "PlayerSentencesInventory")]
public class PlayerFightInventory : ScriptableObject
{
    public PlayerFightSentence[] AllPlayerFightSentences;
    public PlayerDetectiveSentence[] AllDetectiveSentences;
}

public struct PlayerFightSentence
{
    public float DefensiveValue, AggressiveValue, ThreateningValue;
    public string Sentence;

    //Just to sort the Sentences in the VisualInventory
    public FightSentenceType SentenceType;
}

public struct PlayerDetectiveSentence
{
    public float ThreateningValue, UnderstandingValue, NeutralValue, ConfrontingValue;
    public string Sentence;

    //Just to sort the Sentences in the VisualInventory
    public DetectiveSentenceType SentenceType;
}

public enum FightSentenceType
{
    Defensive,
    Aggresive,
    Threatening
}

public enum DetectiveSentenceType
{
    ThreateningValue,
    UnderstandingValue,
    NeutralValue,
    ConfrontingValue
}
