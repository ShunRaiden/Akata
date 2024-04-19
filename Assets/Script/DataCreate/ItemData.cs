using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameItem
{
    [CreateAssetMenu(fileName = "Data", menuName = "Data/ItemData")]
    public class ItemData : ScriptableObject
    {
        public string itemName;
        public string outputName;
        [TextArea(3, 10)] public string description;
        [TextArea(3, 10)] public string effectText;
        public Sprite icon;
        public Sprite rImage;
        public ItemType itemType;
        public BuffType buffType;
        public int addHPindex;
        public int addDMindex;

        public enum ItemType
        {
            UseItem,
            CraftItem,
            PrayItem,
            QuestItem,
            NoitemData,
        }

        public enum BuffType
        {
            Heal,
            Damage,
        }

        
    }
}

