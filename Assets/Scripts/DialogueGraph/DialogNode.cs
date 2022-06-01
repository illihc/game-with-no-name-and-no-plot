using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;

public class DialogueNode : Node
{
    public string ObjectID;
    public bool IsEntryPoint;
    public string DialogueText;
    //The number of the port of the node, which outputs to this node. So if node a had a port 1-5, node b would have the InputPortNumber 1, if
    //it´s connected to port 1 of node a
    public int InputPortNumber;
    public bool IsFightNode;
    public SentenceType[] Sentencetypes;    //The first element of this array, is for the first Answer
}