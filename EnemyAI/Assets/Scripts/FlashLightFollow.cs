using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightFollow : MonoBehaviour
{
    public float lightMoveSpeed = 120.0f;
    public GameObject LightFollowObject;
    Vector3 FollowPOS;
    public float clampAngle = 80.0f;
    public float inputSensitivity = 150.0f;
    public GameObject CameraObject;
    public GameObject PlayerObject;
    public float mouseX;
    public float mouseY;
    public float finalInputX;
    public float finalInputY;
    public float smoothX;
    public float smoothY;
    private float rotY = 0.0f;
    private float rotX = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //EXACT ROTATION AS THE CAMERA
        float inputX = Input.GetAxis("RightStickHorizontal");
        float inputY = Input.GetAxis("RightStickVertical");
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        finalInputX = inputX + mouseX;
        finalInputY = inputY + mouseY;

        rotY += finalInputX * inputSensitivity * Time.deltaTime;
        rotX += finalInputY * inputSensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.rotation = localRotation;
    }

    void LateUpdate()
    {
        LightUpdater();
    }

    private void LightUpdater()
    {
        Transform target = LightFollowObject.transform;

        float step = lightMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
}
