using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New FightSentence", menuName = "Sentence/Fight")]
public class PlayerFightSentence : ScriptableObject
{
    public string Sentence;

    public float Defensive;
    public float Aggressive;
    public float Threatening;

    public FightSentenceType SentenceType;
}

public enum FightSentenceType
{
    Defensive,
    Aggresive,
    Threatening,
}
