using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityStandardAssets.CrossPlatformInput;

public class RecordTrigger : MonoBehaviour {

    public string outputPath;


    private bool recording = false;
    private GameObject playerCar;
    private Rigidbody carRB;
    private int outputCount = 1;

    //public enum outputType {speed, wheel, brake, accelerator};
    //startTime : list of speed change at each delta time after start time

    //record the speed, wheel, accelerator, brake
    private Dictionary<float, List<float>> speedRecords;
    private Dictionary<float, double> speedData = new Dictionary<float, double>();

    //output order:time speed, wheel, brake, acceleration
    private List<List<float>> allData = new List<List<float>>();

    float h, v, b;

        // Use this for initialization
    void Awake()
    {
        playerCar = GameObject.FindGameObjectWithTag("Player");
        //Debug.Log(playerCar);

        carRB = playerCar.GetComponent<Rigidbody>();
        if (carRB == null)
            Debug.LogError("Error no rigbidbody attached to parent object");


    }

    // Update is called once per frame
    /*	void Update ()
        {

            if (recording)
                storeRecord();
            else
                startRecord();

        }
        */

    private void FixedUpdate()
    {
        if (LogitechGSDK.LogiIsConnected(0) == false)
        {
            h = CrossPlatformInputManager.GetAxis("Horizontal");
            v = CrossPlatformInputManager.GetAxis("Vertical");
            b = CrossPlatformInputManager.GetAxis("Vertical");
        }
        else
        {
            LogitechGSDK.DIJOYSTATE2ENGINES rec;
            rec = LogitechGSDK.LogiGetStateUnity(0);


            // pass the input to the car!
            //h = CrossPlatformInputManager.GetAxis("Horizontal");

            h = CrossPlatformInputManager.GetAxis("Horizontal");
            v = Mathf.Abs(rec.lY - 32767);
            b = -Mathf.Abs(rec.lRz - 32767);

            //Debug.Log(h + " "+ v +" " + b);
        }

        if (recording && carRB)
        {
            allData.Add(new List<float> { getTime(), getSpeed(), getWheel(h), getBrake(b), getAcceleration(v), getPositionX(), getPositionY() });
        }
    }

    private void storeData()
    {
        recording = false;

        //only perform at the end of the driving test
        string path = outputPath;
        string filename = "recordData" + outputCount.ToString() + "--" + System.DateTime.Now.ToString().Replace('/', '-').Replace(' ', '-').Replace(':', '-') + ".txt";
        path += filename;
        Debug.Log(path);

        outputCount++;

        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine("Time        " + "Speed       " + "Wheel       " + "Brake       " + "Acceleration " + "X           " + "Z ");

        foreach (List<float> row in allData)
        {
            string tempOut = "";
            foreach (float record in row)
            {
                tempOut += record.ToString("0.00") + "        ";
            }
            writer.WriteLine(tempOut);
        }

        writer.Close();


        //clean datas
        foreach (var item in allData)
            item.Clear();

        allData.Clear();
    }

    /// <summary>
    /// internal usage for fetching driving information
    /// To Implement following function after equipment set up
    /// <returns></returns>
    public float getTime()
    {
        return (float)Math.Round((double)Time.time, 2);
    }

    public float getSpeed()
    {
        //default velocity is m/s
        float mph = carRB.velocity.magnitude * 2.237f;
        return (float)Math.Round((double)mph, 2);
    }

    public float getWheel(float angle)
    {
        return (angle*45);
    }

    public float getBrake(float brake)
    {
        return (brake/65534*100);
    }

    public float getAcceleration(float acc)
    {
        return (acc/65534*100);
    }

    public float getPositionX()
    {
        float X = carRB.transform.position.x;
        return X;
    }

    public float getPositionZ()
    {
        float Z = carRB.transform.position.z;
        return Z;
    }


    /// <summary>
    /// public method for other script to called to store or start record data
    /// </summary>
    public void startRecord()
    {
        if (recording)
        {
            return;
        }

        Debug.Log("Recording");
        recording = true;
    }

    public void storeRecord()
    {
        if (!recording)
        {
            return;
        }

        storeData();
        Debug.Log("DataStored");
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerCar = other.gameObject;
            Awake();
            startRecord();
        }
    }

 /*   void OnTriggerStay(Collider other)
    {

    }
 */

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player") {
            storeRecord();
        }
    }
}

