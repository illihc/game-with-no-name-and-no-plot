using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    public float horizontal, vertical;
    public bool CanMove = true;

    private void Update()
    {
        if (!CanMove)
        {
            horizontal = 0;
            vertical = 0;
        }
        else
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
        }
    }
}
