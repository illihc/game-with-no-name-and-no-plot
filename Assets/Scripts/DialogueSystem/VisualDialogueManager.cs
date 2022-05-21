using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualDialogueManager : MonoBehaviour
{
        public GameObject DialogueCanvas;
        public GameObject CurrentDisplayedNode;

        public void LoadDialogueVisuals()
        {
            DialogueCanvas.SetActive(true);
        }

        public void DisplayNode(NodeDataHolder node)
        {
            CurrentDisplayedNode.SetActive(true);

            CurrentDisplayedNode.GetComponentInChildren<Text>().text = node.DialogueText;
        }

    public void UnloadDialogueVisuals()
    {
        DialogueCanvas.SetActive(false);
    }
}
