using GameItem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopDisplays : MonoBehaviour
{
    GameObject currentDataScreen;
    public List<RequireUpdate> requireUpdate =new List<RequireUpdate>();

    public void OnClick(GameObject newDataScreen)
    {
        if(currentDataScreen != null)
        {
            currentDataScreen.SetActive(false);
        }
        newDataScreen.SetActive(true);
        currentDataScreen = newDataScreen;
    }

    public void PrayUpdateInfo()
    {
        requireUpdate[GameManager.Instance._playerStatus.levelData.currentLevel].SetInfo();
    }

    public void Close()
    {
        if (currentDataScreen != null)
            currentDataScreen.SetActive(false);

        GameManager.Instance._OpenShopUI = false;
        this.gameObject.SetActive(false);
        
    }
}
