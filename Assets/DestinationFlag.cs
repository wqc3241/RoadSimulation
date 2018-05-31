using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DestinationFlag : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay(Collider other)
    {

        if (other.transform.GetComponent<AutoMoveCar>() != null)
        {
            Destroy(other.gameObject);
        }
    }
}
