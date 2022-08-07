using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerSentenceInventory : MonoBehaviour
{
    public List<PlayerFightSentence> PlayerFightSentences;
    public GameObject PlayerFightPrefab;
    public Transform InventoryParentPanel;
    private GameObject CurrentSentence;

    public void DisplayFightSentences()
    {
        ClearOldSentences();

        //Loop through all Sentences, which should be displayed
        for(int i = 0; i < PlayerFightSentences.Count; i++)
        {
            //Creating a new Sentece, Setting the correct values and the correct sentence
            CurrentSentence = Instantiate(PlayerFightPrefab, InventoryParentPanel);
            CurrentSentence.GetComponent<PlayerFightAnswer>().SetFightValues(
                PlayerFightSentences[i].Aggressive, PlayerFightSentences[i].Threatening, 
                PlayerFightSentences[i].Defensive, 
                PlayerFightSentences[i].Sentence);
        }
    }

    private void ClearOldSentences()
    {
        foreach (Transform child in InventoryParentPanel)
        {
            Destroy(child.gameObject);
        }
    }
}
