using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTentKey : MonoBehaviour
{
    public GameObject[] spawnLocationsT;
    public GameObject[] spawnLocationsP;
    public GameObject[] spawnLocationsB;
    public GameObject key;
    public GameObject key2;
    public GameObject key3;
    private Vector3 chosenSpawnT;
    private Vector3 chosenSpawnP;
    private Vector3 chosenSpawnB;
    // Start is called before the first frame update

    void Awake()
    {
        spawnLocationsT = GameObject.FindGameObjectsWithTag("SpawnPointTents");
        spawnLocationsP = GameObject.FindGameObjectsWithTag("SpawnPointPavillion");
        spawnLocationsB = GameObject.FindGameObjectsWithTag("SpawnPointBathroom");
    }
    private void Start()
    {
        chosenSpawnT = key.transform.position;
        chosenSpawnP = key2.transform.position;
        chosenSpawnP = key3.transform.position;

        SpawnKeyT();
        SpawnKeyP();
        SpawnKeyB();
    }

    private void SpawnKeyT()
    {
        int spawn = Random.Range(0, spawnLocationsT.Length);
        GameObject.Instantiate(key, spawnLocationsT[spawn].transform.position, spawnLocationsT[spawn].transform.rotation);
    }
    private void SpawnKeyP()
    {
        int spawn = Random.Range(0, spawnLocationsP.Length);
        GameObject.Instantiate(key, spawnLocationsP[spawn].transform.position, spawnLocationsP[spawn].transform.rotation);
    }
    private void SpawnKeyB()
    {
        int spawn = Random.Range(0, spawnLocationsB.Length);
        GameObject.Instantiate(key, spawnLocationsB[spawn].transform.position, spawnLocationsB[spawn].transform.rotation);
    }
}