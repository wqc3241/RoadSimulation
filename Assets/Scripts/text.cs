﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class text : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject text = new GameObject();
        TextMesh t = text.AddComponent<TextMesh>();
        t.text = "Speed Limit";
        t.fontSize = 200;

        t.transform.localEulerAngles = new Vector3(90, 0, 0);
        t.transform.localPosition = new Vector3(56f, 3f, 40f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
