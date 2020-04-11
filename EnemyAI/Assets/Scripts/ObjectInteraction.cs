using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    public GameObject player;
    public Transform[] pickUps;
    public float pickUpRadius = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i <= pickUps.Length; i++)
        {
            float distance = Vector3.Distance(player.transform.position, pickUps[i].transform.position);

            if(distance <= pickUpRadius)
            {
                print("GG");
            }
        } 
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(player.transform.position, pickUpRadius);
    }
}
