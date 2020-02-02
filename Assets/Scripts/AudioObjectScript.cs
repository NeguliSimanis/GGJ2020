using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioObjectScript : MonoBehaviour
{
    public static AudioObjectScript instance;
    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake()
    {
        if (AudioObjectScript.instance == null)
            AudioObjectScript.instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(this.gameObject);
    }
}
