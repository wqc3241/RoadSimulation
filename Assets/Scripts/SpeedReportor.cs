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
        //Debug.Log(rgbd.velocity.magnitude * 2.23694);
        if (UI_SpeedDisplay.UI_SD && rgbd)
        {
            UI_SpeedDisplay.UI_SD.updateSpeed(rgbd.velocity.magnitude * 13/10);
        }
    }

}
