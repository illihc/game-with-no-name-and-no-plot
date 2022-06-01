using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAnswer : MonoBehaviour
{
    public int AnswerNumber = 1;

    public void LoadNextNPCSentence()
    {
        //Not very performant (it has to do a string comparison with the Tag of every object, but it works in any situation
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<DialogueManager>().LoadNextDialogueStage(AnswerNumber);
        Debug.Log("Loaded the next NPC Sentence");
    }
}
