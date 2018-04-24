using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour
{
    public float delay = 15; //This implies a delay of 2 seconds.

    void Start()
    {
        Destroy(gameObject,delay);
    }
}
