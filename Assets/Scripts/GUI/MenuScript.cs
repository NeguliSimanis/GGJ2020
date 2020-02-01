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
        StartCoroutine(SceneLoading("2_Intro"));
        //SceneManager.LoadScene("2_Intro");
    }

    public void onOptions()
    {
        StartCoroutine(SceneLoading("5_Options"));
    }

    public void onExit()
    {
        Application.Quit();
    }

    public void toMainMenu()
    {
        StartCoroutine(SceneLoading("1_MainMenu"));
    }


    IEnumerator SceneLoading(string Scene)
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(Scene);
    }
}
