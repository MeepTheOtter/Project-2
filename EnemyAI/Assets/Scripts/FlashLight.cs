using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    public GameObject player;
    public Light lightComp;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //sets the light object at the same rotation and position as player
        lightComp.transform.position = player.transform.position;
        lightComp.transform.rotation = player.transform.rotation;

        //If you press F then turn on and off camera
        if (Input.GetKeyDown(KeyCode.F))
        {
            if(lightComp.intensity == 5)
            {
                lightComp.intensity = 0;
            }
            else
            {
                lightComp.intensity = 5;
            }
            
        }
    }
}
