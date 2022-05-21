using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void Mouseinput(); 
public class PlayerInput : MonoBehaviour
{
    public event Mouseinput Leftclick;

    public float horizontal, vertical;
    public bool CanMove = true;

    void Update()
    {
        if (!CanMove)
        {
            horizontal = 0;
            vertical = 0;
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }
}
