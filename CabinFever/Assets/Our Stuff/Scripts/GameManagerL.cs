using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerL : MonoBehaviour
{

    //THIS MAKES IT EASIER TO DECTED PLAYER FOR CAMERA
    #region Singleton

    public static GameManagerL instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject player;
    public static int keys = 0;

    public static bool isCutscene = true;
    public static int bottleCount = 3;   
}
