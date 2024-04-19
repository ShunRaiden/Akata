using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameItem;

public class ItemDisplay : MonoBehaviour
{
    [HideInInspector] public ItemData data;
    public Image iconImage;

    [HideInInspector] public int Index;

    public InventoryScreen invS;

    public void OnClick()
    {
        if (data.itemType != ItemData.ItemType.NoitemData)
            invS.ShowItemInfo(data, Index);
    }

    void UpdateDisplay()
    {
        iconImage.sprite = data.icon;
        iconImage.color = Color.white;
    }

    public void SetupDisplay(ItemData item)
    {
        data = item;
        UpdateDisplay();
    }
}
