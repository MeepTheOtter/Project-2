﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHandMovement : MonoBehaviour
{
    Vector3 startLocation;
    float YMULT = -.05f;
    float XMULT = -.05f;

    float Speed = 7;
    void Start()
    {
        startLocation = transform.localPosition;
    }
    void Update()
    {
        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
        {
            Speed = 1;
        } else
        {
            Speed = 7;
        }
        float time = (Time.time) * Speed;
        float offset = Mathf.Sin(time);

        Vector3 finalLocation = startLocation;
        finalLocation += new Vector3(offset * XMULT, offset * YMULT, 0);
        transform.localPosition = finalLocation;
    }
}