using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject objectPrefab;

    public Transform[] spawnPoints;

    void Start()
    {
        SpawnObjects();
    }

    void SpawnObjects()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            Instantiate(objectPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}
