
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
    private Vector3 GFXPos;
    private float hitTimer = 0;

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
        if (GameManagerL.isCutscene == false)
        {
            GFXPos = transform.position += (transform.TransformDirection(Vector3.up) * 5);
            float distance = Vector3.Distance(target.position, GFXPos);

            if (distance <= lookRadius && bottleNear == false)
            {
                agent.SetDestination(target.position);
                agent.speed = 3;
                waitTime = startWaitTime;
            }
            else
            {
                if (waitTime >= wanderTimer)
                {
                    Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                    agent.SetDestination(newPos);
                    waitTime = 0;
                    if (target != player.transform) target = player.transform;
                }
                else
                {
                    waitTime += Time.deltaTime;
                }
            }

            if(target == player.transform && Vector3.Distance(target.position, GFXPos) <= .5 && hitTimer <= 0)
            {
                hitTimer = 5;
                tempControl.temp -= 10;
            }
            else
            {
                hitTimer -= Time.deltaTime;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 13 && hitTimer <= 0)
        {
            hitTimer = 5;
            tempControl.temp -= 10;
        }
        else
        {
            hitTimer -= Time.deltaTime;
        }
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
        target = bPos.transform;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GFXPos, lookRadius);
    }
}
