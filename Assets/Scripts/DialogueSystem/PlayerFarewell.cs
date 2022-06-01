using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFarewell : MonoBehaviour
{
    public void EndDialogue()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<DialogueManager>().EndDialogue();
    }
}
