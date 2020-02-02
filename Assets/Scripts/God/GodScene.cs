using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GodScene : MonoBehaviour
{
    public static GodScene instance;

    #region SCENE DATA
    private bool isPlayerDefeat = false;
    private int godSceneID = 0;
    private string godSceneName;
    private bool isInGodScene = true;
    private bool isShowingGodQuest = false;
    bool showingQuestIcons = false;
    #endregion

    #region QUEST GENERATION DATA
    public int questsFailed = 0;
    public int questsComplete = 0;
    public bool lastQuestFailed = false;
    [SerializeField]
    private GameObject godScreenQuestIcon;
    public List<QuestItem> currentQuestItems = new List<QuestItem>();
    private int totalQuestItems = 3;
    [SerializeField]
    Sprite bootSprite;
    [SerializeField]
    Sprite skullSprite;
    [SerializeField]
    Sprite wormSprite;
    [SerializeField]
    Sprite secondCloudSprite;
    #endregion

    #region STRINGS
    [SerializeField]
    private GameObject GodSceneTextObject;
    private Text godSceneText;
    private int nextGodTextID = 0;
    private string[] currentSceneGodTexts =
    {
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
        "You have appeased me for now, mortal",
        "Bring me more, MORE!"
    };
    private string[] questFailedGodTexts =
    {
        "You have failed",
    };
    private string[] gameLost =
{
        "Another failure...",
        "Fate worse than death awaits you..."
    };
    private string bringThisToMe = "Bring this to me";
    #endregion

    #region INITIALIZATION
    private void ResetGodSceneData()
    {
        isPlayerDefeat = false;
        godSceneID = 0;
        isInGodScene = true;
        isShowingGodQuest = false;
        showingQuestIcons = false;

        questsFailed = 0;
        questsComplete = 0;
        lastQuestFailed = false;
        nextGodTextID = 0;
    }   


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
        isShowingGodQuest = false;
        if (SceneManager.GetActiveScene().name != godSceneName)
        {
            isInGodScene = false;
            if (SceneManager.GetActiveScene().name == "4_Game")
                GameObject.FindGameObjectWithTag("God").GetComponent<GodController>().InitializeGodController(currentQuestItems);
            else
                ResetGodSceneData();
        }
        else
        {
            SetGodDialogueStrings();
            GenerateQuest();
            isInGodScene = true;
            DisplayNextGodText(true);
            ShowGodQuest(false);
        }
        showingQuestIcons = false;
    }

    void SetGodDialogueStrings()
    {
        if (godSceneID < 2)
            currentSceneGodTexts = startGameGodTexts;
        else if (lastQuestFailed && questsFailed < 3)
            currentSceneGodTexts = questFailedGodTexts;
        else if (lastQuestFailed && questsFailed == 3)
        {
            currentSceneGodTexts = gameLost;
            isPlayerDefeat = true;
        }
        else if (!lastQuestFailed)
            currentSceneGodTexts = questCompleteGodTexts;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
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
                Debug.Log("adding  boot");
                break;
            case ItemType.Skull:
                newQuestItem.sprite = skullSprite;
                Debug.Log("adding  skull");
                break;
            case ItemType.Worm:
                newQuestItem.sprite = wormSprite;
                Debug.Log("adding  worm");
                break;
        }
        currentQuestItems.Add(newQuestItem);
    }
    #endregion

    private void Update()
    {
        if (Input.anyKeyDown && isInGodScene)
        {
            if (!isShowingGodQuest)
                DisplayNextGodText();
            else
                LoadNextLevel();
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
            if (currentSceneGodTexts[0] == questCompleteGodTexts[0])
                godSceneText.fontSize = 90;
            else
                godSceneText.fontSize = 110;
            
            godSceneText.text = currentSceneGodTexts[0];
        }
        else if (nextGodTextID == currentSceneGodTexts.Length)
        {
            if (isPlayerDefeat || questsComplete == 3)
            {
                isShowingGodQuest = true;
                LoadNextLevel();
                return;
            }
            StartCoroutine(ShowGodQuest());
            InstantiateGodText(false);
            return;
        }
        else
        {
            godSceneText.text = currentSceneGodTexts[nextGodTextID];
        }
        nextGodTextID++;
    }

    private IEnumerator ShowGodQuest(bool show = true)
    {
        if (show && !showingQuestIcons)
        {
            showingQuestIcons = true;
            Debug.Log("shiw");
            InstantiateGodText(false);
            GameObject questIcon1 = Instantiate(godScreenQuestIcon, godSceneText.gameObject.transform);
            questIcon1.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = currentQuestItems[0].sprite;
            yield return new WaitForSeconds(0.8f);

            GameObject questIcon2 = Instantiate(godScreenQuestIcon, godSceneText.gameObject.transform);
            questIcon2.GetComponent<Image>().sprite = secondCloudSprite;
            questIcon2.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = currentQuestItems[1].sprite;
            yield return new WaitForSeconds(0.8f);

            GameObject questIcon3 = Instantiate(godScreenQuestIcon, godSceneText.gameObject.transform);
            questIcon3.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = currentQuestItems[2].sprite;
            isShowingGodQuest = true;

        }

    }

    private void InstantiateGodText(bool instantiate = true)
    {
        if (!instantiate)
        {
            godSceneText.enabled = false;
            return;
        }
        GameObject newGodTextObject = 
            Instantiate(GodSceneTextObject, GameObject.FindGameObjectWithTag("GodCanvas").transform);
        godSceneText = newGodTextObject.GetComponent<Text>();

    }

    private void LoadNextLevel()
    {
        if (isPlayerDefeat)
            SceneChanger.instance.LoadLevelAfterFade("7_GameLose");
        else if (questsComplete == 3)
            SceneChanger.instance.LoadLevelAfterFade("6_GameWin");
        else
            SceneChanger.instance.LoadLevelAfterFade("4_Game");
    }

}
