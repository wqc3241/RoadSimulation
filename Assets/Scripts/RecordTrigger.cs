using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class RecordTrigger : MonoBehaviour {

    private string outputPath;


    private bool recording = false;
    private GameObject playerCar;
    private string zoneName;
    private Rigidbody carRB;
    private int outputCount = 1;

    //public enum outputType {speed, wheel, brake, accelerator};
    //startTime : list of speed change at each delta time after start time

    //record the speed, wheel, accelerator, brake

    private Dictionary<float, double> speedData = new Dictionary<float, double>();
    private Dictionary<string, List<List<float>>> zoneData = new Dictionary<string, List<List<float>>>();

    //output order:time speed, wheel, brake, acceleration
    private List<List<object>> allData = new List<List<object>>();


    float h, v, b;

        // Use this for initialization
    void Awake()
    {
        //playerCar = GameObject.FindGameObjectWithTag("Player");
        //Debug.Log(playerCar);

        carRB = gameObject.GetComponent<Rigidbody>();
        if (carRB == null)
            Debug.LogError("Error no rigbidbody attached to parent object");
        //Debug.Log(zoneData.Count);

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

    private void Update()
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

            h = Mathf.Abs(rec.lX - 32767);
            v = Mathf.Abs(rec.lY - 32767);
            b = -Mathf.Abs(rec.lRz - 32767);

            //Debug.Log(h + " "+ v +" " + b);
        }

        if (recording && carRB)
        {
            allData.Add(new List<object> { getZone(zoneName), getTime(), getSpeed(), getWheel(h), getBrake(b), getAcceleration(v), getPositionX(), getPositionZ() });
            //Debug.Log(allData.Count);

        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            storeData();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            foreach (var item in allData)
                item.Clear();

            allData.Clear();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }

    private void storeData()
    {


        recording = false;

        outputPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        //only perform at the end of the driving test

        //outputPath = Application.dataPath + "/Data";
        string path = outputPath + "/Data";
        string filename = "recordData" + outputCount.ToString() + "--" + System.DateTime.Now.ToString().Replace('/', '-').Replace(' ', '-').Replace(':', '-') + ".txt";
        path += filename;
        Debug.Log(path);

        outputCount++;

        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine("Zone        " + "Time        " + "Speed       " + "Wheel       " + "Brake       " + "Acceleration " + "X           " + "Z ");

        foreach ( List<object> entry in allData)
        {
            string tempOut = "";
            foreach ( object record in entry)
            {
                    tempOut += record.ToString() + "        ";
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
        return (float)Math.Round((double)Time.time, 4);
    }

    public float getSpeed()
    {
        //default velocity is m/s
        float mph = carRB.velocity.magnitude * 2.23693629f * 20 / 10f;
        return (float)Math.Round((double)mph, 2);
    }

    public float getWheel(float angle)
    {
        return ((float)Math.Round(angle / 65534  *100* 100f) / 100f) - 50;
    }

    public float getBrake(float brake)
    {
        return ((float)Math.Round(brake/65534*100*100f)/100f);
    }

    public float getAcceleration(float acc)
    {
        return ((float)Math.Round(acc/65534*100*100f)/100f);
    }

    public float getPositionX()
    {
        float X = carRB.transform.position.x;
        return (float)Math.Round(X*100f)/100f;
    }

    public float getPositionZ()
    {
        float Z = (float)Math.Round(carRB.transform.position.z*100f)/100f;
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
        storeData();
        Debug.Log("DataStored");
    }

    public string getZone(string name)
    {

        return name;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "electricalSign" || other.tag == "staticSign")
        {
            //Debug.Log(other.name);
            zoneName = other.name;
            Awake();
            startRecord();
        }
        else if (other.tag == "Finishline")
        {
            storeData();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }


    void OnTriggerExit(Collider other)
    {
        if(other.tag == "electricalSign" || other.tag == "staticSign") 
        {
            recording = false;

                //Debug.Log(zoneData.Count());

        }
    }
}

