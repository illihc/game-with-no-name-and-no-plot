using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCam : MonoBehaviour
{
    [SerializeField] private Transform TargetT;
    public Vector3 Offset = new Vector3(0f, 0f, -10f);

    private void LateUpdate()
    {
        transform.position = TargetT.position + Offset;
    }
}
