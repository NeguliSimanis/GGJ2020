using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestItem
{
    public ItemType type;
    public bool isFound;
    public Sprite sprite;
}

public class GodController : MonoBehaviour
{
    private float questDuration = 60f;
    [Header("QUEST ITEMS")]
    [SerializeField]
    private GameObject[] possibleQuestItems;
    private List<QuestItem> currentQuestItems = new List<QuestItem>();
    private int totalQuestItems = 3;
    private int questItemTypeCount;

    [Header("HUD")]
    [SerializeField]
    GameObject godDialogueBubble;
    [SerializeField]
    SpriteRenderer[] dialogueImages;

    private void Start()
    {
        questItemTypeCount = possibleQuestItems.Length;
        DisplayQuestDialogue(false);
        GenerateQuest();
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
                AddQuestItem(newItemType, possibleQuestItems[i]);
            }
            else
            {
                AddQuestItem((ItemType)Random.Range(0, questItemTypeCount),possibleQuestItems[i]);
            }
        }
    }

    private void AddQuestItem(ItemType itemType, GameObject itemPrefab)
    {
        QuestItem newQuestItem = new QuestItem();
        newQuestItem.type = itemType;
        newQuestItem.isFound = false;
        newQuestItem.sprite = itemPrefab.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite;
        currentQuestItems.Add(newQuestItem);
        Debug.Log("added quest item  " + newQuestItem.type);
    }

    public void SubmitItemForQuest(ItemType itemType, Sprite sprite)
    {
        // TODO - CHECK IF CORRECT TYPE
        for (int i = 0; i < currentQuestItems.Count; i++)
        {
            if(currentQuestItems[i].isFound == false) 
            {
                if (currentQuestItems[i].type != itemType)
                {
                    Debug.Log("QUEST FAILED");
                }
                else
                {
                    Debug.Log("quest step complete");
                    if (i == currentQuestItems.Count - 1)
                    {
                        Debug.Log("ALL QUESTS COMPLETE");
                    }
                }
                return;
            }
        }

        // NO - LOSE LIFE
        // YES - GAIN LIFE
    }

    private void DisplayQuestDialogue(bool display)
    {
        if (!display)
            godDialogueBubble.SetActive(false);
        else
        {
            Debug.Log("displaying");
            godDialogueBubble.SetActive(true);
            for (int i = 0; i < dialogueImages.Length; i++)
            {
                dialogueImages[i].sprite = currentQuestItems[i].sprite;
            }
        }    
    }
}
