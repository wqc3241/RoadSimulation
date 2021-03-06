﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UI_SpeedDisplay : MonoBehaviour {

    public static UI_SpeedDisplay UI_SD;

    private Text speedText;

    private void Awake()
    {
        if (UI_SD == null)
            UI_SD = this;
        else if (UI_SD != this)
            Destroy(this);

        speedText = GetComponentInChildren<Text>();

        if (speedText == null)
            Debug.LogError("Cannot find speed Text in child");
    }
	

    public void updateSpeed(float speed)
    {
        //float mph = rigidbody.velocity.magnitude * 2.237;
        int mph = (int)System.Math.Round(speed * 2.23693629f, 2);

        speedText.color = Color.red;
        speedText.text = mph.ToString() + " MPH";

    }
}
