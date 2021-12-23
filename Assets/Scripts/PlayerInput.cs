using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void Mouseinput(); 
public class PlayerInput : MonoBehaviour
{
    public event Mouseinput Leftclick;

    public float horizontal, vertical;

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }
}
