using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchRock : MonoBehaviour
{
    public float cycleTime = 3f;
    public float startDelay = 0f;
    Vector3 startPos;
    void Start()
    {
        startPos = transform.position;
        InvokeRepeating("Activate", startDelay, cycleTime);
    }

    void Activate()
    {
        transform.position = startPos;
        GetComponent<ParabolaController>().FollowParabola();
    }
}
