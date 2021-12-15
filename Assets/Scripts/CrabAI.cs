using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This script is for a simple state-based AI, that controls crabs.
    Normally they will wander around, unless near a player, where they will run away.
*/

public class CrabAI : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public Player player;
    public LayerMask mask;
    float wanderRadius = 4.0f; //Find new position within a 4 unit radius
    float wanderTimer = 5.0f; //Wander to new position every 5 seconds
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = wanderTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanSeePlayer())
        {
            agent.destination = PickHidingPlace();
            agent.speed = 8f;
        }
        else
        {
            agent.speed = 2f;
            timer += Time.deltaTime;

            if (timer >= wanderTimer)
            {
                agent.destination = PickRandomPosition();
                timer = 0f;
            }
        }
        
    }

    Vector3 PickRandomPosition() //Picks next position to wander to
    {
        Vector3 origin = transform.position;
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * wanderRadius;
        randomDirection += origin;

        UnityEngine.AI.NavMeshHit navHit;
        UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out navHit, wanderRadius, mask);

        return navHit.position;
    }

    bool CanSeePlayer()
    {
        Vector3 rayPos = transform.position;
        Vector3 rayDir = (player.transform.position - rayPos).normalized;

        RaycastHit info;
        if (Physics.Raycast(rayPos, rayDir, out info, 20f))
        {
            if (info.transform.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }

    Vector3 PickHidingPlace()
    {
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;

        UnityEngine.AI.NavMeshHit navHit;
        UnityEngine.AI.NavMesh.SamplePosition(transform.position - (directionToPlayer * wanderRadius), out navHit, wanderRadius, mask);

        return navHit.position;
    }
}
