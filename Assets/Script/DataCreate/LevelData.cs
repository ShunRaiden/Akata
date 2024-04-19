using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "UpgradeData", menuName = "Data/UpgradeData ")]
public class LevelData : ScriptableObject
{
    public int currentLevel;
    public List<LevelStat> levelStat = new List<LevelStat>();

    [System.Serializable]
    public class LevelStat
    {
        public string level;
        public int max_HP;
        public int atk;
        public int skill_atk;
        public int move_spd;
        public int atk_Spd;
    }

}



