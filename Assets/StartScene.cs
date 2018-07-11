using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour {
    public string sceneName = "Scene_Yubing_stright";
    public void StartSimulatorScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
