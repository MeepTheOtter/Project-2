using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour
{
    public Rigidbody bulletPrefabs;
    public GameObject cursor;
    public GameObject player;
    public LayerMask layer;
    public LayerMask wallLayer;
    public LayerMask floorLayer;
    public Transform shootPoint;
    public LineRenderer lineVisual;
    private RaycastHit hit;
    public Material matWhite;
    public Material matRed;
    private Vector3 vO;
    public int lineSegment = 10;
    public bool shootMode = false;
    public GameObject reticle;

    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        lineVisual.positionCount = lineSegment;
        //lineVisual = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        LaunchProjectile();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (shootMode == true)
            {
                reticle.SetActive(true);
                shootMode = false;
                lineVisual.startWidth = 0f;
                lineVisual.endWidth = 0f;
                cursor.SetActive(false);
            }
            else
            {
                reticle.SetActive(false);
                shootMode = true;
                lineVisual.startWidth = .01f;
                lineVisual.endWidth = .18f;
                cursor.SetActive(true);
            }
        }
    }

    void LaunchProjectile()
    {
        if (shootMode == true)
        {
            Ray camRay = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(camRay, out hit, 100f, layer))
            {
                lineVisual.material = matWhite;
                if (Physics.Raycast(camRay, out hit, 100f, wallLayer))
                {
                   cursor.transform.position = hit.point; //hit.normal * 0.1f;
                   cursor.transform.rotation = Quaternion.LookRotation(Vector3.up, hit.normal); 
                }
                if (Physics.Raycast(camRay, out hit, 100f, floorLayer))
                {
                    cursor.transform.rotation = Quaternion.Euler(90, 0, 0);
                   
                }

                if (Vector3.Distance(player.transform.position, hit.point) <= 10)
                {
                    vO = CalculateVelocity(hit.point, shootPoint.transform.position, 1f);
                    cursor.transform.position = hit.point + Vector3.up * 0.1f;
                    lineVisual.material = matWhite;
                    lineVisual.startWidth = .01f;
                    lineVisual.endWidth = .18f;
                    cursor.SetActive(true);

                    if (Input.GetMouseButtonDown(0))
                    {
                        Rigidbody obj = Instantiate(bulletPrefabs, shootPoint.position, Quaternion.identity);
                        obj.velocity = vO;

                    }
                }
                else
                {
                    
                    hit.point = hit.point;
                    cursor.transform.position = cursor.transform.position;
                    lineVisual.startWidth = 0f;
                    lineVisual.endWidth = 0f;
                    cursor.SetActive(false);
                    lineVisual.material = matRed;
                }
                Visualize(vO);

               
            }
            else
            {
                if (vO != null)
                {
                    cursor.transform.position = hit.point + Vector3.up * 0.1f;
                    Visualize(vO);
                    lineVisual.material = matWhite;
                }
            }
        }
    }

    void Visualize(Vector3 vo)
    {
        if (shootMode == true)
        {
            for (int i = 0; i < lineSegment; i++)
            {
                Vector3 pos = CalculatePosInTime(vo, i / (float)lineSegment);
                lineVisual.SetPosition(i, pos);
            }
        }
    }

    Vector3 CalculateVelocity(Vector3 target, Vector3 origin, float time)
    {
        //x and y distance defined first 
        Vector3 distance = target - origin;
        Vector3 distanceXZ = distance;
        distanceXZ.y = 0f;

        //make a float that stands for distance 
        float sY = distance.y;
        float sXZ = distanceXZ.magnitude;

        float vXZ = sXZ / time;
        float vY = sY / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        Vector3 result = distanceXZ.normalized;
        result *= vXZ;
        result.y = vY;

        return result;
    }

    Vector3 CalculatePosInTime(Vector3 vo, float time)
    {
        Vector3 vXZ = vo;
        vXZ.y = 0f;

        Vector3 result = shootPoint.position + vo * time;
        float sY = (-0.5f * Mathf.Abs(Physics.gravity.y) * (time * time) + (vo.y * time) + shootPoint.position.y);

        result.y = sY;

        return result;
    }
}
