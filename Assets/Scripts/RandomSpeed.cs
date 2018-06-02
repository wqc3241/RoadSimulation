using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpeed : MonoBehaviour {

    //Set these Textures in the Inspector
    public Material[] m_MainTexture;
    Renderer m_Renderer;



    // Use this for initialization
    void Start()
    {
        //Fetch the Renderer from the GameObject
        m_Renderer = GetComponent<Renderer>();
        
        m_Renderer.material = m_MainTexture[Random.Range(0, m_MainTexture.Length)];
    }

    // Update is called once per frame
    void Update () {
		
	}
}
