using GameItem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestData", menuName = "Data/QuestData ")]
public class QuestData : ScriptableObject
{
    public string questName;
    public int dialogueSetIndex;

    [TextArea(3, 10)]
    public string questDiscription;

    public QuestStatus questStatus;
    public QuestType questType;
    public NPC_All npcQuestGiver;
    public string npcGiverOutput;

    [Header("Talk Quest")]
    public NPC_All npcToTalk;
    public bool openShop;

    [Header("Kill Quest")]
    public int killTaget;
    public int killCount;

    [Header("GetItem Quest")]
    public string itemName;
    public int itemTagetCount;
    public int itemCount;

    [Header("PassState Quest")]
    public string stateTarget;

    public enum QuestStatus
    {
        InActive = 0,
        Active,
        Completed,
    }

    public enum QuestType
    {
        Talk,
        Kill,
        GetItem,
        PassState,
    }

    public enum NPC_All
    {
        Uncle = 0,
        Boy,
        Chef,
        Miko,
    }

    public void TalkQuest(string npcCurrent)
    {
        if (npcToTalk.ToString() == npcCurrent)
        {
            Debug.Log("Quest Completed");
            questStatus = QuestStatus.Completed;
            ItemDrop();

            CenterManager.instance.dialogUpdate++;

            if (openShop)
            {
                CenterManager.instance._npcInteractControll[(int)npcToTalk].GetComponent<NPCInteract>()._ShopOpen = openShop;
                PlayerPrefs.SetInt(npcToTalk.ToString()+"OpenUI",1);
                PlayerPrefs.Save();
            }
        }
    }

    public void KillCountQuest()
    {
        if (killCount == killTaget)
        {
            Debug.Log("Quest Kill Completed");
            questStatus = QuestStatus.Completed;
            ItemDrop();
            GameManager.Instance._questManager.currentData = null;
            GameManager.Instance._questManager.indexQuestCurrent++;
            CenterManager.instance.dialogUpdate = dialogueSetIndex;
        }
        else
        {
            killCount++;
        }
    }

    public void GetItemQuest()
    {
        if(itemTagetCount == itemCount)
        {
            questStatus = QuestStatus.Completed;
            ItemDrop();
            CenterManager.instance.dialogUpdate = dialogueSetIndex;
        }
    }

    public void PassStateQuest(string stateName)
    {
        if(stateName == stateTarget)
        {
            questStatus = QuestStatus.Completed;
            ItemDrop();
            CenterManager.instance.dialogUpdate = dialogueSetIndex;
        }
    }

    public void ItemDrop()
    {
        GameManager.Instance._questManager.QuestComplete();
    }
}
