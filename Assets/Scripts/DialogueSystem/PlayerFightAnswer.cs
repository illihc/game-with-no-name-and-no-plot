using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFightAnswer : MonoBehaviour
{
    private FightManager Fightmanager;

    public float Aggresion;
    public float Threat;
    public float Defense;

    private void OnEnable()
    {
        FindFightManager();
    }
    private void FindFightManager()
    {
        Fightmanager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<FightManager>();
    }

    public void UseAnswer()
    {
        StartCoroutine(Fightmanager.PlayerDealsDamage(Aggresion, Threat, Defense));
    }
}
