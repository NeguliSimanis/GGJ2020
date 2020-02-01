using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GodScene : MonoBehaviour
{
    public static GodScene instance;

    #region SCENE DATA
    private int godSceneID = 0;
    private string godSceneName;
    private bool isInGodScene = true;
    #endregion

    #region QUEST GENERATION DATA
    public List<QuestItem> currentQuestItems = new List<QuestItem>();
    private int totalQuestItems = 3;
    [SerializeField]
    Sprite bootSprite;
    [SerializeField]
    Sprite skullSprite;
    [SerializeField]
    Sprite wormSprite;
    #endregion

    #region STRINGS
    [SerializeField]
    private GameObject GodSceneTextObject;
    private Text godSceneText;
    private int nextGodTextID = 0;
    private string[] currentSceneGodTexts =
    {
        "You have two choices, mortal",
        "One - perish in the dark abyss,",
        "Two - bring me what I desire",
        "and your vessel shall be repaired"
    };

    private string[] startGameGodTexts =
    {
        "You have two choices, mortal",
        "One - perish in the dark abyss,",
        "Two - bring me what I desire",
        "and your vessel shall be repaired"
    };
    private string[] questCompleteGodTexts =
    {

    };
    private string[] questFailedGodTexts =
    {

    };
    #endregion

    #region INITIALIZATION
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
        {
            isInGodScene = false;
            GameObject.FindGameObjectWithTag("God").GetComponent<GodController>().InitializeGodController(currentQuestItems);
        }
        else
        {
            GenerateQuest();
            isInGodScene = true;
            DisplayNextGodText(true);
        }
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    private void Start()
    {
        DisplayNextGodText();
    }
    #endregion

    #region GOD QUEST GENERATOR
    public void GenerateQuest()
    {
        currentQuestItems.Clear();
        for (int i = 0; i < totalQuestItems; i++)
        {
            // if two previous items were of the same type, 
            // don't make the third item of the same type as well
            if (i == totalQuestItems - 1
                && currentQuestItems[0].type == currentQuestItems[1].type)
            {
                ItemType newItemType = (ItemType)Random.Range(0, 3);
                while (newItemType == currentQuestItems[0].type)
                {
                    newItemType = (ItemType)Random.Range(0, 3);
                }
                AddQuestItem(newItemType);
            }
            else
            {
                AddQuestItem((ItemType)Random.Range(0, 3));
            }
        }
    }

    private void AddQuestItem(ItemType itemType)
    {
        QuestItem newQuestItem = new QuestItem();
        newQuestItem.type = itemType;
        newQuestItem.isFound = false;
        switch (itemType)
        {
            case ItemType.Boot:
                newQuestItem.sprite = bootSprite;
                break;
            case ItemType.Skull:
                newQuestItem.sprite = skullSprite;
                break;
            case ItemType.Worm:
                newQuestItem.sprite = wormSprite;
                break;
        }
        currentQuestItems.Add(newQuestItem);
    }
    #endregion

    private void Update()
    {
        if (Input.anyKeyDown && isInGodScene)
        {
            DisplayNextGodText();
        }
    }

    private void DisplayNextGodText(bool reset = false)
    {
        if (reset)
        {
            nextGodTextID = 0;
        }
        if (nextGodTextID == 0)
        {
            InstantiateGodText();
            godSceneText.text = currentSceneGodTexts[0];
        }
        else if (nextGodTextID == currentSceneGodTexts.Length)
        {
            ShowGodQuest();
            return;
        }
        else
        {
            godSceneText.text = currentSceneGodTexts[nextGodTextID];
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
