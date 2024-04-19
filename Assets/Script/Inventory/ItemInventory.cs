using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInventory : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance._saveItemInvData.LoadData();
        InventoryScreen.instance.itemInv = GameManager.Instance._saveItemInvData.inv.itemInv;
    }

}
