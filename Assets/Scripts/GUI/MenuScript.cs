using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioSource ac;

    bool reduceVolume = false;

    void Start()
    {
        Cursor.visible = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (reduceVolume)
        {
            ReduceVolume();
        }
    }

    public void onPlay()
    {
        SceneChanger.instance.LoadLevelAfterFade("2_Intro");
        //StartCoroutine(SceneLoading("2_Intro"));
        ////SceneManager.LoadScene("2_Intro");
        //reduceVolume = true;
    }

    public void onOptions()
    {
        SceneChanger.instance.LoadLevelAfterFade("5_Options");
        //StartCoroutine(SceneLoading("5_Options"));
        //reduceVolume = true;
    }

    public void onExit()
    {
        Application.Quit();
    }

    public void toMainMenu()
    {
        SceneChanger.instance.LoadLevelAfterFade("1_MainMenu");
        //StartCoroutine(SceneLoading("1_MainMenu"));
        //reduceVolume = true;
    }


    IEnumerator SceneLoading(string Scene)
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(Scene);
    }

    void ReduceVolume()
    {
        if(ac.volume != 0)
        {
            ac.volume -= Time.deltaTime;
        }
    }

}
