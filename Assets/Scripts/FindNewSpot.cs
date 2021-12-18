using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This script finds a new house for the friend in encounter 3
    to hide in. If the player dies, the friend will find a new house
    to hide in.
*/

public class FindNewSpot : MonoBehaviour
{
    GameObject[] positions;

    // Start is called before the first frame update
    void Start()
    {
        positions = GameObject.FindGameObjectsWithTag("FriendSpawn");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NewSpot()
    {
        int randomPos = Random.Range(0, positions.Length);
        Vector3 newPos = positions[randomPos].transform.position;
        transform.position = newPos;
        transform.localEulerAngles = positions[randomPos].transform.localEulerAngles;
    }
}
