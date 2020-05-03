using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManagment : MonoBehaviour
{
    public List<GameObject> Ghosts = new List<GameObject>();
    public delegate void EnemyDel(GameObject bottle);

    public static EnemyDel attractEnemy;

    // Start is called before the first frame update
    
    void Start()
    {
        attractEnemy += AC;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AC(GameObject bottle)
    {
        for (int i = 0; i < Ghosts.Count; i++)
        {
            if (Vector3.Distance(Ghosts[i].transform.position, bottle.transform.position) < 10)
            {
                Ghosts[i].GetComponent<GhostAI>().AttractMe(bottle); 
            }
            
        }

        //Destroy(bottle);
        /*make instacne of gameobject DistractionSpawn
        //use distractions spawn to distract enemeies
        // once enemey reaches distract point, it should despawn.*/
    }
}
