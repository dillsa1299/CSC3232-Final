using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    This script is used to contain the players data and control how they
    interact with objects
*/

public class Player : MonoBehaviour
{
    public PlayerMovement movement;
    public int friendsRescued = 0;
    public Transform boat;
    public Text uiInfo;
    public Transform startArea;
    public GameObject friend1;
    public GameObject friend2;
    //public GameObject friend3;
    CharacterController charController;
    public Transform island2Spawn;

    public Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.H))
        {
            teleportPlayer(startArea.transform.position);
        }

        if ((Vector3.Distance(boat.transform.position, transform.position) < 12f))
        {
            if (friendsRescued < 3)
            {
                uiInfo.text = "You haven't rescued all of your friends!";
            }
            else
            {
                uiInfo.text = "Lets go home!";
            }
        }
        else
        {
            uiInfo.text = "";
        }

        if ((Vector3.Distance(friend1.transform.position, transform.position) < 3f))
        {
            uiInfo.text = "Press E to free friend";
            if (Input.GetKey(KeyCode.E))
            {
            friend1.SetActive(false);
            friendsRescued +=1;
            teleportPlayer(startArea.transform.position);
            movement.jumpBoostPowerup = false;
            movement.doubleJump = false;
            }
        }
        if ((Vector3.Distance(friend2.transform.position, transform.position) < 3f))
        {
            uiInfo.text = "Press E to free friend";
            if (Input.GetKey(KeyCode.E))
            {
            friend2.SetActive(false);
            friendsRescued +=1;
            teleportPlayer(startArea.transform.position);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Lava"))
        {
            teleportPlayer(island2Spawn.position);
        }
    }

    public void teleportPlayer(Vector3 vectorLocation)
    {
        charController.enabled = false; //Need to disable character controller to teleport player
        transform.position = vectorLocation;
        charController.enabled = true;
    }
}
