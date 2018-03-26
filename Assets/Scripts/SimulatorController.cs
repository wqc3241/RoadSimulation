using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulatorController : MonoBehaviour {

    public Camera mainCamera;
    public Camera firstPerson;
    public GameObject car;
    public GameObject road;

    float start_pos_z;
    float road_pos_z;
    float car_pos_z;

    // Use this for initialization
    void Start()
    {
        mainCamera.enabled = false;
        firstPerson.enabled = true;
        start_pos_z = car.transform.position.z;
        road_pos_z = road.transform.position.z;

    }
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Space))
        {
            mainCamera.enabled = !mainCamera.enabled;
            firstPerson.enabled = !firstPerson.enabled;
        }
        var car_pos = car.transform.position;
        if (car_pos.z > 470.0)
        {
            car_pos.z = start_pos_z;
            car.transform.position = car_pos;
        }

        if (car_pos.z < -24.0)
        {
            car_pos.z = ToSingle(470.0);
            car.transform.position = car_pos;
        }

    }

    public static float ToSingle(double value)
    {
        return (float)value;
    }
}
