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
    private int itemTypeCount;

    [Header("UI")]
    [SerializeField]
    GameObject godDialogueBubble;

    private void Start()
    {
        itemTypeCount = ItemType.GetNames(typeof(ItemType)).Length;
        GenerateQuest();
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
                ItemType newItemType = (ItemType)Random.Range(0, itemTypeCount);
                while (newItemType == questItems[0].type)
                {
                    newItemType = (ItemType)Random.Range(0, itemTypeCount);
                }
                AddQuestItem(newItemType);
            }
            else
            {
                AddQuestItem((ItemType)Random.Range(0, itemTypeCount));
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

    public void DisplayQuestDialogue()
    {

    }
}
