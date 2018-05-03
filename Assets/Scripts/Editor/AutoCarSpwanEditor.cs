using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AutoCarSpawner))]
public class AutoCarSpwanEditor : Editor
{
    private void OnSceneGUI()
    {
        AutoCarSpawner rf = (AutoCarSpawner)target;
        Handles.color = Color.white;
    }
}
