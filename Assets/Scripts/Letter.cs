using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Letter : Interactable
{
    [SerializeField] private PlayerInput Playerinput;


    [SerializeField] private TextMeshProUGUI Text;
    private int PageCount;
    [SerializeField] private GameObject LetterPanel;

    [TextArea(15, 20)]
    public string[] Pages;
    public override void StartAction()
    {
        Debug.Log("Action startet");
        PageCount = 0;

        LetterPanel.SetActive(true);
        SetText();
    }

    public void DisplayNextPage()
    {
        if (PageCount + 1 == Pages.Length)
        {
            EndAction(); 
            return;
        }

        PageCount++;
        SetText();
    }

    public void DisplayLastPage()
    {
        if (PageCount == 0)
            return;

        PageCount--;
        SetText();
    }

    private void SetText()
    {
        Text.text = Pages[PageCount];
    }

    public override void EndAction()
    {
        LetterPanel.SetActive(false);
        Playerinput.CanMove = true;
    }
}
