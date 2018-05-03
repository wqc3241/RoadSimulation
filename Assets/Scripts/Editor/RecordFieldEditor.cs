using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (RecordField))]
public class RecordFieldEditor : Editor
{
    private void OnSceneGUI()
    {
        RecordField rf = (RecordField)target;
        Handles.color = Color.white;
        Vector3 topLeft = new Vector3(rf.centorPos.x - rf.width / 2, rf.centorPos.y, rf.centorPos.z + rf.length / 2);
        Vector3 topRight = new Vector3(rf.centorPos.x + rf.width / 2, rf.centorPos.y, rf.centorPos.z + rf.length / 2);
        Vector3 buttomLeft = new Vector3(rf.centorPos.x - rf.width / 2, rf.centorPos.y, rf.centorPos.z - rf.length / 2);
        Vector3 buttomRight = new Vector3(rf.centorPos.x + rf.width / 2, rf.centorPos.y, rf.centorPos.z - rf.length / 2);

        Handles.DrawLine(topLeft, topRight);
        Handles.DrawLine(topLeft, buttomLeft);
        Handles.DrawLine(topRight, buttomRight);
        Handles.DrawLine(buttomRight, buttomLeft);
    }
}
