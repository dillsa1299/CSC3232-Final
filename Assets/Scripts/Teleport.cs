using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This script is used to teleport the player to the corresponding island when they collide with it
*/

public class Teleport : MonoBehaviour
{
    public GameObject player;
    CharacterController charController;
    public Transform island;

    // Start is called before the first frame update
    void Start()
    {
        charController = player.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.GetComponent<Player>().teleportPlayer(island.transform.position);
            player.transform.eulerAngles = new Vector3(0,0,0); //Resets player rotation
        }
    }
}
