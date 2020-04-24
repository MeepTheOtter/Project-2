using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    public Light lightComp;
    public GameObject mainCam;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        lightComp.transform.rotation = mainCam.transform.rotation;
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
