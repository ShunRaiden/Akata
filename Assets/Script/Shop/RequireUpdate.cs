using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GameItem;

public class RequireUpdate : MonoBehaviour
{
    [Header("Use All")]
    public List<RequireList> requireLists = new List<RequireList>();
    public ItemRequireData itemRequireData;
    public Button shopBTN;  

    [Header("Pray Use")]
    public bool _isPray;
    public TMP_Text levelNext;
    public GameObject Commingsoon; // For now

    [Header("Shop Use")]
    public TMP_Text itemName;
    public TMP_Text descriptionText;
    public TMP_Text effectText;
    public Image icon;
    [SerializeField] ItemInstance itemInstance;

    [HideInInspector] public List<ItemInv> datas = new List<ItemInv>();
    bool reqCheck = false;
    bool lastReqCheck;


    public void SetInfo()
    {
        if (!_isPray)
            ShopInfo();

        datas.Clear();
        lastReqCheck = true;

        for (int i = 0; i < requireLists.Count; i++)
        {
            requireLists[i].icon.sprite = itemRequireData.requiteLists[i].items.icon;

            requireLists[i].count.text = "";

            FindItemInIvn(itemRequireData.requiteLists[i].items, requireLists[i].count, itemRequireData.requiteLists[i].countRequite);
            requireLists[i].count.text += /*äÍà·Á¨Ò¡ã¹ inv + */itemRequireData.requiteLists[i].countRequite.ToString();
            if (!reqCheck)
            {
                lastReqCheck = false;
            }
        }

        if (!lastReqCheck)
            shopBTN.interactable = false;
        else
            shopBTN.interactable = true;
    }

    public void ShopInfo()
    {
        itemName.text = itemRequireData.items.outputName;
        descriptionText.text = itemRequireData.items.description;
        effectText.text = itemRequireData.items.effectText;
        icon.sprite = itemRequireData.items.icon;
    }

    public void FindItemInIvn(ItemData itemData, TMP_Text text, int reqIndex)
    {
        reqCheck = false;
        for (int i = 0; i < GameManager.Instance._saveItemInvData.inv.itemInv.Count; i++)
        {
            ItemInv data = GameManager.Instance._saveItemInvData.inv.itemInv[i];

            if (itemData.itemName == data.itemData.itemName)
            {
                datas.Add(data);
                text.text = data.itemCount.ToString() + " l ";
                if (data.itemCount >= reqIndex)
                {
                    reqCheck = true;
                }
                else
                {
                    reqCheck = false;
                }

                return;
            }
            else
            {
                text.text = "0 l ";
            }

        }

        if (GameManager.Instance._saveItemInvData.inv.itemInv.Count == 0)
        {
            text.text = "0 l ";
        }
    }

    public void Shopping()
    {
       
        for (int i = 0; i < datas.Count; i++)
        {
            GameManager.Instance._inventoryScreen.TradingItem(itemRequireData.requiteLists[i].countRequite, datas[i].itemData);           
        }

        itemInstance.itemData = itemRequireData.items;

        if(GameManager.Instance._questManager.currentData != null)
        {
            if (GameManager.Instance._questManager.currentData.questType == QuestData.QuestType.GetItem)
            {
                Debug.Log("ItemC");
                Debug.Log(itemInstance.itemData.itemName);
                QuestManager qm = GameManager.Instance._questManager;

                if (itemInstance.itemData.itemName == qm.currentData.itemName)
                {
                    Debug.Log("ItemQ+");
                    qm.currentData.itemCount++;
                    qm.UpadateGetItemQuest();
                    qm.ShowQuestNPCSentUI(qm.currentData);
                }
            }
        }
        

        GameManager.Instance._inventoryScreen.AddGameItem(itemInstance);
        SetInfo();

    }

    public void UpLevel()
    {
        for (int i = 0; i < datas.Count; i++)
        {
            GameManager.Instance._inventoryScreen.TradingItem(itemRequireData.requiteLists[i].countRequite, datas[i].itemData);
        }

        levelNext.text = "2";
        GameManager.Instance._playerStatus.levelData.currentLevel++;
        SetInfo();

        GameManager.Instance._statusPlayerInfo.UpdateStatusPlayer();
    }
}

[System.Serializable]
public class RequireList
{
    public Image icon;
    public TMP_Text count;
}
