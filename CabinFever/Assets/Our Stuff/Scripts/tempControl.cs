using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tempControl : MonoBehaviour
{
    public static float temp = 30;
    public RawImage tempUI;
    public LayerMask player;
    private bool isColliding = false;
    private int timer = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManagerL.isCutscene == false)
        {
            if (temp >= 16)
            {
                tempUI.color = Color.Lerp(Color.blue, Color.red, (temp - 15) / 15);
            }
            else if (temp <= 15) tempUI.color = Color.Lerp(Color.cyan, Color.blue, temp / 15);
            //tempUI.color = Color.Lerp(Color.blue, Color.red, temp / 30);
            if (isColliding == true)
            {
                if (timer % 30 == 0) temp++;
            }
            if (isColliding == false)
            {
                if (timer % 400 == 0) temp--;
            }
            if (temp >= 30) temp = 30;
            timer++;
            isColliding = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (checkDigit(player.value, other.gameObject.layer))
        {
            isColliding = true;
            //Debug.Log(isColliding);
        }
    }
    int setDigit(int bitfield, int n)
    {
        return (1 << n) | bitfield;
    }

    bool checkDigit(int bitfield, int n)
    {
        return ((bitfield >> n) & 1) == 1;
    }
}