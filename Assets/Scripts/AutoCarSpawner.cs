using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCarSpawner : MonoBehaviour {

    //public List<GameObject> autoCarList;
    //    public List<GameObject> destinationList;

    public GameObject autoCarPrefab;
    //public List<GameObject> destinationFlagObject;
    public float spawnInernalMaxTime = 5.0f;
    public float spawnInernalMinTime = 1.0f;

    [Tooltip("All Spped are in MPH")]
    public float minSpeed = 55.0f;
    public float maxSpeed = 85.0f;
    public float speedDiff = 10.0f;

    [Tooltip("Number of Car will be spawned in total")]
    public int spwawnAmount = 5;
    
    [Tooltip("Only active when before car can see the spawned car")]
    public bool active = false;
    [Tooltip("chase mean car spwaned behind player, otherwise spawn in front of player")]
    public bool chaseMode = false;

    private float SpawnTimeCount = 0;
    private float waitTime = 0;
    [SerializeField]
    private float spawnSpeed = 0;

    // Use this for initialization
    void Start ()
    {
        rollNewWaitTime();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (active)
        {
            SpawnTimeCount += Time.deltaTime;
            if (SpawnTimeCount > waitTime)
            {
                SpawnTimeCount = 0;
                rollNewWaitTime();
                spawnCar();
            }
        }


        if (spwawnAmount <= 0)
        {
            active = false;
        }
    }


    void rollNewWaitTime()
    {
        waitTime = Random.Range(spawnInernalMinTime, spawnInernalMaxTime);
    }

    void spawnCar()
    {
        //autoCarPrefab = autoCarList[0];

        if (autoCarPrefab == null)
        {
            return;
        }

        AutoMoveCar insCar =  Instantiate(autoCarPrefab, transform).GetComponent<AutoMoveCar>();
        insCar.transform.parent = null;
        spwawnAmount--;
        insCar.SetSpeed(spawnSpeed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!chaseMode && other.CompareTag("Player"))
        {
            active = true;
            //car in 
            DataRecorder dr = other.GetComponentInChildren<DataRecorder>();
            if (dr)
            {
                if (dr.getSpeed() - speedDiff < minSpeed)
                {
                    spawnSpeed = minSpeed;
                }
                else
                {
                    spawnSpeed = dr.getSpeed() - speedDiff;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (chaseMode && other.CompareTag("Player"))
        {
            active = true;
            //car in 
            DataRecorder dr = other.GetComponentInChildren<DataRecorder>();
            if (dr)
            {
                if (dr.getSpeed() + speedDiff > maxSpeed)
                {
                    spawnSpeed = maxSpeed;
                }
                else
                {
                    spawnSpeed = dr.getSpeed() + speedDiff;
                }
            }
        }
    }
}
