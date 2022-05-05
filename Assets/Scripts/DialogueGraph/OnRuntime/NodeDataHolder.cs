using System;
using System.Collections.Generic;
using UnityEngine;


//Diese Klasse ist NUR für die Speichrung der Daten in das ScriptableObject verantwortlich
//Die Klasse, die in den aktiven Prozess eingebunden ist: DialogueNode
[System.Serializable]
public class NodeDataHolder
{
    public string Guid;
    public string DialogueText;
    public Vector2 GraphPosition;
    public bool IsEntryPoint;
    public int PortNumber;
}



