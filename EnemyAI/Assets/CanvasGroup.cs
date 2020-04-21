using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasGroup : MonoBehaviour
{

    public GameObject Fbutton;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fbutton.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            Fbutton.SetActive(true);
        }
    }
}
