using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class GraphSave
{
    private DialogueGraphView GraphView;
    private DialogueContainer DialogueSaveContainer;
    List<Edge> AllEdges;

    List<DialogueNodeTest> Nodes => GraphView.nodes.ToList().Cast<DialogueNodeTest>().ToList();

    public static GraphSave GetInstance(DialogueGraphView _Graphview)
    {
        return new GraphSave { GraphView = _Graphview };
    }

    public void SaveGraph(string _FileName)
    {
        //If there is no Scriptableobject with the given Name, create a new one
        if (Resources.Load<DialogueContainer>(_FileName) == null)
            DialogueSaveContainer = new DialogueContainer();
        else
            DialogueSaveContainer = Resources.Load<DialogueContainer>(_FileName);


        SaveEdges();
        SaveNodes();

        //If there is no "Ressources folder - in which the scritable object with all the data is saved - create one
        if (!AssetDatabase.IsValidFolder("Assets/Resources"))
            AssetDatabase.CreateFolder("Assets", "Resources");

        if(Resources.Load<DialogueContainer>(_FileName) == null)
            AssetDatabase.CreateAsset(DialogueSaveContainer, $"Assets/Resources/{_FileName}.asset");

        EditorUtility.SetDirty(Resources.Load<DialogueContainer>(_FileName));

        AssetDatabase.SaveAssets();
    }

    private void SaveNodes()
    {
        //Deleting all previous saved nodes, so there are no doublettes
        DialogueSaveContainer.NodesData.Clear();

        //Saving the Guid, the text, and the position of all nodes, except the startnode in de DialoguesaveContainer in the NodesData-List
        foreach (var _DialogueNode in Nodes)
        {
            DialogueSaveContainer.NodesData.Add(new NodeDataHolder
            {
                Guid = _DialogueNode.ObjectID,
                DialogueText = _DialogueNode.DialogueText,
                GraphPosition = _DialogueNode.GetPosition().position,
                IsEntryPoint = _DialogueNode.IsEntryPoint,
                PortNumber = _DialogueNode.InputPortNumber,
            });
        }
    }

    private void SaveEdges()
    {
        //Deleting all previous saved Edges, so there are no doublettes
        DialogueSaveContainer.EdgesData.Clear();

        //Getting all edges, which are currently in the graph
        AllEdges = GraphView.edges.ToList();

        //If there are no edges in the graph, return
        if (!AllEdges.Any()) { return; }

        //Iterating over all edges to:
        for (int i = 0; i < AllEdges.Count(); i++)
        {
            //Getting the output and input node of the edge
            DialogueNodeTest OutputNode = AllEdges[i].output.node as DialogueNodeTest;
            DialogueNodeTest InputNode = AllEdges[i].input.node as DialogueNodeTest;

            //Filling the list of edges data of the dialogcontainer with the guid of in- and outputport and the name of the outputport
            DialogueSaveContainer.EdgesData.Add(new EdgesDataHolder
            {
                BaseNodeGuid = OutputNode.ObjectID,
                PortName = AllEdges[i].output.portName,
                TargetNodeGuid = InputNode.ObjectID,
            });

            //Save here the InputPortNumber, so it can be saved in the SaveNodes-function, which is called afterwards
            InputNode.InputPortNumber = FindInputPortNumber(AllEdges[i], OutputNode);
        }
    }

    private int FindInputPortNumber(Edge edge, DialogueNodeTest OutputNode)
    {
        Debug.Log("Childcount of outputcontainer is: " + OutputNode.outputContainer.childCount);
        //Finding the number of the outputport int the outputcontainer of the OutputNode
        for (int i = 0; i < OutputNode.outputContainer.childCount; i++)
        {
            Debug.Log(OutputNode.outputContainer[i]);

            //The edge is not the same as the outputchannel sitting in OutputNode.outputContainer[i]
            if (OutputNode.outputContainer[i] == edge.output)
            {
                Debug.Log("Node is: " + OutputNode.name + "Input port number is: " + i);
                return i + 1;
            }


        }

        Debug.LogError("Couldn´t find a fitting edge for outputnode: " + OutputNode.DialogueText);
        return 10000;
    }

    public void LoadGraph(string _FileName)
    {
        DialogueSaveContainer = Resources.Load<DialogueContainer>(_FileName);
        Debug.Log(Resources.Load<DialogueContainer>(_FileName));

        ClearGraph();
        LoadNodes();
        LoadEdges();
    }

    private void ClearGraph()
    {
        AllEdges = GraphView.edges.ToList();

        //Remove all nodes from the Graph
        foreach (DialogueNodeTest n in Nodes)
        {
            GraphView.RemoveElement(n);
        }

        //Remove all edges from the graph
        foreach (Edge e in AllEdges)
        {
            GraphView.RemoveElement(e);
        }
    }

    private void LoadNodes()
    {
        foreach (NodeDataHolder nodedata in DialogueSaveContainer.NodesData)
        {
            //Creating a new node, setting all its parameters to the saved parameters of nodedata and adding it to the graph. Detect if it is the entrypoint an then just make an outputport
            DialogueNodeTest _node;

            if (nodedata.IsEntryPoint)
                _node = GraphView.CreateDialogueNode(nodedata.DialogueText, true);
            else
                _node = GraphView.CreateDialogueNode(nodedata.DialogueText);

            _node.ObjectID = nodedata.Guid;
            _node.IsEntryPoint = nodedata.IsEntryPoint;
            GraphView.AddElement(_node);

            //Getting all the ports, the node had
            var _nodePorts = DialogueSaveContainer.EdgesData.Where(x => x.BaseNodeGuid == nodedata.Guid).ToList();
            _nodePorts.ForEach(port => GraphView.AddOutputPort(_node, port.PortName));

            //Setting the position of the node
            _node.SetPosition(new Rect(nodedata.GraphPosition, new Vector2(150, 200)));
        }

    }

    private void LoadEdges()
    {
        for (int i = 0; i < Nodes.Count; i++)
        {
            //Finding all edges, which basenodes are the same as nodes[i], which means they are connected to the output of nodes[i]
            //List<EdgesDataHolder> Connections = DialogueSaveContainer.EdgesData.Where(n => n.BaseNodeGuid == Nodes[i].ObjectID).ToList();
            List<EdgesDataHolder> Connections = new List<EdgesDataHolder>();

             foreach (EdgesDataHolder EdgeData in DialogueSaveContainer.EdgesData)
             {
                if (EdgeData.BaseNodeGuid == Nodes[i].ObjectID)
                    Connections.Add(EdgeData);
             }

            //Iterating over all found edges, who are connected to the output of nodes[i] to:
            for (int j = 0; j < Connections.Count(); j++)
            {
                //Getting the Guid and the Node to which input port the edge is connected (the outputnode is Nodes[i]
                string TargetNode = Connections[j].TargetNodeGuid;
                DialogueNodeTest InputNode = Nodes.First(n => n.ObjectID == TargetNode);

                Debug.Log(InputNode.inputContainer[0]); //Null argument exception for the inputcontainer
                Debug.Log(Nodes[i].outputContainer[j]);

                ConnectNodes((Port)InputNode.inputContainer[0], Nodes[i].outputContainer[j].Q<Port>());
            }
        }
    }

    private void ConnectNodes(Port _Input, Port _Output)
    {
        Edge edge = new Edge()
        {
            input = _Input,
            output = _Output
        };

        edge?.input.Connect(edge);
        edge?.output.Connect(edge);
        GraphView.Add(edge);

    }
}
