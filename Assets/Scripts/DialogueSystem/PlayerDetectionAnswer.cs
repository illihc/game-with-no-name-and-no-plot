using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectionAnswer : MonoBehaviour
{
    private DetectiveManager Detectivemanager;

    public float Threat;
    public float Neutral;
    public float Understanding;
    public float Confront;

    private void OnEnable()
    {
        FindDetectiveManager();
    }
    private void FindDetectiveManager()
    {
        Detectivemanager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<DetectiveManager>();
    }

    public void Answer()
    {
        StartCoroutine(Detectivemanager.PlayerAsksQuestion(Threat, Neutral, Understanding, Confront));
    }
}
