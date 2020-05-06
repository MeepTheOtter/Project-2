using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnButton : MonoBehaviour
{
    public GameObject[] spawnLocations;
    public GameObject button;
    private Vector3 chosenSpawn;
    // Start is called before the first frame update

    void Awake()
    {
        spawnLocations = GameObject.FindGameObjectsWithTag("SpawnPoint");
    }
    private void Start()
    {
        chosenSpawn = button.transform.position;

        SpawnButt();
    }

    private void SpawnButt()
    {
        int spawn = Random.Range(0, spawnLocations.Length);
        GameObject.Instantiate(button, spawnLocations[spawn].transform.position, spawnLocations[spawn].transform.rotation);
    }
}
