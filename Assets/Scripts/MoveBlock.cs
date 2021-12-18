using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock : MonoBehaviour
{
    public float offSet;
    public float speed = 1.0f;
    Vector3 startPos;
    Vector3 pos1;
    Vector3 pos2;

    public bool leftRight = false;
    public bool upDown = false;
    
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        if(leftRight)
        {
            pos1 = startPos;
            pos1.x = pos1.x - offSet;
            pos2 = startPos;
            pos2.x = pos2.x + offSet;
        }
        if(upDown)
        {
            pos1 = startPos;
            pos2 = startPos;
            pos2.y = pos2.y - offSet;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(pos1, pos2, (Mathf.Sin(speed*Time.time) + 1.0f) / 2.0f);
    }
}
