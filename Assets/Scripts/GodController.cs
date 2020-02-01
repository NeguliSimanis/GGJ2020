using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestItem
{
    public ItemType type;
    public bool isFound;
}

public class GodController : MonoBehaviour
{
    private float questDuration = 60f;
    private List<QuestItem> questItems = new List<QuestItem>();
    private int totalQuestItems = 3;
    private int questItemTypeCount;

    [Header("UI")]
    [SerializeField]
    GameObject godDialogueBubble;
    [SerializeField]
    SpriteRenderer[] dialogueImages;

    private void Start()
    {
        questItemTypeCount = ItemType.GetNames(typeof(ItemType)).Length-1;
        DisplayQuestDialogue(false);
        GenerateQuest();
        DisplayQuestDialogue(true);
    }

    public void GenerateQuest()
    {
        questItems.Clear();
        for (int i = 0; i < totalQuestItems; i++)
        {
            // if two previous items were of the same type, 
            // don't make the third item of the same type as well
            if (i == totalQuestItems - 1
                && questItems[0].type == questItems[1].type)
            {
                ItemType newItemType = (ItemType)Random.Range(0, questItemTypeCount-1);
                while (newItemType == questItems[0].type)
                {
                    newItemType = (ItemType)Random.Range(0, questItemTypeCount-1);
                }
                AddQuestItem(newItemType);
            }
            else
            {
                AddQuestItem((ItemType)Random.Range(0, questItemTypeCount-1));
            }
        }
    }

    private void AddQuestItem(ItemType itemType)
    {
        QuestItem newQuestItem = new QuestItem();
        newQuestItem.type = itemType;
        newQuestItem.isFound = false;
        questItems.Add(newQuestItem);
        Debug.Log("added quest item  " + newQuestItem.type);
    }

    public void SubmitItemForQuest(ItemType itemType, Sprite sprite)
    {
        // TODO - CHECK IF CORRECT TYPE
       // foreach (f)

        // NO - LOSE LIFE
        // YES - GAIN LIFE
    }

    private void DisplayQuestDialogue(bool display)
    {
        if (!display)
            godDialogueBubble.SetActive(false);
        else
        {
            godDialogueBubble.SetActive(true);
            foreach(SpriteRenderer dialogueImage in dialogueImages)
            {
               // dialogueImage.sprite
            }
        }    
    }
}
