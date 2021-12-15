using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchRock : MonoBehaviour
{
    public float cycleTime = 3f;
    Vector3 startPos;
    void Start()
    {
        startPos = transform.position;
        InvokeRepeating("Activate", 0f, cycleTime);
    }

    void Activate()
    {
        transform.position = startPos;
        GetComponent<ParabolaController>().FollowParabola();
    }
}
