using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodScene : MonoBehaviour
{
    public static GodScene instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);    
    }

    private void Start()
    {
        Debug.Log("I AM GOD");

    }
}
