using GameItem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class QuestManager : MonoBehaviour
{
    #region Singleton
    public static QuestManager instance { get { return _instance; } }
    private static QuestManager _instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Destroy the new instance, not this script
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        if (PlayerPrefs.HasKey("QuestIndex"))
        {
            indexQuestCurrent = PlayerPrefs.GetInt("QuestIndex");
            currentData = availableQuests[indexQuestCurrent];
        }

    }
    #endregion

    public List<QuestData> availableQuests = new List<QuestData>();
    public QuestData currentData;

    public int indexQuestCurrent;

    [Header("UI")]
    public GameObject questUICanvas;
    public GameObject questUI;
    public TMP_Text questName;
    public TMP_Text questInfo;
    public GameObject questNavUI;
    public GameObject questNavUI2;
    public TMP_Text questNavInfo;

    public List<GameObject> iconQuest = new List<GameObject>();
    public string npcTalkingCurrent;
    public string npcQuestGiver;

    public List<GiveItem> giveItems = new List<GiveItem>();

    public void Start()
    {
        if (PlayerPrefs.GetInt("FirstTalkNPC") == 0)
        {
            questNavUI.SetActive(true);
            return;
        }
        else
        {
            questNavUI.SetActive(false);
            if (PlayerPrefs.HasKey("QuestIndex"))
            {
                indexQuestCurrent = PlayerPrefs.GetInt("QuestIndex");
                currentData = availableQuests[indexQuestCurrent];

                if (currentData.questStatus == QuestData.QuestStatus.Completed)
                    ShowQuestNPCSentUI(currentData);
                else
                    ShowQuestUI(currentData);
            }
        }

    }

    public void StartQuest()
    {
        if (indexQuestCurrent >= availableQuests.Count)// Fix Bug Out of Quest
        {
            questUI.SetActive(false);
            currentData = null;
            return;
        }

        string npcQG = availableQuests[indexQuestCurrent].npcQuestGiver.ToString(); //Get Name NPC Give Quest

        if (npcQG != npcTalkingCurrent && currentData == null)
        {
            Debug.Log("Not Yes Quest");
            return;
        }


        questNavUI2.SetActive(false);


        if (currentData == null)
        {
            Debug.Log("Quest Start");
            CenterManager.instance.dialogUpdate++;
            currentData = availableQuests[indexQuestCurrent];
            UpdateQuestProgress(currentData);
            PlayerPrefs.SetInt("QuestIndex", indexQuestCurrent);
            PlayerPrefs.Save();

            ShowQuestUI(currentData);
        }
        else
        {
            Debug.Log(currentData.questStatus.ToString());
            if (currentData.questStatus == QuestData.QuestStatus.Completed && npcQG == npcTalkingCurrent)
            {
                indexQuestCurrent++;

                if (indexQuestCurrent >= availableQuests.Count)// Fix Bug Out of Quest
                {
                    questUI.SetActive(false);
                    currentData = null;
                    return;
                }

                currentData = availableQuests[indexQuestCurrent];
                CenterManager.instance.dialogUpdate++;
                Debug.Log("Quest Complete");

                //Quest Done Do some Anim
                ShowQuestUI(currentData);
            }
            else
            {

                UpdateQuestProgress(currentData);
                Debug.Log("Quest Update");
                ShowQuestUI(currentData);

            }

        }



    }
    public void UpdateQuestProgress(QuestData quest)
    {
        if (quest.questStatus == QuestData.QuestStatus.Completed)
            return;

        Debug.Log("Quest Active");
        quest.questStatus = QuestData.QuestStatus.Active;

        switch (quest.questType)
        {
            case QuestData.QuestType.Talk:
                Debug.Log("Quest Talk");
                quest.TalkQuest(npcTalkingCurrent);
                break;
            case QuestData.QuestType.Kill:
                quest.KillCountQuest();
                break;
            case QuestData.QuestType.GetItem:
                quest.GetItemQuest();
                break;
            case QuestData.QuestType.PassState:
                quest.PassStateQuest(SceneManager.GetActiveScene().buildIndex.ToString());
                break;
        }

        if (quest.questStatus == QuestData.QuestStatus.Completed && quest.questType != QuestData.QuestType.Talk)
        {
            ShowQuestNPCSentUI(currentData);
        }

        PlayerPrefs.SetInt("QuestIndex", indexQuestCurrent);
        PlayerPrefs.Save();

    }

    public void UpdateTalkQuest()
    {
        if (currentData == null || currentData.questStatus != QuestData.QuestStatus.Active)
            return;

        if (currentData.questType == QuestData.QuestType.Talk)
        {
            Debug.Log("Quest Pass");
            currentData.TalkQuest(npcTalkingCurrent);
        }
    }

    public void UpdatePassQuest()
    {
        if (currentData == null || currentData.questStatus != QuestData.QuestStatus.Active)
            return;

        if (currentData.questType == QuestData.QuestType.PassState)
        {
            Debug.Log("Quest Pass");
            currentData.PassStateQuest(SceneManager.GetActiveScene().name);
        }
    }

    public void UpadateGetItemQuest()
    {
        if (currentData == null || currentData.questStatus != QuestData.QuestStatus.Active)
            return;

        if (currentData.questType == QuestData.QuestType.GetItem)
        {
            Debug.Log("Quest Pass");
            currentData.GetItemQuest();
        }
    }

    public void QuestComplete()
    {
        if (giveItems[indexQuestCurrent].items == null)
            return;

        for (int i = 0; i < giveItems[indexQuestCurrent].items.Count; i++)
        {
            for (int j = 0; j < giveItems[indexQuestCurrent].dropNumber[i]; j++)
            {
                giveItems[indexQuestCurrent].itemData.itemData = giveItems[indexQuestCurrent].items[i];

                GameManager.Instance._inventoryScreen.AddGameItem(giveItems[indexQuestCurrent].itemData);
                GameManager.Instance._inventoryScreen.UpdateScreen();
            }
        }
    }

    public void ShowQuestUI(QuestData quest)
    {
        if (currentData == null)
            return;

        questUI.SetActive(true);
        questName.text = quest.questName;

        switch (quest.questType)
        {
            case QuestData.QuestType.Talk:
                questInfo.text = currentData.questDiscription;
                break;
            case QuestData.QuestType.Kill:
                questInfo.text = currentData.questDiscription + " : " + currentData.killCount.ToString() + " / " + currentData.killTaget.ToString();
                break;
            case QuestData.QuestType.GetItem:
                questInfo.text = currentData.questDiscription + " : " + currentData.itemCount.ToString() + " / " + currentData.itemTagetCount.ToString();
                break;
            case QuestData.QuestType.PassState:
                questInfo.text = currentData.questDiscription;
                break;
        }
    }

    public void ShowQuestNPCSentUI(QuestData quest)
    {
        if (quest == null)
            return;

        questUI.SetActive(true);
        questName.text = quest.questName;
        questInfo.text = "令�¡Ѻ " + quest.npcGiverOutput;
    }
}

[System.Serializable]
public class GiveItem
{
    public string name = "Quest";
    public List<ItemData> items = new List<ItemData>();
    public ItemInstance itemData;
    public List<int> dropNumber = new List<int>();
}
