using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameItem.ItemData;

namespace GameItem
{
    [CreateAssetMenu(fileName = "Data", menuName = "Data/ItemRequireData")]
    public class ItemRequireData : ScriptableObject
    {
        public ItemData items;
        public List<RequiteList> requiteLists = new List<RequiteList>();       
    }

    [System.Serializable]
    public class RequiteList
    {
        public ItemData items;
        public int countRequite;
    }

}
