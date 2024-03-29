using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private DialogueContainer DialogueData;
    private VisualDialogueManager VisualManager;
    private NodeDataHolder CurrentNode;
    private List<string> PlayerAnswers = new List<string>();
    DialogueStarter CurrentDialogueStarter;
    PossibleActionDialogueStarter CurrentActionDialogueStarter;
    [SerializeField] private PlayerInput Playerinput;

    //For Action
    [SerializeField] private FightManager Fightmanager;
    [SerializeField] private DetectiveManager Detectivemanager;
    private Queue<NodeDataHolder> AllActionNodes = new Queue<NodeDataHolder>();

    public void StartDialogue(DialogueContainer _DialogueData, VisualDialogueManager _VisualManager, DialogueStarter _CurrentDialogueStarter) 
    {
        DialogueData = _DialogueData;
        VisualManager = _VisualManager;
        CurrentDialogueStarter = _CurrentDialogueStarter;

        //Disable other game-functionality

        //Let the visual manager load in the graphics
        VisualManager.LoadDialogueVisuals();

        //Load the fitting dialogue
        LoadFirstDialogueStage();

        //Check if there are choices of other dialogues, which have to be payes attention to
            //Tell the Diloguecontainer about this
            //Let the visual manager change the graphics according to that
    }

    public void StartDialogue(DialogueContainer _DialogueData, VisualDialogueManager _VisualManager, PossibleActionDialogueStarter _CurrentDialogueStarter)
    {
        DialogueData = _DialogueData;
        VisualManager = _VisualManager;
        CurrentActionDialogueStarter = _CurrentDialogueStarter;

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
        //Unload all old Answers, the player could have given
        VisualManager.UnloadPlayerAnswers();


        CurrentNode = FindNextNode(_OutputPortNumber);

        if(CurrentNode.IsFightDialogue)
        {
            FindAllActionNodes();
            Fightmanager.StartFightDialogue(AllActionNodes, CurrentActionDialogueStarter);
            return;

            //With this action, the dialoguemanager is done and unable to do anything until the next dialogue has been triggered
        }
        else if(CurrentNode.IsDetectiveDialogue)
        {
            FindAllActionNodes();
            Detectivemanager.StartDetectiveDialogue(AllActionNodes, CurrentActionDialogueStarter);
            return;
        }
        
        VisualManager.DisplayNode(CurrentNode);

        //If the player has answers, load them, if not end the dialogue
        if (!NodeIsLastNode())
            LoadPlayerAnswers();
        else
            PrepareDialogueEnd();
    }

    private NodeDataHolder FindNextNode(int _OutputPortNumber)
    {
        foreach (EdgesDataHolder edge in DialogueData.EdgesData)
        {
            //Find any edge, which was outputed by the CurrentNode
            if (edge.BaseNodeGuid == CurrentNode.Guid)
            {
                //Debug.Log("Found a edge, which Basenode == Currentnode");

                //Find the fitting node to the edge NodeGuid
                foreach (NodeDataHolder node in DialogueData.NodesData)
                {
                    //Debug.Log("Found nodes in the Scrpitableobject�s DialogueData");
                    if (node.Guid == edge.TargetNodeGuid && node.PortNumber == _OutputPortNumber)
                    {
                        return node;
                    }
                }
            }
        }

        return null;
    }

    //To Check, if the Player has Answers, or the dialogue ends with the current node
    private bool NodeIsLastNode()
    {
        if (FindNextNode(1) == null)
            return true;
        else
            return false;
    }

    public void PrepareDialogueEnd()
    {
        VisualManager.LoadDialogueEnding();
    }

    public void EndDialogue()
    {
        //check if there were choices made
        //Save the choices

        //Let the visual manager unload the graphics
        VisualManager.UnloadDialogueVisuals();
        Debug.Log("Ended dialogue");

        //enable other game-functionality
        if (CurrentDialogueStarter != null)
            CurrentDialogueStarter.DialogueIsActive = false;
        else
            CurrentActionDialogueStarter.DialogueIsActive = false;

        Playerinput.CanMove = true;
    }

    private void LoadPlayerAnswers()
    {
        PlayerAnswers.Clear();

        //Finding all possible answer-edges to the currentnode
        for(int i = 0; i < DialogueData.EdgesData.Count; i++)
        {
            if(DialogueData.EdgesData[i].BaseNodeGuid == CurrentNode.Guid)
            {
                PlayerAnswers.Add(DialogueData.EdgesData[i].PortName);
            }
        }

        VisualManager.DisplayPlayerAnswers(PlayerAnswers);
    }

    private void FindAllActionNodes()
    {
        //Clear all previous fight nodes
        AllActionNodes.Clear();

        //Add the current Node (which is a Fight Node, because this code has been triggered
        AllActionNodes.Enqueue(CurrentNode);

        //Iterate through all next nodes to
        while(true)
        {
            //Find all next nodes
            CurrentNode = FindNextNode(1);

            //And if there is any, add them to the list
            if (CurrentNode == null)
                return;
            else
                AllActionNodes.Enqueue(CurrentNode);
        }
    }
}
