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
    public bool IsFightDialogue;
    public bool IsDetectiveDialogue;
    public string DialogueText;
    public int PortNumber;
}

