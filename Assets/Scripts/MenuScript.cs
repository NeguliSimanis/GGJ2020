using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onPlay()
    {
        SceneManager.LoadScene("Scene1");
    }

    public void onOptions()
    {
        SceneManager.LoadScene("Options");
    }

    public void onExit()
    {
        Application.Quit();
    }

    public void toMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
