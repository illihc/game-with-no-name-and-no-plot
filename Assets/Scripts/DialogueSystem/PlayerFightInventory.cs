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
    public FightSentenceType SentenceType;
}

public struct PlayerDetectiveSentence
{
    public float ThreateningValue, UnderstandingValue, NeutralValue, ConfrontingValue;
    public string Sentence;
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
