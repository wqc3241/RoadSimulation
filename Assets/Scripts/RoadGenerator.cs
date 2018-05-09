using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour {

    public GameObject exitPrefab;
    public GameObject straightPrefab;
    public Transform roadHoder;
    public float exitRoadLength;
    public float straightRoadLength;
    public int strightRoadCount;
    public float totalRoadLenght;

    private new Vector3 headPos; 
	// Use this for initialization
	public void buildRoad ()
    {
        headPos = transform.position;
	    if (exitPrefab == null || straightPrefab == null)
        {
            Debug.LogError("Missing road prefab");
        }

        createNewRoad(exitPrefab, headPos);
        headPos = new Vector3(headPos.x, headPos.y, headPos.z + exitRoadLength);

        for (int i = 0; i < strightRoadCount; ++i)
        {
            createNewRoad(straightPrefab, headPos);
            headPos = new Vector3(headPos.x, headPos.y, headPos.z + straightRoadLength);
        }

    }


    void createNewRoad(GameObject prefab, Vector3 headPos)
    {
        if (roadHoder == null)
            Instantiate(prefab, headPos, Quaternion.identity);
        else
            Instantiate(prefab, headPos, Quaternion.identity, roadHoder);

    }

    // Update is called once per frame
    void Update () {
		
	}
}
