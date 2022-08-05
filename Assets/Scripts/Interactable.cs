using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] private GameObject InteractionIconG;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            ShowInteractableIcon();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HideInteractableIcon();
        }
    }

    public abstract void StartAction();

    public abstract void EndAction();

    private void ShowInteractableIcon()
    {
        InteractionIconG.SetActive(true);
        InteractionIconG.GetComponent<InteractionIcon>().GetOriginObject(this);
    }

    private void HideInteractableIcon()
    {
        InteractionIconG.SetActive(false);
    }
}
