using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using UnityEngine.Bindings;
using UnityEngine.Scripting;


public class DialogueGraphView : GraphView
{
    public DialogueGraphView()
    { 
        //Adding the ability to drag things around
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());
        this.AddManipulator(new ContentZoomer());

        //Adding a cool Baclground and settings it�s layer to the lowest 
        GridBackground Background = new GridBackground();
        Background.StretchToParentSize();
        Insert(0, Background);

        //Adding the first node
        AddElement(GenerateEntryPoint());
    }

    public void CreateNode(string _nodename)
    {
        AddElement(CreateDialogueNode(_nodename, false, false));
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        List<Port> CompatiblePorts = new List<Port>();

        ports.ForEach(funcCall: (port) =>
        {
            if (startPort != port && startPort.node != port.node)
                CompatiblePorts.Add(port);
        });

        return CompatiblePorts;
    }

    private DialogueNode GenerateEntryPoint()
    {
        return CreateDialogueNode("start", false, false, true);
    }

    //Generating a port (a point, where one node can be connectet to another node)
    private Port GeneratePort(DialogueNode node, Direction PortDirection, Port.Capacity capacity = Port.Capacity.Single)
    {
        return node.InstantiatePort(Orientation.Horizontal, PortDirection, Port.Capacity.Single, typeof(bool));
    }

    public DialogueNode CreateDialogueNode(string _NodeName, bool _IsFightNode, bool _IsDetectiveNode, bool IsEntryNode = false)
    {
        DialogueNode _Dialoguenode = new DialogueNode
        {
            title = _NodeName,
            DialogueText = _NodeName,
            ObjectID = System.Guid.NewGuid().ToString(),
            IsFightNode = _IsFightNode,
            IsDetectiveNode = _IsDetectiveNode,
        };

        if(! IsEntryNode)
        {
            //Creating the input port
            Port InputPort = GeneratePort(_Dialoguenode, Direction.Input, Port.Capacity.Multi);
            InputPort.portName = "Input";
            _Dialoguenode.inputContainer.Add(InputPort);

            //Create a button to create new OutputPorts
            Button OutPutButton = new Button(clickEvent: () => { AddOutputPort(_Dialoguenode); });
            OutPutButton.text = "New Choice";
            _Dialoguenode.titleContainer.Add(OutPutButton);

            //Creating the textfeld, in which the story can be told
            TextField Textfield = new TextField(string.Empty);
            Textfield.RegisterValueChangedCallback(text =>
            {
                _Dialoguenode.DialogueText = text.newValue;
            });
            Textfield.SetValueWithoutNotify(_Dialoguenode.title);
            _Dialoguenode.mainContainer.Add(Textfield);

            //Creating the Title of the node
            TextField Titletext = new TextField(string.Empty);
            Textfield.RegisterValueChangedCallback(_title =>
            {
                _Dialoguenode.title = _title.newValue;
            });
            Titletext.SetValueWithoutNotify(_Dialoguenode.title);
            _Dialoguenode.titleContainer.Add(Titletext);

            //Create a button to mark the node as Fight-Node
            Toggle IsFightNodeToggle = new Toggle("IsFightNode");
            IsFightNodeToggle.SetValueWithoutNotify(_Dialoguenode.IsFightNode);
            IsFightNodeToggle.RegisterValueChangedCallback(_IsFight =>
            {
                _Dialoguenode.IsFightNode = _IsFight.newValue;
            });
            _Dialoguenode.titleContainer.Add(IsFightNodeToggle);

            //Create a button to mark the node as Detective-Node
            Toggle IsDetectiveNodeToggle = new Toggle("IsDetectiveNode");
            IsDetectiveNodeToggle.SetValueWithoutNotify(_Dialoguenode.IsDetectiveNode);
            IsDetectiveNodeToggle.RegisterValueChangedCallback(_IsDetective =>
            {
                _Dialoguenode.IsDetectiveNode = _IsDetective.newValue;
            });
            _Dialoguenode.titleContainer.Add(IsDetectiveNodeToggle);
        }
        else if(IsEntryNode)
        {
            //Adding a new created and named port to the node
            Port generatedPort = GeneratePort(_Dialoguenode, Direction.Output);
            generatedPort.portName = "Start";

            if(!_Dialoguenode.outputContainer.Contains(generatedPort))
                _Dialoguenode.outputContainer.Add(generatedPort);
        }

        _Dialoguenode.RefreshExpandedState();
        _Dialoguenode.RefreshPorts();
        _Dialoguenode.SetPosition(new Rect(Vector2.zero, new Vector2(150, 200)));

        return _Dialoguenode;
    }

    public void AddOutputPort(DialogueNode _Node, string _overwrittenPortName = "")
    {
        Port OutputPort = GeneratePort(_Node, Direction.Output);

        var portLabel = OutputPort.contentContainer.Q<Label>("type");
        OutputPort.contentContainer.Remove(portLabel);

        int OutputPortNumber = _Node.outputContainer.Query(name: "connector").ToList().Count;

        string outputPortName = string.IsNullOrEmpty(_overwrittenPortName)
            ? $"Option {OutputPortNumber + 1}"
            : _overwrittenPortName;

        //Create a textfield to name the Choice Option
        TextField textField = new TextField()
        {
            name = string.Empty,
            value = outputPortName
        };
        textField.RegisterValueChangedCallback(evt => OutputPort.portName = evt.newValue);
        OutputPort.contentContainer.Add(new Label("  "));
        OutputPort.contentContainer.Add(textField);



        //Certate a button to delete the choice
        Button deletebutton = new Button(clickEvent: () => RemovePort(_Node, OutputPort))
        {
            text = "Delete"
        };

        OutputPort.contentContainer.Add(deletebutton);

        OutputPort.portName = outputPortName;
        _Node.outputContainer.Add(OutputPort);
        _Node.RefreshPorts();
        _Node.RefreshExpandedState();
    }

    private void RemovePort(DialogueNode _Node, Port _port)
    {
        //Getting possible edges of the port
        var NodeEdges = edges.ToList().Where(e => e.output.portName == _port.portName && e.output.node == _port.node);

        //Disconnect and remove the found edges of the port, which we want to remove (so we have no edges flying around with no port)
        if (NodeEdges.Any())
        {
            Edge edge = NodeEdges.First();
            edge.input.Disconnect(edge);
            RemoveElement(NodeEdges.First());
        }

        //remove the port and Refresh the Node-Look
        _Node.outputContainer.Remove(_port);
        _Node.RefreshPorts();
        _Node.RefreshExpandedState();
    }

}
