using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class DataRecorder : MonoBehaviour {

    public string outputPath = "Assets/Data/speedData.txt";
    public bool recording = true;

    private Rigidbody carRB;
    
    //startTime : list of speed change at each delta time after start time
    private Dictionary<float, List<float>> speedRecords;
    private Dictionary<float, double> speedData = new Dictionary<float, double>();

	// Use this for initialization
	void Awake ()
    {
        carRB = GetComponentInParent<Rigidbody>();

        if (carRB == null)
            Debug.LogError("Error no rigbidbody attached to parent object");
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (Input.GetKeyDown("q"))
        {
            storeData();
        }
	}

    private void FixedUpdate()
    {
        if (recording && carRB)
        {
            float mph = carRB.velocity.magnitude * 2.237f;
            
            speedData.Add((float)Math.Round((double)Time.time, 2), (float)Math.Round((double)mph, 2) );
        }
    }


    private void storeData()
    {
        recording = false;
        //only perform at the end of the driving test
        string path = outputPath;
        StreamWriter writer = new StreamWriter(path, true);
        foreach (KeyValuePair<float, double> speed in speedData)
        {
            writer.WriteLine(speed.Key.ToString("0.00") + "         "  + speed.Value.ToString("0.00"));
        }

        writer.Close();
    }
}
