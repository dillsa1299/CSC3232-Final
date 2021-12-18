using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;


public class Zombie : MonoBehaviour
{
    NavMeshAgent agent;
    float startSpeed;
    public GameObject player;
    Player playerInfo;
    public float remainingDistance;
    public float findNewLocationTime = 10f;
    float timer = 0f;
    public int health = 3;
    public TextMeshPro healthBar;
    public float viewDistance = 20f;
    public float deathTime = 10f;
    float deathTimer = 0f;
    Vector3 startPos;
    Vector3 startScale;
    bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        startSpeed = agent.speed;
        startPos = transform.position;
        startScale = transform.localScale;
        playerInfo = player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        

        if (isDead)
        {
            deathTimer += Time.deltaTime;
            if ( deathTimer > deathTime)
            {
                Respawn();
                deathTimer = 0;
            }
        }

        Vector3 pos = transform.position;
        if (CanSeePlayer())
        {
            agent.destination = player.transform.position;
            timer = 0;
            agent.speed = startSpeed*2.5f;
            
        }
        else
        {
            agent.speed = startSpeed;
            if(agent.remainingDistance < 0.2f)
            {
                agent.destination = PickRandomPosition();
                timer = 0;
            }
            else
            {
                /*
                    If agent doesn't reach destination within 10 seconds
                    likely because stuck, then pick a new destination
                */
                timer += Time.deltaTime;
                if(timer > findNewLocationTime)
                {
                    agent.destination = PickRandomPosition();
                    timer = 0;
                }
            }
        }
        remainingDistance = agent.remainingDistance;

        var lookPos = agent.destination - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 1f);

        switch (health)
        {
            case 3:
                healthBar.color = new Color(0f,0.6f,0f,1f); //Full health
                healthBar.text = "---";
                //<color=grey>
                break;
            case 2:
                healthBar.color = new Color(0.85f,0.44f,0f,1f); // Half health
                healthBar.text = "--<color=grey>-";
                break;
            case 1:
                healthBar.color = new Color(0.66f,0f,0f,1f); // Low health
                healthBar.text = "-<color=grey>--";
                break;
            default:
                healthBar.text = "";
                break;
        }
    }

    Vector3 PickRandomPosition()
    {
        Vector3 destination = transform.position;
        Vector2 randomDirection = UnityEngine.Random.insideUnitCircle * 8.0f;
        destination.x += randomDirection.x;
        destination.z += randomDirection.y;
        NavMeshHit navHit;
        NavMesh.SamplePosition(destination, out navHit, 8.0f, NavMesh.AllAreas);
        return navHit.position;
    }

    bool CanSeePlayer()
    {
        Vector3 rayPos = transform.position;
        Vector3 rayDir = (player.transform.position - rayPos).normalized;
        RaycastHit info;
        if (Physics.Raycast(rayPos, rayDir, out info, viewDistance))
        {
            if (info.transform.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInfo.damagePlayer(1); 
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 1)
        {
            Die();
        }
    }

    void Die()
    {
        transform.localScale = new Vector3(0,0,0);
        agent.speed = 0;
        isDead = true;
        gameObject.layer = 9; //Dead layer, cant interact with player
    }

    void Respawn()
    {
        transform.localScale = startScale;
        transform.position = startPos;
        agent.speed = startSpeed;
        isDead = false;
        gameObject.layer = 6; //Can interact with player again
        health = 3;
    }
}
