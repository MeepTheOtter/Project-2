using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandMovement : MonoBehaviour
{
    Vector3 startLocation;
    float ZMULT = .05f;
    float XMULT = .05f;

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
        finalLocation += new Vector3(offset * XMULT, 0.1f, 0);
        transform.localPosition = finalLocation;
    }
}
