using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueContainer : ScriptableObject
{
    public List<NodeDataHolder> NodesData = new List<NodeDataHolder>();
    public List<EdgesDataHolder> EdgesData = new List<EdgesDataHolder>();
}
