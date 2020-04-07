using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterParamaters : MonoBehaviour
{
    public bool isDead = false;
    public float pTemp = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pTemp <= 0) isDead = true; //If player's temp drops to 0 or less he dies
    }
}
