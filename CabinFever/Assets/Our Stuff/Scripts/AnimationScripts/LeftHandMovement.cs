using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHandMovement : MonoBehaviour
{
    Vector3 startLocation;
   // float ZMULT = -.05f;
    float XMULT = -.05f;

    float Speed = 7;
    CharacterMovement player;
    void Start()
    {
        startLocation = transform.localPosition;
        player = GetComponentInParent<CharacterMovement>();
    }
    void Update()
    {
        float Yoffset = 0;
        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
        {
            Speed = 1;
        } else if (!player.running)
        {
            Speed = 7;
        } else if (player.running)
        {
            Speed = 9;
            Yoffset = 0.09f;
        }
        float time = (Time.time) * Speed;
        float offset = Mathf.Sin(time);

        Vector3 finalLocation = startLocation;
        finalLocation += new Vector3(offset * XMULT, 0.01f + Yoffset, 0);
        transform.localPosition = finalLocation;
    }
}
