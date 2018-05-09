using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RoadGenerator))]
public class RoadGeneratorEditor : Editor {
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        RoadGenerator rg = (RoadGenerator)target;

        if (GUILayout.Button("BuildRoad"))
        {
            rg.buildRoad();
        }
    }
}
