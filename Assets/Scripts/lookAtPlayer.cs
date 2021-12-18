using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAtPlayer : MonoBehaviour
{
    public Transform target;
    public float offSet = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
        Vector3 rot = transform.eulerAngles;
        rot.y += offSet;
        transform.eulerAngles = rot;
    }
}
