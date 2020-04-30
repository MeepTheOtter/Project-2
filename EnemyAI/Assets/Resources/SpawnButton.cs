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
    void Start()
    {
        button = (GameObject)Resources.Load("Button", typeof(GameObject));

        chosenSpawn = button.transform.position;

        SpawnButt();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SpawnButt()
    {
        int spawn = Random.Range(0, spawnLocations.Length);
        GameObject.Instantiate(button, spawnLocations[spawn].transform.position, Quaternion.identity);
    }
}
