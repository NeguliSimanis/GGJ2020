using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GodScene : MonoBehaviour
{
    public static GodScene instance;

    [SerializeField]
    private GameObject GodSceneTextObject;
    private int godSceneID = 0;
    private string godSceneName;
    private bool isInGodScene = true;
    private Text godSceneText;
    private int nextGodTextID = 0;
    private string[] godTexts =
    {
        "You have two choices, mortal",
        "One - perish in the dark abyss,",
        "Two - bring me what I desire",
        "and your vessel shall be repaired"
    };

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(this.gameObject);
        godSceneName = SceneManager.GetActiveScene().name;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("NEW SCENE");
        if (godSceneID > 0)
            Debug.Log("SHOULD SKIP INTRO");
        godSceneID++;
        if (SceneManager.GetActiveScene().name != godSceneName)
            isInGodScene = false;
        else
            isInGodScene = true;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    private void Start()
    {
        DisplayNextGodText();
    }

    private void Update()
    {
        if (Input.anyKeyDown && isInGodScene)
        {
            DisplayNextGodText();
        }
    }

    private void DisplayNextGodText()
    {
        if (nextGodTextID == 0)
        {
            InstantiateGodText();
            godSceneText.text = godTexts[0];
        }
        else if (nextGodTextID == godTexts.Length)
        {
            ShowGodQuest();
            return;
        }
        else
        {
            godSceneText.text = godTexts[nextGodTextID];
        }
        nextGodTextID++;
    }

    private void ShowGodQuest()
    {
        LoadNextLevel();
    }

    private void InstantiateGodText()
    {
        GameObject newGodTextObject = 
            Instantiate(GodSceneTextObject, GameObject.FindGameObjectWithTag("GodCanvas").transform);
        godSceneText = newGodTextObject.GetComponent<Text>();
    }

    private void LoadNextLevel()
    {
        SceneChanger.instance.LoadLevelAfterFade("4_Game");
    }
}
