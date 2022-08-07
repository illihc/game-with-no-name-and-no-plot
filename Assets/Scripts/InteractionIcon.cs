using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionIcon : MonoBehaviour
{
    private Interactable CurrentInteractable;
    [SerializeField] private PlayerInput Playerinput;

    public void GetOriginObject(Interactable _CurrentInteractable)
    {
        CurrentInteractable = _CurrentInteractable;
    }

    public void PressInteraction()
    {
        //Start the interaction
        CurrentInteractable.StartAction();

        //Prevent the player from moving
        Playerinput.CanMove = false;

        //disable the Interactionicon
        gameObject.SetActive(false);
    }
}
