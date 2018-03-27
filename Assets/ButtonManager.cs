using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonManager : MonoBehaviour {

    [SerializeField] private List<GameObject> childButton;
	// Use this for initialization
	void Start ()
    {
		for (int i = 0; i < transform.childCount; ++i)
        {
            childButton.Add(transform.GetChild(i).gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void deActiveAllChild()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            childButton[i].GetComponent<TestTypeButton>().setActive(false);
        }
    }

    public void updateTestType(string name)
    {
        GameManager.WeatherType.Rainy.ToString();

        if (name == "Highway")
        {
            GameManager.GM.setTestRoadType(GameManager.RoadType.Highway);
        }
        else if (name == "Rural")
        {
            GameManager.GM.setTestRoadType(GameManager.RoadType.Rural);

        }
        else if (name == "Local")
        {
            GameManager.GM.setTestRoadType(GameManager.RoadType.Local);
        }
        else if (name == "Snow")
        {
            GameManager.GM.setTestWeaterType(GameManager.WeatherType.Snow);
        }
        else if (name == "Rainy")
        {
            GameManager.GM.setTestWeaterType(GameManager.WeatherType.Rainy);

        }
        else if (name == "Windy")
        {
            GameManager.GM.setTestWeaterType(GameManager.WeatherType.Windy);
        }
        else if (name == "Sunny")
        {
            GameManager.GM.setTestWeaterType(GameManager.WeatherType.Sunny);
        }
    }
}
