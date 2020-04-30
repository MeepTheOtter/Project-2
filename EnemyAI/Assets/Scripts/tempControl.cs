using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tempControl : MonoBehaviour
{
    private float temp = 30;
    public RawImage tempUI;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tempUI.color = Color.Lerp(Color.blue, Color.red, temp / 30);
        temp--;
    }
}
