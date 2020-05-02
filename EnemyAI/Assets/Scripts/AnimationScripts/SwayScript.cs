using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwayScript : MonoBehaviour
{
    Quaternion startRotation;
    float Limiter = .5f;
    float Limiter2 = .7f;
    CharacterMovement player;

    void Start()
    {
        startRotation = transform.localRotation;
        player = GetComponentInParent<CharacterMovement>();
    }
    
    void Update() 
    {
        float time = Time.time;
        float RotateOffset = Mathf.Sin(time * 7);
        bool Moving = (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0) ? true : false;
        if (Moving && !player.running)
        {
            Quaternion A = startRotation;
            A *= Quaternion.Euler(0, 0, RotateOffset * Limiter);
            transform.localRotation = A;
        } else if (Moving && player.running) 
        {
            Quaternion A = startRotation;
            A *= Quaternion.Euler(10 * Input.GetAxis("Vertical"), RotateOffset * 13, -10 * Input.GetAxis("Horizontal"));
            transform.localRotation = A;
        } else
        {
            transform.localRotation = startRotation;
        }
    }
}
