using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    float rotation = 0f;
    float gravity = 1f;
    float inputSensitivity = 150;
    float speed = 1;
    public bool running = false;
    CharacterController CC;
    Vector3 Movement;
    Vector3 Rotation;

    private void Start()
    {
        CC = gameObject.GetComponent<CharacterController>();
        Rotation = transform.rotation.eulerAngles;
        rotation = Rotation.y;
    }
    void Update()
    {
        if (GameManagerL.isCutscene == false)
        {
            //////////////////////////////////////////////////////
            //PLAYER MOVEMENT
            Movement *= .8f;

            Movement -= transform.right * Input.GetAxis("Vertical");
            Movement += transform.forward * Input.GetAxis("Horizontal");
            Movement -= transform.up * gravity;


            if (Input.GetKey(KeyCode.LeftShift) && tempControl.temp >= 15)
            {
                speed = 2;
                running = true;
            }
            else
            {
                speed = 1;
                running = false;
            }
            Vector3 playerMovement = ((Movement * speed) * Time.deltaTime);
            CC.Move(playerMovement);
            /////////////////////////////////////////////////////
            //PLAYER ROTATION

            //Rotates the player the same way the camera is rotated
            float inputX = Input.GetAxis("RightStickHorizontal");
            float inputY = Input.GetAxis("RightStickVertical");
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            float finalInputX = inputX + mouseX;
            float finalInputY = inputY + mouseY;

            rotation += ((finalInputX * inputSensitivity) * Time.deltaTime);

            //ONLY ROTATES ON THE rotY AXIS NOT X
            Quaternion localRotation = Quaternion.Euler(0, rotation, 0.0f);
            transform.rotation = localRotation;
        }
    }
}
