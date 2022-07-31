using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerFightAnswer : MonoBehaviour
{
    private FightManager Fightmanager;

    public float Aggresion;
    public float Threat;
    public float Defense;

    [SerializeField] private TextMeshProUGUI Text;

    private void OnEnable()
    {
        FindFightManager();
    }
    private void FindFightManager()
    {
        Fightmanager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<FightManager>();
    }
    public void SetFightValues(float _Aggresion, float _Threat, float _Defense, string _Sentence)
    {
        Aggresion = _Aggresion;
        Threat = _Threat;
        Defense = _Defense;

        Text.text = _Sentence;
    }

    public void Answer()
    {
        StartCoroutine(Fightmanager.PlayerDealsDamage(Aggresion, Threat, Defense));
    }
}
