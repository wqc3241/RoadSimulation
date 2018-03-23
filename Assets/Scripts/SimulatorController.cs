using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulatorController : MonoBehaviour {

    public Camera mainCamera;
    public Camera firstPerson;
    public Object car;

    // Use this for initialization
    void Start () {
        mainCamera.enabled = false;
        firstPerson.enabled = true;

	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKey(KeyCode.Space))
        {
            mainCamera.enabled = !mainCamera.enabled;
            firstPerson.enabled = !firstPerson.enabled;
        }
	}
}
