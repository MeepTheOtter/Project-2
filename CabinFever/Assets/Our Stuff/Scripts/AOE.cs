using Boo.Lang;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AOE : MonoBehaviour
{
    public ParticleSystem soundWaves;
    public GameObject brokenBottle;
    public LayerMask layerMasque;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9 || collision.gameObject.layer == 10)
        {
            Instantiate(soundWaves, transform.position, Quaternion.identity);
            if (soundWaves.IsAlive())
            {
                Destroy(soundWaves);
            }
            GameObject brokenBottleObj = Instantiate(brokenBottle, transform.position, gameObject.transform.rotation) as GameObject;
            Rigidbody[] allRigidBodies = brokenBottleObj.GetComponentsInChildren<Rigidbody>();
            if (allRigidBodies.Length > 0)
            {
                foreach (var body in allRigidBodies)
                {
                    body.AddExplosionForce(100, brokenBottleObj.transform.position, 3);
                }
            }
            EnemyManagment.attractEnemy(brokenBottle);
            Destroy(gameObject);
        }
    }
}
