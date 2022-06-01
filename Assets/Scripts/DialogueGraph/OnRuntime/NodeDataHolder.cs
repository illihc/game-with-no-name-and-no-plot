using System;
using System.Collections.Generic;
using UnityEngine;


//Diese Klasse ist NUR für die Speichrung der Daten in das ScriptableObject verantwortlich
//Die Klasse, die in den aktiven Prozess eingebunden ist: DialogueNode
[System.Serializable]
public class NodeDataHolder
{
    //For the GraphView
    public string Guid;
    public Vector2 GraphPosition;
    public bool IsEntryPoint;

    //For the gameplay
    public string[] PlayerAnswers;
    public bool IsFightDialogue;
    public SentenceType[] Sentencetypes;//Can be null (None), if the DialogueType is a Talk-Dialogue
    public string DialogueText;
    public int PortNumber;
}


public enum SentenceType
{
    None = 0,
    Threatening = 1,
    Understanding = 2,
    Neutral = 3,
    Defensiv = 4,
    Confronting = 5
}