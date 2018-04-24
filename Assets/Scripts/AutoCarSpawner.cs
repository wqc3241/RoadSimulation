using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCarSpawner : MonoBehaviour {

    public GameObject autoCarPrefab;
    public float spawnInernalMaxTime = 10f;
    public float spawnInernalMinTime = 2.0f;

    private float SpawnTimeCount = 0;
    private float waitTime = 0; 
    // Use this for initialization
    void Start ()
    {
        rollNewWaitTime();
    }
	
	// Update is called once per frame
	void Update ()
    {
        SpawnTimeCount += Time.deltaTime;


        if (SpawnTimeCount > waitTime)
        {
            SpawnTimeCount = 0;
            rollNewWaitTime();
            spawnCar();
        }
    }


    void rollNewWaitTime()
    {
        waitTime = Random.Range(spawnInernalMinTime, spawnInernalMaxTime);
    }

    void spawnCar()
    {
        if (autoCarPrefab == null)
        {
            return;
        }

        Instantiate(autoCarPrefab);
    }
}
