using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    public Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * Random.Range(10,50);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
