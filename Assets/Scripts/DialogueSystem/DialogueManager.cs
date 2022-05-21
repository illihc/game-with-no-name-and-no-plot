using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager
{
    [SerializeField] DialogueContainer[] AllDialogueContainers;
    private DialogueContainer DialogueData;
    private VisualDialogueManager VisualManager;
    private NodeDataHolder CurrentNode;

    public void StartDialogue(DialogueContainer _DialogueData, VisualDialogueManager _VisualManager) 
    {
        DialogueData = _DialogueData;
        VisualManager = _VisualManager;

        //Disable other game-functionality

        //Let the visual manager load in the graphics
        VisualManager.LoadDialogueVisuals();

        //Load the fitting dialogue
        LoadFirstDialogueStage();

        //Check if there are choices of other dialogues, which have to be payes attention to
            //Tell the Diloguecontainer about this
            //Let the visual manager change the graphics according to that
    }

    private void LoadFirstDialogueStage()
    {
        CurrentNode = FindStartNode();
        LoadNextDialogueStage();
    }

    private NodeDataHolder FindStartNode()
    {
        //Find the first node, display and save it
        foreach (NodeDataHolder node in DialogueData.NodesData)
        {
            if (node.IsEntryPoint)
            {
                return node;
            }
        }

        Debug.LogError("No StartNode found in this DialogueContainer");
        return null;
    }

    public void LoadNextDialogueStage(int _OutputPortNumber = 1)
    {
        CurrentNode = FindNextNode(_OutputPortNumber);

        //Display the next node, if there is any
        if (CurrentNode != null)
            VisualManager.DisplayNode(CurrentNode);
        //End the dialogue if there isn´t
        else
            EndDialogue();
    }

    private NodeDataHolder FindNextNode(int _OutputPortNumber)
    {
        foreach (EdgesDataHolder edge in DialogueData.EdgesData)
        {
            //Find any edge, which was outputed by the CurrentNode
            if (edge.BaseNodeGuid == CurrentNode.Guid)
            {
                Debug.Log("Found a edge, which Basenode == Currentnode");

                //Find the fitting node to the edge NodeGuid
                foreach (NodeDataHolder node in DialogueData.NodesData)
                {
                    Debug.Log("Found nodes in the Scrpitableobject´s DialogueData");
                    if (node.Guid == edge.TargetNodeGuid && node.PortNumber == _OutputPortNumber)
                    {
                        return node;
                    }
                }
            }
        }

        EndDialogue();
        return null;
    }

    public void EndDialogue()
    {
        //check if there were choices made
        //Save the choices

        //Let the visual manager unload the graphics
        VisualManager.UnloadDialogueVisuals();
        Debug.Log("Ended dialogue");

        //enable other game-functionality
    }


}
