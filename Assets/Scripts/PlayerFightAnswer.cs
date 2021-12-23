using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFightAnswer : MonoBehaviour
{
    [TextArea(1, 5)]
    public string AnswerText;

    public bool IsLocked;

    private void Awake()
    {
        GetComponentInChildren<Text>().text = AnswerText;

        if (IsLocked)
            gameObject.SetActive(false);
    }
}
