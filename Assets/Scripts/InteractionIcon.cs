using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionIcon : MonoBehaviour
{
    private Interactable CurrentInteractable;

    public void GetOriginObject(Interactable _CurrentInteractable)
    {
        CurrentInteractable = _CurrentInteractable;

        Debug.Log("Testing: Origin Objekt of Current Interaction is: ");
        Debug.Log(CurrentInteractable.gameObject.name);
    }

    public void PressInteraction()
    {
        CurrentInteractable.StartAction();
    }
}
