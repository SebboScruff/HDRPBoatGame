using System.Collections;
using System.Collections.Generic;
using UnityEngine;

                //          ***UNUSED***

public class NPCSpawnManager : MonoBehaviour
{
    public GameObject[] npcBoats = new GameObject[2];
    public GameObject[] spawnPoints = new GameObject[5];

    public int spawnFrequency = 5;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnBoats", 0f, spawnFrequency);
    }

    void SpawnBoats()
    {
        int npcBoatIndex = Random.Range(0, 2);
        int spawnPointIndex = Random.Range(0, 5);
        int yRotation = Random.Range(0, 360);

        Instantiate(npcBoats[npcBoatIndex], spawnPoints[spawnPointIndex].transform.position, Quaternion.Euler(0f, yRotation, 0f));
    }
}
