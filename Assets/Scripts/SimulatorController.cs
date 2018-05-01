using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulatorController : MonoBehaviour {

    public Camera mainCamera;
    public Camera firstPerson;
    public GameObject car;
    public GameObject road;
    public GameObject recordArea;

    public int RandomCarsNumber;
    public GameObject RandomCars;
    public Vector3 spawnValues;

    public float spawnWait;
    public float startWait;
    public float waveWait;

    float start_pos_z;
    float road_pos_z;
    float car_pos_z;

    float roadArea_pos_z;
    public float roadArea_scale_z;

    private GameObject instantiatedRoadSign;

    // Use this for initialization
    void Start()
    {
        mainCamera.enabled = false;
        firstPerson.enabled = true;
        start_pos_z = car.transform.position.z;
        road_pos_z = road.transform.position.z;
        StartCoroutine(SpawnCars());
        SpawnRecordArea();
        recordArea.transform.position = new Vector3(1.5f, -3.5f, 50f);
    }
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Space))
        {
            mainCamera.enabled = !mainCamera.enabled;
            firstPerson.enabled = !firstPerson.enabled;
        }
        var car_pos = car.transform.position;
        if (car_pos.z > 470.0)
        {
            Destroy(instantiatedRoadSign);
            car_pos.z = start_pos_z;
            car.transform.position = car_pos;
            SpawnRecordArea();
        }

        if (car_pos.z < -24.0)
        {
            car_pos.z = ToSingle(470.0);
            car.transform.position = car_pos;
        }

    }

    public static float ToSingle(double value)
    {
        return (float)value;
    }

    IEnumerator SpawnCars()
    {
        for (int i = 0; i < RandomCarsNumber; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(ToSingle(-3.3), ToSingle(-1.5)), spawnValues.y, spawnValues.z);
            Quaternion spawnRotation = Quaternion.identity;
            spawnRotation.eulerAngles = new Vector3(0, 180, 0);
            Instantiate(RandomCars, spawnPosition, spawnRotation);
            Debug.Log(RandomCars.transform.position);
            yield return new WaitForSeconds(spawnWait);
        }
        yield return new WaitForSeconds(waveWait);
    }

    bool CarPositionReset(GameObject car)
    {
        return car.transform.position.z == 470;
    }

    void SpawnRecordArea()
    {
        Vector3 spawnPosition = new Vector3(0f, 0.75f, Random.Range(0.0f, 450.0f));
        Quaternion spawnRotation = Quaternion.identity;
        spawnRotation.eulerAngles = new Vector3(-90, 0, 0);
        instantiatedRoadSign = Instantiate(recordArea, spawnPosition, spawnRotation);
    }
}
