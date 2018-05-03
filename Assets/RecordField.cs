using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordField : MonoBehaviour {

    public float length;
    public float width;
    public Vector3 centorPos;

    private BoxCollider bc;

    private void Awake()
    {
        centorPos = transform.position;

        bc = GetComponentInParent<BoxCollider>();

        if (bc)
        {
            length = bc.size.y;
            width = bc.size.z;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
