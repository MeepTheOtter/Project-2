using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempControl : MonoBehaviour
{

    private float temp = 30f;
    public Image tempUI;
    private int timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tempUI.color = Color.Lerp(Color.blue, Color.red, temp/30);
        timer++;
        Debug.Log(timer);
        if (timer % 12 == 0)
        {
            
            temp--;
        }
    }
}
