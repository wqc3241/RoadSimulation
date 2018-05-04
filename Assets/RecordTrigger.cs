using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordTrigger : MonoBehaviour {

    void onTriggerEnter(Collider other)
    {
/*        if (other.tag == "Player")
        {
            Debug.Log("Object Entered the tirgger");
        }
        */
    }

    void onTriggerStay(Collider other)
    {
        Debug.Log("object stay");
    }

    void onTriggerExit(Collider other)
    {
        Debug.Log("object exit");
    }
}

