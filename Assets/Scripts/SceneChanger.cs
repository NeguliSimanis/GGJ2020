using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger instance;

    Animator fadeAnimator;
    bool fadedIn = false;
    bool isFadingOut = false;
    private string levelToLoadNext;

    void Awake()
    {
        if (SceneChanger.instance == null)
            SceneChanger.instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(this.gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
    }

    void OnDisable()
    {
        isFadingOut = false;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        fadeAnimator = gameObject.GetComponent<Animator>();
    }

    public void LoadLevelAfterFade(string levelName)
    {
        //if (isFadingOut)
        //    return;
        isFadingOut = true;
        levelToLoadNext = levelName;
        //fadeAnimator.SetBool("fadeOut", true);
        fadeAnimator.SetTrigger("out");
    }

    public void LoadLevel()
    {

        
        //fadeAnimator.SetBool("fadeOut", false);
        //fadeAnimator.SetBool("fadeIn", true);
        fadeAnimator.SetTrigger("in");
        SceneManager.LoadScene(levelToLoadNext);
    }

}
