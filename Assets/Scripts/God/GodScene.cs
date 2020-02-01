using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            LoadNextLevel();
        }
    }

    private void LoadNextLevel()
    {
        SceneChanger.instance.LoadLevelAfterFade("4_Game");
    }
}
