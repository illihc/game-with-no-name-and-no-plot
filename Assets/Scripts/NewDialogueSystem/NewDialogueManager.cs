using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDialogueManager : MonoBehaviour
{
    [SerializeField] DialogueContainer[] AllDialogueContainers;
    private DialogueContainer DialogueData;
    [SerializeField] private VisualDialogueManager VisualManager;
    private NodeDataHolder CurrentNode;

    public void StartDialogue(DialogueContainer _DialogueData) 
    {
        DialogueData = _DialogueData;

        //Disable other game-functionality

        //Let the visual manager load in the graphics

        //Load the fitting dialogue
        LoadFirstDialogueStage();

        //Check if there are choices of other dialogues, which have to be payes attention to
            //Tell the Diloguecontainer about this
            //Let the visual manager change the graphics according to that
    }

    private void LoadFirstDialogueStage()
    {
        //Find the first node, display and save it
        foreach(NodeDataHolder node in DialogueData.NodesData)
        {
            if(node.IsEntryPoint)
            {
                VisualManager.DisplayNode(node);
                CurrentNode = node;
            }
        }
    }

    public void LoadNextDialogueStage()
    {
        //THIS ONLY WORKS IF THERE IS JUST AN EDGE FOR EACH NODE! FIRST MAKE A LIST OF POSSIBLE EDGES THEN TAKE THE RIGHT ONE AND THEN DO THESE THINGS
        //I´m just to tired to do this

        //Find the next node
        foreach(EdgesDataHolder edge in DialogueData.EdgesData)
        {
            //Find the edge, which was outputed by the CurrentNode
            if(edge.BaseNodeGuid == CurrentNode.Guid)
            {
                //Find the fitting node to the edge NodeGuid
                foreach(NodeDataHolder node in DialogueData.NodesData)
                {
                    if(node.Guid == edge.BaseNodeGuid)
                    {
                        CurrentNode = node;
                    }
                }
            }
        }

        //Display the next node
        VisualManager.DisplayNode(CurrentNode);
    }

    public void EndDialogue()
    { 
        //check if there were choices made
            //Save the choices

        //Let the visual manager unload the graphics

        //enable other game-functionality
    }


}
