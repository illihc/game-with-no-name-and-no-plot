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

        SaveEdges();
        SaveNodes();

        //If there is no "Ressources folder - in which the scritable object with all the data is saved - create one
        if (!AssetDatabase.IsValidFolder("Assets/Resources"))
            AssetDatabase.CreateFolder("Assets", "Resources");

        AssetDatabase.CreateAsset(DialogueSaveContainer, $"Assets/Resources/{_FileName}.asset");
        AssetDatabase.SaveAssets();
    }

    private void SaveNodes()
    {
        //Getting all the nodes in the Graph, as Dialognodes
        //Nodes =  GraphView.nodes.ToList().Cast<DialogueNodeTest>().ToList();

        //Saving the Guid, the text, and the position of all nodes, except the startnode in de DialoguesaveContainer in the NodesData-List
        foreach (var _DialogueNode in Nodes)
        {
            DialogueSaveContainer.NodesData.Add(new NodeDataHolder
            {
                Guid = _DialogueNode.ObjectID,
                DialogueText = _DialogueNode.DialogueText,
                GraphPosition = _DialogueNode.GetPosition().position,
                IsEntryPoint = _DialogueNode.IsEntryPoint,
            });
        }
    }

    private void SaveEdges()
    {
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
        }
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
        //Nodes = GraphView.nodes.ToList().Cast<DialogueNodeTest>().ToList();
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
            var Connections = DialogueSaveContainer.EdgesData.Where(n => n.BaseNodeGuid == Nodes[i].ObjectID).ToList();

            //Iterating over all found edges, who are connected to the output of nodes[i] to:
            for (int j = 0; j < Connections.Count(); j++)
            {
                //Getting the Guid and the Node to which input port the edge is connected (the outputnode is Nodes[i]
                string InputNodeGuid = Connections[j].TargetNodeGuid;
                var InputNode = Nodes.First(n => n.ObjectID == InputNodeGuid);

                ConnectNodes(Nodes[i].outputContainer[j].Q<Port>(), (Port)InputNode.inputContainer[0]);
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
