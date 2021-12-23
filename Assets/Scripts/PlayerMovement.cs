using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInput Playerinput;


    public float MoveSpeed = 10f;
    private Rigidbody2D rb;

    private void Awake()
    {
        Playerinput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(Playerinput.horizontal, Playerinput.vertical) * MoveSpeed * Time.fixedDeltaTime;
    }
}
