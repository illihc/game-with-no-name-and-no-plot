using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VisualDialogueManager : MonoBehaviour
{
        public GameObject DialogueCanvas;
        public GameObject PlayerAnswersCanvas;
        public GameObject CurrentDisplayedNode;
        [SerializeField] private GameObject PlayerTextPrefab;
    [SerializeField] private GameObject PlayerGoodbyePrefab;

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

    public void UnloadPlayerAnswers()
    {
        foreach (Transform child in PlayerAnswersCanvas.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void LoadDialogueEnding()
    {
        Instantiate(PlayerGoodbyePrefab, PlayerAnswersCanvas.transform);
    }

    public void DisplayPlayerAnswers(List<string> _PlayerAnswersT)
    {
        GameObject PlayerAnswerG;

        for (int i = 0; i < _PlayerAnswersT.Count; i++)
        {
            //Creating the button an displaying its text
            PlayerAnswerG = Instantiate(PlayerTextPrefab, PlayerAnswersCanvas.transform);
            PlayerAnswerG.GetComponentInChildren<TMP_Text>().text = _PlayerAnswersT[i];

            //Sett the Buttonscript to the correct number
            PlayerAnswerG.GetComponent<PlayerAnswer>().AnswerNumber = i + 1;
        }
    }
}
