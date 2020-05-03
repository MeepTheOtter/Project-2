
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostAI : MonoBehaviour
{
    public float lookRadius = 4f;
    NavMeshAgent agent;
    Transform target;
    public GameObject player;
 
    private float waitTime;
    public float wanderTimer;
    public float startWaitTime;
    public float wanderRadius = 50f;
    public GameObject bPos;
    private bool bottleNear = false;
   


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = player.transform;
       
        waitTime = startWaitTime;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius && bottleNear == false)
        {
            agent.SetDestination(target.position);
            waitTime = startWaitTime;
        }
        else
        {
            if (waitTime >= wanderTimer)
            {
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                agent.SetDestination(newPos);
                waitTime = 0;
            }
            else
            {
                waitTime += Time.deltaTime;
            }
        }
         if(bPos != null) 
        {
            if (Vector3.Distance(bPos.transform.position, transform.position) <= .01f) 
            {
                /*if (waitTime >= wanderTimer)
                {
                    Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                    agent.SetDestination(newPos);
                    waitTime = 0;
                }
                else
                {
                    waitTime += Time.deltaTime;
                }*/

                Destroy(bPos);
                bottleNear = false;
            }
            else
            {
                waitTime = 0;
            }

        }

        //print(waitTime);
        
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;

        randomDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);

        return navHit.position;
    }

    public void AttractMe(GameObject bottle)
    {
        bPos = bottle;
        bottleNear = true;
        agent.SetDestination(bPos.transform.position);
        waitTime = startWaitTime;
        

    }



    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
