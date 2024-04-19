using GameItem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropItemManager : MonoBehaviour
{
    public List<ListItemInstance> itemDropList = new List<ListItemInstance>();
    public List<int> dropRate = new List<int>();
    public GameObject dropLayoutObj;
    public TMP_Text[] dropIndex;

    private void Start()
    {
        GameManager.Instance._dropItemManager = this;
    }

    public void ItemStateDrop()
    {
        for (int i = 0; i < itemDropList.Count; i++)
        {
            for (int j = 0; j < dropRate[i]; j++)
            {
                GameManager.Instance._inventoryScreen.AddGameItem(itemDropList[i].itemDrop);
            }
        }
    }

    public void ItemRandomDrop()
    {      
        for (int i = 0; i < itemDropList.Count; i++)
        {
            itemDropList[i].itemCount = Random.Range(1, itemDropList[i].dropRate);
            dropRate[i] = itemDropList[i].itemCount;
            dropIndex[i].text = "x" + dropRate[i];
        }

        dropLayoutObj.SetActive(true);
    }

}

[System.Serializable]
public class ListItemInstance
{
    public ItemInstance itemDrop;
    public int itemCount;
    public int dropRate;
}

