using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VisualDialogueManager : MonoBehaviour
{
    [Header("Generell")]
    public GameObject DialogueCanvas;
        public GameObject PlayerAnswersCanvas;
        public GameObject CurrentDisplayedNode;
        [SerializeField] private GameObject PlayerTextPrefab;
    [SerializeField] private GameObject PlayerGoodbyePrefab;

    [Space(10)]
    [Header("Fighting")]
    //Fighting 
    public GameObject FightCanvas;
    public GameObject PlayerAnswerCover;
    public GameObject NPCFightNode;
    public Slider NPCHealthSlider;
    public Slider PlayerHealthSlider;
[Space (10)]
[Header("Detection")]

    //Detective
    public GameObject DetectiveCanvas;
    public Slider NPCAnxietySlider;
    public RectTransform RightAnxietySpotUI;
    public GameObject NPCDetectiveNode;
    public float MaxAnxietyMeterMarkerXPos;

    //Generell
    public GameObject ConflictResultCanvas;

    #region NormalDialogue
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

    #endregion

    #region FightRegion
    public void LoadFightVisuals()
    {
        //Deactivate the DialogueCanvas
        DialogueCanvas.SetActive(false);
        FightCanvas.SetActive(true);
    }

    public void DisplayNextFightRound(string _NPCFightSentence)
    {
        //Set the text to, whatever the NPC has to say
        NPCFightNode.GetComponentInChildren<TMP_Text>().text = _NPCFightSentence;
    }

    public void UsePlayerAnswerCoverUp(bool Activating)
    {
        PlayerAnswerCover.SetActive(Activating);
    }

    public void MaximizeNPCHealth(float _MaxHealth)
    {
        NPCHealthSlider.maxValue = _MaxHealth;
        NPCHealthSlider.value = _MaxHealth;
    }
    public void SetNPCHealthGFX(float _CurrentHealth)
    {
        NPCHealthSlider.value = _CurrentHealth;
    }

    public void MaximizePlayerHealth(float _MaxHealth)
    {
        PlayerHealthSlider.maxValue = _MaxHealth;
        PlayerHealthSlider.value = _MaxHealth;
    }

    public void SetPlayerHealth(float _CurrentHealth)
    {
        PlayerHealthSlider.value = _CurrentHealth;
    }
    public void UnlaodFightVisuals()
    {
        FightCanvas.SetActive(false);
    }

    #endregion

    #region DetectiveDialogue
    public void SetMaxAnxietyMeter(float _MaxAnxietyValue)
    {
        NPCAnxietySlider.maxValue = _MaxAnxietyValue;
    }
    public void SetAnxietyMeter(float _AnxietyValue)
    {
        NPCAnxietySlider.value = _AnxietyValue;
    }

    //Setting the position of the marker, which indicates the right amount of anxiety
    public void SetAnxietyCorrectMarker(float _RightFillAmount, float _MaxFillAmount)
    {
        RightAnxietySpotUI.position = new Vector3((_RightFillAmount * MaxAnxietyMeterMarkerXPos) / _MaxFillAmount, RightAnxietySpotUI.position.y, RightAnxietySpotUI.position.z);

 /*
            Lenght of Healtbar Graphic: 100u
            NeededFillAmount: 50
            MaxFillAmount = 110

            MaxFillPosition =^ 100u
            MinFillPosition =^ 0u

            Verkürzter Dreisatz:

            MaxFillPosition = 100u =^ MaxFillAmount = 110
            NeededPositionOfα =^ NeededFillAmount = 50
            

            NeededPositionOfα = MinFillPosition + (NeededFillAmount * MaxFillPosition) / MaxFillAmount
            NeededPositionOfα = 0u + (50 * 100u) / 110
            
*/
    }

    public void DisplayNextDetectiveRound(string _NPCFightSentence)
    {
        //Set the text to, whatever the NPC has to say
        NPCDetectiveNode.GetComponentInChildren<TMP_Text>().text = _NPCFightSentence;
    }

    public void LoadDetectiveVisuals()
    {
        DialogueCanvas.SetActive(false);
        DetectiveCanvas.SetActive(true);
    }

    public void UnloadDetectiveVisuals()
    {
        DetectiveCanvas.SetActive(false);
    }

    #endregion

    public IEnumerator DisplayConflictResult(string _FightResult)
    {
        ConflictResultCanvas.SetActive(true);
        ConflictResultCanvas.GetComponentInChildren<TMP_Text>().text = _FightResult;

        yield return new WaitForSeconds(2f);
        ConflictResultCanvas.SetActive(false);
    }
}
