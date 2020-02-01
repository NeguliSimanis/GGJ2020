using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onPlay()
    {
        SceneChanger.instance.LoadLevelAfterFade("2_Intro");
        //SceneManager.LoadScene("2_Intro");
    }

    public void onOptions()
    {
        SceneManager.LoadScene("4_Options");
    }

    public void onExit()
    {
        Application.Quit();
    }

    public void toMainMenu()
    {
        SceneManager.LoadScene("1_MainMenu");
    }
}
