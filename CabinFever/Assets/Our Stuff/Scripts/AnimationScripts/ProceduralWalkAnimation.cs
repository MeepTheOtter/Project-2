using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralWalkAnimation : MonoBehaviour
{
    Vector3 startingPosition;
    public Transform HintPos;

    Vector3 Currpos;
    //float p = 1;

    float sinWaveSpeed = 7;
    public float sinWaveOffset;

    float feetDistancing = 1.5f;
    float footHeight = .5f;
    float howFarFeetGo = .5f;
    CharacterMovement player;

    void Start()
    {
        startingPosition = transform.localPosition;
        player = GetComponentInParent<CharacterMovement>();

        Currpos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float RunOffset = 1;
        if (player.running)
        {
            sinWaveSpeed = 9;
            RunOffset = 1.4f;
        }
        else
        {
            sinWaveSpeed = 7;
        }
        float time = (Time.time + sinWaveOffset * Mathf.PI) * sinWaveSpeed;
        float offsetForward = Mathf.Cos(time)/2;
        float offestY = Mathf.Cos(time)/2 * RunOffset;
        if (offestY < 0) offestY = 0;
        
        Vector3 finalPosition = startingPosition;

        finalPosition.x *= feetDistancing;

        Vector3 Movement = FindMovement();
        if (Movement.x == 0 && Movement.z == 0) offestY = 0;
        if (GameManagerL.isCutscene == false)
        {
            finalPosition.y += offestY * footHeight; // move final position up or down
            finalPosition.z += Movement.z * offsetForward * howFarFeetGo;
            finalPosition.x += Movement.x * offsetForward * howFarFeetGo;
        }
        
        transform.localPosition = finalPosition;
    }
    Vector3 FindMovement()
    {
        Vector3 A = new Vector3(0, 0, 0);
        Vector3 localForward = transform.worldToLocalMatrix.MultiplyVector(player.gameObject.transform.forward);
        Vector3 localRight = transform.worldToLocalMatrix.MultiplyVector(player.gameObject.transform.right);
        A -= localRight * Input.GetAxis("Horizontal");
        A += localForward * Input.GetAxis("Vertical");
        A.y = 0;
        return A;
    }
}