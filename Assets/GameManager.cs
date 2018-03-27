using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager GM;

    [SerializeField] private GameObject rainyPrefab;
    [SerializeField] private GameObject snowPrefab;
    [SerializeField] private GameObject windyPrefab;
    [SerializeField]  private WeatherType testWeather;
    [SerializeField]  private RoadType testRoad;

    public enum WeatherType {Sunny, Rainy, Snow, Windy};
    public enum RoadType {Highway, Local, Rural};


    private void Awake()
    {
        if (GM == null)
        {
            GM = this;
            DontDestroyOnLoad(this.gameObject);

        }
        else if (GM != this)
        {
            Destroy(this);
        }

        //default test choice in menu scene 
        if (getCurScene().buildIndex == 0)
        {
            setTestWeaterType(WeatherType.Sunny);
            setTestRoadType(RoadType.Highway);
        }
    }
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void setTestWeaterType(WeatherType w)
    {
        //only allow modify test type in menu scene
        if (getCurScene().buildIndex != 0)
        {
            return;
        }
            testWeather = w;
    }

    public void setTestRoadType(RoadType r)
    {
        if (getCurScene().buildIndex != 0)
        {
            return;
        }

        testRoad = r;
    }

    public Scene getCurScene()
    {
        return SceneManager.GetActiveScene();
    }

    public void loadNextScene()
    { 
        if (getCurScene().buildIndex == 0 &&
            testWeather == WeatherType.Sunny &&
            testRoad == RoadType.Highway)
        {
            //temperal scene name
            SceneManager.LoadScene("Scene_Yubing");
        }
    }

    public void loadCustomedScene(int buildIndex = 1)
    {
        if (getCurScene().buildIndex == 0 && buildIndex == 1)
        {
            if (testRoad == RoadType.Highway)
            {

            }

            if (testWeather == WeatherType.Sunny)
            {

            }
            else if (testWeather == WeatherType.Rainy)
            {
             
            }
        }
    }
}
