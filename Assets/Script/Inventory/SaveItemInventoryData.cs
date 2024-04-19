using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static SaveItemInventoryData;


[System.Serializable]
[CreateAssetMenu(fileName = "SaveInventoryData", menuName = "Data/Inventory ")]
public class SaveItemInventoryData : ScriptableObject
{
    [SerializeField] SaveInv sInv;
    public SaveInv inv;
    public int MaxSize;

    public string stringData;


    public void SaveData(SaveInv saveInv)
    {
        inv = saveInv;
        stringData = JsonUtility.ToJson(sInv);
        PlayerPrefs.SetString("Inventory", stringData);
    }
    public void LoadData()
    {
        string data = PlayerPrefs.GetString("Inventory");
        sInv = JsonUtility.FromJson<SaveInv>(data);
        inv = sInv;
    }   

    [System.Serializable]
    public class SaveInv
    {
        public List<ItemInv> itemInv = new List<ItemInv>();
    }
}
