using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GameItem;
using static GameItem.ItemData;
using static SaveItemInventoryData;
using UnityEngine.SceneManagement;

public class InventoryScreen : MonoBehaviour
{

    public static InventoryScreen instance { get { return _instance; } }
    private static InventoryScreen _instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

    }

    public TMP_Text nameText;
    public TMP_Text descriptionText;
    //public Image iconImage;
    //public Button useButton;
    //public Button dropButton;
    public ItemData noItemData;
    public List<ItemDisplay> itemDisplays = new List<ItemDisplay>();
    public List<TMP_Text> itemCountDisplays;
    public GameObject infoDisplays;

    public bool _isPictureCreate;
    public Image _iconImage;

    public GameObject useButton;
    int buttonIndex;
    ItemData dataForUse;

    [Header("Quick Item")]
    [SerializeField] ItemInv dataHPQuickUse;
    ItemData dataDMQuickUse;
    public GameObject useHealBtn;
    public TMP_Text healItemCount;
    int healItemIndex;
    int healItemCurrentIndex;
    bool _canUseHeal;
    bool _canUseDM;

    public List<ItemInv> itemInv = new List<ItemInv>();

    int _selectedIndex = -1;

    //public KeyCode pauseGameKey;

    private void Start()
    {
        infoDisplays.SetActive(false);
        _canUseHeal = false;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "Center" && !_canUseHeal)
        {
            _canUseHeal = true;
            useHealBtn.SetActive(true);
            CheckItemQuick();
        }

        if (SceneManager.GetActiveScene().name == "Center")
        {
            _canUseHeal = false;
            useHealBtn.SetActive(false);
        }
    }

    public void UpdateScreen()
    {
        int maxSize = GameManager.Instance._saveItemInvData.MaxSize;

        for (int i = 0; i < maxSize; i++)
        {

            itemDisplays[i].Index = i;

            if (itemInv.Count > i)
            {
                itemDisplays[i].SetupDisplay(itemInv[i].itemData);
                itemCountDisplays[i].text = itemInv[i].itemCount.ToString();
            }
            else
            {
                itemDisplays[i].SetupDisplay(noItemData);
                itemCountDisplays[i].text = "0";
            }

        }

        //SaveToInv
        SavaDataInv();
    }

    public void SavaDataInv()
    {      
        SaveInv saveInv = new SaveInv();

        for (int i = 0; i < itemInv.Count; i++)
        {
            saveInv.itemInv.Add(itemInv[i]);
        }

        GameManager.Instance._saveItemInvData.SaveData(saveInv);
    }

    public void ShowItemInfo(ItemData data, int index)
    {
        infoDisplays.SetActive(true);

        if (!GameManager.Instance._isInvOpen)
        {
            return;
        }

        _iconImage.sprite = data.rImage;
        nameText.SetText(data.outputName);
        descriptionText.SetText(data.description);

        _isPictureCreate = true;
        //iconImage.sprite = data.Icon;

        useButton.SetActive(false);
        if (data.itemType == ItemType.UseItem && SceneManager.GetActiveScene().name != "Center")
        {
            buttonIndex = index;
            dataForUse = data;
            useButton.SetActive(true);
        }

        //dropButton.interactable = data.droppable;
        _selectedIndex = index;

    }

    public void ClearItemInfo()
    {
        ShowItemInfo(noItemData, -1);
    }

    public void AddGameItem(ItemInstance item)
    {
        ItemInv _item = new ItemInv();
        _item.itemData = item.itemData;
        FindRepeatItem(_item, item);
    }

    public void FindRepeatItem(ItemInv item, ItemInstance itemIns)
    {
        bool _isItemRepeat = false;

        //Debug.Log("Test IN");
        for (int i = 0; i < itemInv.Count; i++)
        {
            //Debug.Log("Find Repeat : " + i);
            if (item.itemData.itemName == itemInv[i].itemData.itemName)
            {
                itemInv[i].itemCount++;
                _isItemRepeat = true;
                //Debug.Log("Test Repeat" + item.itemCount);
            }
        }

        if (!_isItemRepeat)
        {
            item.itemCount = itemIns.itemCount;
            //Debug.Log("Test Add Item");
            itemInv.Add(item);
        }

    }


    public void RemoveItem(int index)
    {
        if (itemInv.Count > 0)
            itemInv.RemoveAt(index);
    }

    public void UseItem()
    {
        switch (dataForUse.buffType)
        {
            case BuffType.Heal:

                if (GameManager.Instance.playerHP == GameManager.Instance.playerMaxHP)
                    return;

                GameManager.Instance.playerHP += dataForUse.addHPindex;

                if (GameManager.Instance.playerHP >= GameManager.Instance.playerMaxHP)
                    GameManager.Instance.playerHP = GameManager.Instance.playerMaxHP;

                StatusPlayerManager.instance.GetHeal();
                break;
            case BuffType.Damage:
                break;
        }

        if (itemInv[buttonIndex].itemCount > 1)
        {
            itemInv[buttonIndex].itemCount--;
        }
        else
        {
            RemoveItem(buttonIndex);
            ClearItemInfo();
        }

        UpdateScreen();

    }

    public void CheckItemQuick()
    {
        UpdateScreen();

        int maxSize = GameManager.Instance._saveItemInvData.MaxSize;

        for (int i = 0; i < GameManager.Instance._saveItemInvData.inv.itemInv.Count; i++)
        {
            ItemInv data = GameManager.Instance._saveItemInvData.inv.itemInv[i];

            if (data.itemData.itemType == ItemType.UseItem && data.itemData.buffType == BuffType.Heal)
            {
                healItemIndex = GameManager.Instance._saveItemInvData.inv.itemInv[i].itemCount;
                healItemCount.text = healItemIndex.ToString();
                dataHPQuickUse = data;
                healItemCurrentIndex = i;

                if (healItemIndex <= 0)
                {
                    useHealBtn.GetComponent<Button>().enabled = false;
                }
                else
                {
                    useHealBtn.GetComponent<Button>().enabled = true;
                }
            }

            if (data.itemData.itemType == ItemType.UseItem && data.itemData.buffType == BuffType.Damage)
            {

            }
        }
    }

    public void QuickHeal()
    {

        if (dataHPQuickUse.itemCount == 0)
        {
            useHealBtn.GetComponent<Button>().enabled = false;
            return;
        }
        else
        {
            useHealBtn.GetComponent<Button>().enabled = true;
        }

        if (GameManager.Instance.playerHP == GameManager.Instance.playerMaxHP)
            return;

        GameManager.Instance.playerHP += dataHPQuickUse.itemData.addHPindex;

        if (GameManager.Instance.playerHP >= GameManager.Instance.playerMaxHP)
            GameManager.Instance.playerHP = GameManager.Instance.playerMaxHP;

        StatusPlayerManager.instance.GetHeal();


        if (dataHPQuickUse.itemCount > 1)
        {
            dataHPQuickUse.itemCount--;
        }
        else
        {
            dataHPQuickUse.itemCount--;
            useHealBtn.GetComponent<Button>().enabled = false;
            healItemCount.text = "0";
            RemoveItem(healItemCurrentIndex);
            ClearItemInfo();
        }

        CheckItemQuick();
    }

    public void QuickDM()
    {

    }

    public void TradingItem(int index, ItemData item)
    {
        Debug.Log("2");
        for (int i = 0; i < itemInv.Count; i++)
        {
            if (itemInv[i].itemData.itemName == item.itemName)
            {
                Debug.Log("3");
                if (itemInv[i].itemCount > 1)
                {
                    Debug.Log("4");
                    itemInv[i].itemCount -= index;
                    if (itemInv[i].itemCount < 1)
                    {
                        Debug.Log("5.1");
                        RemoveItem(i);
                        ClearItemInfo();
                    }

                }
                else
                {
                    Debug.Log("5.2");
                    RemoveItem(i);
                    ClearItemInfo();
                }

            }
        }


        UpdateScreen();
    }
}


[System.Serializable]
public class ItemInv
{
    public ItemData itemData;
    public int itemCount = 1;

    public virtual void Use()
    {
        if (itemCount < 1)
        {
            return;
        }

        itemCount--;
    }
}
