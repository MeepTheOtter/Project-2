using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashLight : MonoBehaviour
{
    public Light lightComp;
    public GameObject mainCam;
    public RawImage flashlight;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManagerL.isCutscene == false)
        {
            lightComp.transform.rotation = mainCam.transform.rotation;
            //If you press F then turn on and off camera
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (lightComp.intensity == 7)
                {
                    flashlight.color = Color.gray;
                    lightComp.intensity = 0;
                }
                else
                {
                    flashlight.color = Color.green;
                    lightComp.intensity = 7;
                }
            }
        }
    }
}
