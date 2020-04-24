using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public int lineSegment = 10;
    public bool shootMode = false;

    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        lineVisual.positionCount = lineSegment;
    }

    // Update is called once per frame
    void Update()
    {
        LaunchProjectile();


        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (shootMode == true)
            {
                shootMode = false;
                lineVisual.startWidth = 0f;
                lineVisual.endWidth = 0f;
                cursor.SetActive(false);
            }
            else
            {
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
            RaycastHit hit;

            if (Physics.Raycast(camRay, out hit, 100f, layer))
            {
                cursor.SetActive(true);
                cursor.transform.position = hit.point + Vector3.up * 0.1f;
                if (Physics.Raycast(camRay, out hit, 100f, wallLayer))
                {
                    cursor.transform.rotation = Quaternion.LookRotation(player.transform.position, Vector3.down);
                }
                if (Physics.Raycast(camRay, out hit, 100f, floorLayer))
                {
                    cursor.transform.rotation = Quaternion.Euler(90, 0, 0);
                }

                Vector3 vO = CalculateVelocity(hit.point, shootPoint.position, 1f);

                Visualize(vO);

                //transform.rotation = Quaternion.LookRotation(vO);
                //transform.Rotate(0, -90, 0);

                if (Input.GetMouseButtonDown(0))
                {
                    Rigidbody obj = Instantiate(bulletPrefabs, shootPoint.position, Quaternion.identity);
                    obj.velocity = vO;
                }
            }
            else
            {
                cursor.SetActive(false);
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
