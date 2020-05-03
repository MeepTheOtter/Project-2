using Boo.Lang;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AOE : MonoBehaviour
{
    public ParticleSystem soundWaves;
    public LayerMask layerMasque;

    private void OnCollisionEnter(Collision collision)
    {
        soundWaves.Play();
        var name = Instantiate(new GameObject(), transform.position, Quaternion.identity);
        name.name = "Loc";
        if(collision.gameObject.layer == layerMasque) // !=13
        { 
            EnemyManagment.attractEnemy(name);
            Destroy(this.gameObject);
        }
        //Instantiate(soundWaves);
        //soundWaves = GetComponent<ParticleSystem>();
        //ParticleSystem.EmitParams emitOverride = new ParticleSystem.EmitParams();
        //emitOverride.startLifetime = 10f;
        //soundWaves.Emit(emitOverride, 20);

        print("gg");
    }

}
