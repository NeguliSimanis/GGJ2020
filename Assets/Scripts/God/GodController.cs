using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuestItem
{
    public ItemType type;
    public bool isFound;
    public Sprite sprite;
}

public class GodController : MonoBehaviour
{
    bool isWinning = false;
    PlayerHealth playerHealth;
    int questsComplete = 0;

    private float questDuration = 60f;
    [Header("QUEST ITEMS")]
    [SerializeField]
    private GameObject[] possibleQuestItems;
    public List<QuestItem> currentQuestItems = new List<QuestItem>();
    [HideInInspector]
    public int totalQuestItems = 3;
    private int questItemTypeCount;

    [Header("GOD QUEST DIALOGUE")]
    [SerializeField]
    GameObject godDialogueBubble;
    [SerializeField]
    SpriteRenderer[] dialogueImages;
    [SerializeField]
    Sprite bootSprite;
    [SerializeField]
    Sprite skullSprite;
    [SerializeField]
    Sprite wormSprite;

    #region QUEST HUD
    Image questItemHUD0;
    Image questItemHUD1;
    Image questItemHUD2;

    Color questItemDefaultColor = Color.black;
    Color questItemFoundColor = Color.white;
    #endregion
    private void Start()
    {
        //playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        questItemTypeCount = possibleQuestItems.Length;
        DisplayQuestDialogue(false);
        //GenerateQuest();
        
    }

    public void InitializeGodController(List<QuestItem> newQuestItems)
    {
        Debug.Log("should initialize god");
        currentQuestItems.Clear();
        currentQuestItems = newQuestItems;
        foreach (QuestItem questItem in newQuestItems)
        {
            Debug.Log("ADDED CONTROL " + questItem.type);
        }
        InitializeQuestItemHUD();
        DisplayQuestDialogue(true);
    }

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
                ItemType newItemType = (ItemType)Random.Range(0, questItemTypeCount);
                while (newItemType == currentQuestItems[0].type)
                {
                    newItemType = (ItemType)Random.Range(0, questItemTypeCount);
                }
                AddQuestItem(newItemType);
            }
            else
            {
                AddQuestItem((ItemType)Random.Range(0, questItemTypeCount));
            }
        }
        InitializeQuestItemHUD();
        DisplayQuestDialogue(true);
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

    public void SubmitItemForQuest(ItemType itemType, Sprite sprite)
    {
        for (int i = 0; i < currentQuestItems.Count; i++)
        {
            if(currentQuestItems[i].isFound == false) 
            {
                // quest step failed
                if (currentQuestItems[i].type != itemType)
                {
                    if (playerHealth == null)
                        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
                    playerHealth.DamagePlayer();
                    GenerateQuest();
                }
                // quest step complete
                else
                {
                    CompleteQuestStep(i);
                }
                return;
            }
        }
    }

    private void CompleteQuestStep(int questStepID)
    { 
        currentQuestItems[questStepID].isFound = true;
        UpdateQuestItemHUD(true);
    }

    private void InitializeQuestItemHUD()
    {
        questItemHUD0 = GameObject.Find("QuestItemIcon").GetComponent<Image>();
        questItemHUD1 = GameObject.Find("QuestItemIcon (1)").GetComponent<Image>();
        questItemHUD2 = GameObject.Find("QuestItemIcon (2)").GetComponent<Image>();

        questItemHUD0.sprite = currentQuestItems[0].sprite;
        questItemHUD1.sprite = currentQuestItems[1].sprite;
        questItemHUD2.sprite = currentQuestItems[2].sprite;

        questItemHUD0.color = questItemDefaultColor;
        questItemHUD1.color = questItemDefaultColor;
        questItemHUD2.color = questItemDefaultColor;
    }

    private void UpdateQuestItemHUD(bool itemFound)
    {
        for (int i = 0; i < currentQuestItems.Count; i++)
        {
            if (currentQuestItems[i].isFound)
            {
                switch (i)
                {
                    case 0:
                        questItemHUD0.color = questItemFoundColor;
                        break;
                    case 1:
                        questItemHUD1.color = questItemFoundColor;
                        break;
                    case 2:
                        Debug.Log("yay");
                        questItemHUD2.color = questItemFoundColor;
                        DealWithPlayerQuestCompletion();
                        break;
                }
            }
        }
    }

    private void DisplayQuestDialogue(bool display)
    {
        if (!display)
        {

        }
           // godDialogueBubble.SetActive(false);
        else
        {
            //godDialogueBubble.SetActive(true);
            for (int i = 0; i < dialogueImages.Length; i++)
            {
                dialogueImages[i].sprite = currentQuestItems[i].sprite;
            }
        }    
    }

    private void DealWithPlayerQuestCompletion()
    {
        if (!isWinning)
            StartCoroutine(WinGameAfterDelay());
    }

    private IEnumerator WinGameAfterDelay()
    {
        isWinning = true;
        yield return new WaitForSeconds(1);
        if (GodScene.instance != null)
        {
            GodScene.instance.lastQuestFailed = false;
            GodScene.instance.questsComplete++;
        }
        else
        {
            Debug.LogError("GOD NOT DEFINED. START THE GAME FROM GOD SCENE TO AVOID THIS ERRRO");
        }
        SceneChanger.instance.LoadLevelAfterFade("3_GodScreen");
    }

    public void DealWithPlayerDeath()
    {
        if (GodScene.instance != null)
        {
            GodScene.instance.lastQuestFailed = true;
            GodScene.instance.questsFailed++;
        }
        else
        {
            Debug.LogError("GOD NOT DEFINED. START THE GAME FROM GOD SCENE TO AVOID THIS ERRRO");
        }
        SceneChanger.instance.LoadLevelAfterFade("3_GodScreen");
        
    }
}
