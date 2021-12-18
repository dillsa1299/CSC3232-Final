using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public GameObject friend3;
    CharacterController charController;
    public Transform island2Spawn;
    public int health = 10;
    public GameObject gun;
    public GameObject crosshair;
    public Text friendsRescuedText;
    public TextMeshPro healthBar;
    public GameObject finalCam;
    public GameObject healthBarObj;

    bool canDamage = true;
    public float damageCooldown = 0.5f;
    float cooldownTimer = 0;

    public AudioSource attackSound;

    Vector3 Island3Spawn;

    // Start is called before the first frame update
    void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!canDamage)
        {
            cooldownTimer += Time.deltaTime;
            if (cooldownTimer > damageCooldown)
            {
                cooldownTimer = 0;
                canDamage = true;
            }
        }
        UpdateHealthBar();
        friendsRescuedText.text = "Friends Rescued " + friendsRescued.ToString() + "/3";
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
                uiInfo.text = "Press E to return home!";
                if (Input.GetKey(KeyCode.E))
                {
                    finalCam.SetActive(true);
                    teleportPlayer(startArea.transform.position);
                    friendsRescuedText.text = "";
                }
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
        if ((Vector3.Distance(friend3.transform.position, transform.position) < 3f))
        {
            uiInfo.text = "Press E to free friend";
            if (Input.GetKey(KeyCode.E))
            {
            friend3.SetActive(false);
            friendsRescued +=1;
            CanShoot(false);
            health = 10;
            healthBarObj.SetActive(false);
            teleportPlayer(startArea.transform.position);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Lava"))
        {
            teleportPlayer(island2Spawn.position);
            transform.eulerAngles = new Vector3(0,0,0); //Resets player rotation
        }
        if (other.gameObject.CompareTag("Health"))
        {
            health += 2;
            if (health > 10)
            {
                health = 10;
            }
        }
    }

    public void teleportPlayer(Vector3 vectorLocation)
    {
        charController.enabled = false; //Need to disable character controller to teleport player
        transform.position = vectorLocation;
        charController.enabled = true;
    }

    public void CanShoot(bool canShoot)
    {
        Island3Spawn = transform.position;
        gun.SetActive(canShoot);
        crosshair.SetActive(canShoot);
        if (friend3 != null)
        {
            friend3.GetComponent<FindNewSpot>().NewSpot();
        }
    }

    public void damagePlayer(int damage)
    {
        if (canDamage)
        {
            if (health > 1)
            {
                health--;
                canDamage = false;
                attackSound.volume = Random.Range(0.6f, 0.8f);
                attackSound.pitch = Random.Range(0.9f, 1.1f);
                attackSound.Play();
            }
            else
            {
                attackSound.volume = Random.Range(0.6f, 0.8f);
                attackSound.pitch = Random.Range(0.9f, 1.1f);
                attackSound.Play();
                Kill();
            }
        }
    }

    public void Kill()
    {
        transform.position = Island3Spawn;
        if (friend3 != null)
        {
            friend3.GetComponent<FindNewSpot>().NewSpot();
        }
        health = 10;
    }

    void UpdateHealthBar()
    {
        switch (health)
        {
            case 10:
                healthBar.color = new Color(0f,0.6f,0f,1f); //Full health
                healthBar.text = "----------";
                break;
            case 9:
                healthBar.color = new Color(0f,0.6f,0f,1f); //Full health
                healthBar.text = "---------<color=grey>-";
                break;
            case 8:
                healthBar.color = new Color(0f,0.6f,0f,1f); //Full health
                healthBar.text = "--------<color=grey>--";
                break;
            case 7:
                healthBar.color = new Color(0f,0.6f,0f,1f); //Full health
                healthBar.text = "-------<color=grey>---";
                break;
            case 6:
                healthBar.color = new Color(0.85f,0.44f,0f,1f); // Half health
                healthBar.text = "------<color=grey>----";
                break;
            case 5:
                healthBar.color = new Color(0.85f,0.44f,0f,1f); // Half health
                healthBar.text = "-----<color=grey>-----";
                break;
            case 4:
                healthBar.color = new Color(0.85f,0.44f,0f,1f); // Half health
                healthBar.text = "----<color=grey>------";
                break;
            case 3:
                healthBar.color = new Color(0.66f,0f,0f,1f); // Low health
                healthBar.text = "---<color=grey>-------";
                break;
            case 2:
                healthBar.color = new Color(0.66f,0f,0f,1f); // Low health
                healthBar.text = "--<color=grey>--------";
                break;
            case 1:
                healthBar.color = new Color(0.66f,0f,0f,1f); // Low health
                healthBar.text = "-<color=grey>---------";
                break;
            default:
                healthBar.text = "<color=grey>----------";
                break;
        }
    }
}
