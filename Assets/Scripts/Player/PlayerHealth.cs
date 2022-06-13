using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int MaxPlayerHealth = 100;
    public float CurrentPlayerhealth;

    private void Awake()
    {
        CurrentPlayerhealth = MaxPlayerHealth;
    }

    public void TakeDamage(int _Damage)
    {
        CurrentPlayerhealth -= _Damage;
    }

    public void RegenerateHealth(int _Regeneration)
    {
        CurrentPlayerhealth += _Regeneration;
        Mathf.Clamp(CurrentPlayerhealth, 0f, MaxPlayerHealth);
    }
}
