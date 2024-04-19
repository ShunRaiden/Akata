using GameItem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInstance : MonoBehaviour
{
    public ItemData itemData;
    public int itemCount = 1;

    public virtual void Use()
    {
        if(itemCount < 1)
        {
            return;
        }

        itemCount--;
    }


}
