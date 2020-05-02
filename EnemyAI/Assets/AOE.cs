using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOE : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        var name = Instantiate(new GameObject(), transform.position, Quaternion.identity);
        name.name = "Loc";
        if(collision.gameObject.layer != 13)
        { 
            EnemyManagment.attractEnemy(name);
            Destroy(this.gameObject);
        }
        
        
    }

}
