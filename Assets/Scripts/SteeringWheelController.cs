using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringWheelController : MonoBehaviour {

    void Start()
    {
        transform.localRotation = Quaternion.identity;
        transform.localPosition = Vector3.zero;
    }

    void Update()
    {
        transform.localEulerAngles = new Vector3(0, Input.GetAxis("Horizontal") * 180, 0);
    }
}
