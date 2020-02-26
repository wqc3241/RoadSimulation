using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCarTexture : MonoBehaviour {

    public Material BodyMaterial;

	// Use this for initialization
	void Start () {
        //BodyMaterial.color = new Color(Random.value, Random.value, Random.value, 1.0f);
    }

	// Update is called once per frame
	void Update () {
		
	}

    public void SetColor()
    {
       // BodyMaterial = GetComponent<Material>();
       // BodyMaterial.color = new Color(Random.value, Random.value, Random.value, 1.0f);
    }
}
