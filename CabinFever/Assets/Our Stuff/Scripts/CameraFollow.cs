using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFollow : MonoBehaviour
{
    public float cameraMoveSpeed = 120.0f;
    public GameObject CameraFollowObject;
    Vector3 FollowPOS;
    public float clampAngle = 80.0f;
    public float inputSensitivity = 150.0f;
    public GameObject CameraObject;
    public GameObject PlayerObject;
    public float camDistanceXToPlayer;
    public float camDistanceYToPlayer;
    public float camDistanceZToPlayer;
    public float mouseX;
    public float mouseY;
    public float finalInputX;
    public float finalInputY;
    public float smoothX;
    public float smoothY;
    private float rotY = 0.0f;
    private float rotX = 0.0f;
    public LayerMask layerMask;
    public Text pickUp;
    public GameObject key1;
    public GameObject key2;
    public GameObject key3;
    public GameObject key4;
    public GameObject bookCase;
    public GameObject EntranceGate1;
    public GameObject EntranceGate2;
    public GameObject PavillionDoor;
    public GameObject HouseDoor;
    public GameObject BathroomDoor1;
    public GameObject BathroomDoor2;
    private bool rotateGate = false;
    private bool rotatePDoor = false;
    private bool rotateHD = false;
    private bool rotateBD = false;
    private float turningRate = 30f;
    private Quaternion targetRotation1 = Quaternion.Euler(0, -90, 0);
    private Quaternion targetRotation2 = Quaternion.Euler(0, 270, 0);
    private Quaternion targetRotation3 = Quaternion.Euler(0, 70, 0);
    private Quaternion targetRotation4 = Quaternion.Euler(0, -22, 0);
    private Quaternion targetRotation5 = Quaternion.Euler(0, 50, 0);
    private Quaternion targetRotation6 = Quaternion.Euler(0, 12, 0);
    private float bookCaseFreezeTimer;
    private bool bookCaseFreezeCheck = false;
    private float cutsceneTimer = 7;
    public Canvas can;
    //public Canvas Canvas;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;

        //Locks screen and hides mouse
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //var fps = 1.0 / Time.deltaTime;
        //print(fps);
        Application.targetFrameRate = 60;

        if (GameManagerL.isCutscene == true)
        {
            can.enabled = false;
            cutsceneTimer -= Time.deltaTime;
            if (cutsceneTimer <= 0)
            {
                cutsceneTimer = 0;
                GameManagerL.isCutscene = false;
            }
        }
        // print(cutsceneTimer);
        if (GameManagerL.isCutscene == false)
        {
            can.enabled = true;

            //Exit Game
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            }



            //Gets all the axis and bases them off the mouseX and mouseY
            float inputX = Input.GetAxis("RightStickHorizontal");
            float inputY = Input.GetAxis("RightStickVertical");
            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");
            finalInputX = inputX + mouseX;
            finalInputY = inputY + mouseY;

            //Rotates camera based on Mouse movement and sensitivity
            rotY += finalInputX * inputSensitivity * Time.deltaTime;
            rotX += finalInputY * inputSensitivity * Time.deltaTime;

            //Restricts the camera from moving too far up and down

            if (Projectile.shootMode)
            {
                rotX = Mathf.Clamp(rotX, 13.5f, clampAngle);
            }
            else
            {
                rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);
            }

            Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
            transform.rotation = localRotation;

            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 3f, layerMask))
            {
                pickUp.enabled = true;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (hit.collider.gameObject.tag == "Key")
                    {
                        GameManagerL.keys++;
                        Destroy(hit.collider.gameObject);
                    }
                    if (hit.collider.gameObject.tag == "Lock1" && GameManagerL.keys >= 1)
                    {
                        rotatePDoor = true;
                    }
                    if (hit.collider.gameObject.tag == "Lock2" && GameManagerL.keys >= 2)
                    {
                        rotateBD = true;
                    }
                    if (hit.collider.gameObject.tag == "Lock3" && GameManagerL.keys >= 3)
                    {
                        rotateHD = true;
                    }
                    if (hit.collider.gameObject.tag == "Lock4" && GameManagerL.keys == 4)
                    {
                        rotateGate = true;
                        Destroy(hit.collider.gameObject);
                    }
                    if (hit.collider.gameObject.tag == "Bottle")
                    {
                        GameManagerL.bottleCount += 3;
                        Destroy(hit.collider.gameObject);
                    }
                    if (hit.collider.gameObject.tag == "Button")
                    {
                        bookCaseFreezeCheck = true;
                        bookCaseFreezeTimer = 1;
                        bookCase.GetComponent<Rigidbody>().AddForce(new Vector3(350, 0, 0));
                        Destroy(hit.collider.gameObject);
                    }
                }
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                //Debug.Log("Did Hit");

            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 3, Color.white);
                pickUp.enabled = false;
            }

            if (bookCaseFreezeCheck == true)
            {
                if (bookCaseFreezeTimer >= 1)
                {
                    bookCaseFreezeTimer -= Time.deltaTime;
                }
                if (bookCaseFreezeTimer <= 0)
                {
                    bookCaseFreezeTimer = 0;
                    bookCase.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }
            }

            if (rotateGate)
            {
                EntranceGate1.transform.rotation = Quaternion.RotateTowards(EntranceGate1.transform.rotation, targetRotation1, turningRate * Time.deltaTime);
                EntranceGate2.transform.rotation = Quaternion.RotateTowards(EntranceGate2.transform.rotation, targetRotation2, turningRate * Time.deltaTime);
            }
            if (rotatePDoor)
            {
                PavillionDoor.transform.rotation = Quaternion.RotateTowards(PavillionDoor.transform.rotation, targetRotation3, turningRate * Time.deltaTime);
            }
            if (rotateBD)
            {
                BathroomDoor1.transform.rotation = Quaternion.RotateTowards(BathroomDoor1.transform.rotation, targetRotation4, turningRate * Time.deltaTime);
                BathroomDoor2.transform.rotation = Quaternion.RotateTowards(BathroomDoor2.transform.rotation, targetRotation5, turningRate * Time.deltaTime);
            }
            if (rotateHD)
            {
                HouseDoor.transform.rotation = Quaternion.RotateTowards(HouseDoor.transform.rotation, targetRotation6, turningRate * Time.deltaTime);
            }

            if (GameManagerL.keys == 1)
            {
                key1.SetActive(true);
            }
            if (GameManagerL.keys == 2)
            {
                key2.SetActive(true);
            }
            if (GameManagerL.keys == 3)
            {
                key3.SetActive(true);
            }
            if (GameManagerL.keys == 4)
            {
                key4.SetActive(true);
            }
        }
    }

    void LateUpdate()
    {
        CameraUpdater();
    }

    private void CameraUpdater()
    {
        //Sets camera follow target
        Transform target = CameraFollowObject.transform;

        //Moves the camera around the object based on speed (step)
        float step = cameraMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
}
