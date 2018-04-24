using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedDetector : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

            if (rb)
            {
                GetComponentInParent<ElectronicRoadSign.ElectronicRoadSignScript>().MessageText =
                other.gameObject.GetComponentInChildren<DataRecorder>().getSpeed().ToString();
            }
            Debug.Log("Dector car");
        }
    }
}
