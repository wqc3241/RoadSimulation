using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedReportor : MonoBehaviour {


    private Rigidbody rgbd;
	// Use this for initialization
	void Start () {
        rgbd = GetComponent<Rigidbody>(); 
	}

    private void FixedUpdate()
    {
        if (UI_SpeedDisplay.UI_SD && rgbd)
        {
            UI_SpeedDisplay.UI_SD.updateSpeed(rgbd.velocity.magnitude);
        }
    }

}
