using UnityEngine;
using System.Collections;

public class destroyMyself : MonoBehaviour
{
    public void Update()
    {
        Destroy(gameObject, 5);
    }
}