using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerUnlock : MonoBehaviour
{
    public GameObject[] AllAnswers;
    public List<string> UnlockedAnswers;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Answer")
        {
            //Activate the button, which has the same name as the key and add it to the unlocked list
            for(int i = 0; i < AllAnswers.Length; i++)
            {
                if(AllAnswers[i].name == collision.name)
                {
                    AllAnswers[i].GetComponent<PlayerFightAnswer>().IsLocked = false;
                    AllAnswers[i].SetActive(true);
                    UnlockedAnswers.Add(AllAnswers[i].name);
                }
            }

            //Deactivating the key
            collision.gameObject.SetActive(false);
        }
    }
}
