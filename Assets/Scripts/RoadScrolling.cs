using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadScrolling : MonoBehaviour {

    public float moveSpeed;
    public float tileSizeZ;

    public Canvas loopNumber;
    private int loop;

    private Vector3 startPosition;

    // Use this for initialization
    void Start () {
        startPosition = transform.position;
        loop = 0;
    }
	
	// Update is called once per frame
	void Update () {
        float newPosition = Mathf.Repeat(Time.time * moveSpeed, tileSizeZ);
        transform.position = startPosition + Vector3.back * newPosition;
        if (transform.position == startPosition)
        {
            loop++;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            moveSpeed = moveSpeed - ToSingle(0.2);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveSpeed = moveSpeed + ToSingle(0.2);
        }
	}
    
    public void AddLoop(int newLoop)
    {
        loop++;
        UpdateLoop();
    }

    void UpdateLoop()
    {
    }

    public static float ToSingle(double value)
    {
        return (float)value;
    }
}
