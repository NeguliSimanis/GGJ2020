using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages
/// - scene loading
/// - scene change animationS
/// - BG music changes
/// </summary>
public class SceneChanger : MonoBehaviour
{
    string mainMenuSceneName = "1_MainMenu";
    string introSceneName = "2_Intro";
    string godSceneName = "3_GodScreen";
    string gameplaySceneName = "4_Game";
    string defeatSceneName = "7_GameLose";
    string victorySceneName = "6_GameWin";
    string creditsSceneName = "5_Options";

    #region AUDIO DATA
    private float musicFadeDuration = 2f;
    AudioSource audioSource;
    [SerializeField]
    AudioSource sfxAudioSource;
    [SerializeField]
    AudioClip mainMenuMusic;
    [SerializeField]
    AudioClip godSceneMusic;
    [SerializeField]
    AudioClip gamePlayMusic;
    [SerializeField]
    AudioClip victoryMusic;
    [SerializeField]
    AudioClip defeatMusic;
    #endregion

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

    // called before start on every scene load
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("LOADED " + SceneManager.GetActiveScene().name);
        ManageBgMusicChange();
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        
        fadeAnimator = gameObject.GetComponent<Animator>();
    }

    #region SCENE LOAD METHODS
    public void LoadLevelAfterFade(string levelName)
    {
        if (isFadingOut)
            return;
        isFadingOut = true;
        levelToLoadNext = levelName;
        fadeAnimator.SetTrigger("out");
    }

    public void LoadLevel()
    {
        isFadingOut = false;
        Debug.Log("LOADING ! " + levelToLoadNext);
        fadeAnimator.SetTrigger("in");
        SceneManager.LoadScene(levelToLoadNext);
    }
    #endregion

    #region BACKGROUND MUSIC METHODS
    private void ManageBgMusicChange()
    {
        if (audioSource == null)
            audioSource = gameObject.GetComponent<AudioSource>();
        string currSceneName = SceneManager.GetActiveScene().name;
        if (currSceneName == mainMenuSceneName)
        {
            SetBgMusic(mainMenuMusic);
        }
        else if (currSceneName == introSceneName)
        {

        }
        else if (currSceneName == godSceneName)
        {
            StartCoroutine(FadeToOtherBackgroundMusic(godSceneMusic));
        }
        else if (currSceneName == gameplaySceneName)
        {
            StartCoroutine(FadeToOtherBackgroundMusic(gamePlayMusic));
        }
        else if (currSceneName == defeatSceneName)
        {
            PlayAudioOneShot(defeatMusic);
            StartCoroutine(FadeToOtherBackgroundMusic(mainMenuMusic));
        }
        else if (currSceneName == victorySceneName)
        {
            StartCoroutine(FadeToOtherBackgroundMusic(victoryMusic));
        }
        else if (currSceneName == creditsSceneName)
        {

        }
    }

    private IEnumerator FadeToOtherBackgroundMusic(AudioClip targetMusic)
    {
        float startVolume = audioSource.volume;
        float volumeChangeIncrement = startVolume * 0.01f;
        float halfFadeDuration = musicFadeDuration * 0.5f;
        float fadeIncrement = halfFadeDuration * 0.01f;

        // fade out current music
        while (audioSource.volume > 0)
        {
            audioSource.volume -= volumeChangeIncrement;
            yield return new WaitForSeconds(fadeIncrement);
        }

        // switch audio clips
        SetBgMusic(targetMusic);

        // fade in new music
        while (audioSource.volume < startVolume)
        {
            audioSource.volume += volumeChangeIncrement;
            yield return new WaitForSeconds(fadeIncrement);
        }
    }

    private void SetBgMusic(AudioClip music)
    {
        audioSource.clip = music;
        audioSource.Play();
    }

    public void PlayAudioOneShot(AudioClip audioClip)
    {
        sfxAudioSource.PlayOneShot(audioClip);
    }
    #endregion

}
