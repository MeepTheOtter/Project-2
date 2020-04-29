using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwayScript : MonoBehaviour
{
    Quaternion startRotation;
    float Limiter = .05f;

    void Start()
    {
        startRotation = transform.localRotation;
    }
    
    void Update() 
    {
        float time = Time.time;
        float RotateOffset = Mathf.Sin(time * 7);
        bool Moving = (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0) ? true : false;
        if (Moving)
        {
            Quaternion A = startRotation;
            startRotation *= Quaternion.Euler(0, 0, RotateOffset * Limiter);
            transform.localRotation = A;
        } else
        {
            transform.localRotation = startRotation;
        }
    }
}
