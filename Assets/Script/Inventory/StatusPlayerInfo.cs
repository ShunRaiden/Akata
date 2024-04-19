using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatusPlayerInfo : MonoBehaviour
{
    public LevelData levelData;

    public TMP_Text levelText;
    public TMP_Text hpText;
    public TMP_Text atkText;
    public TMP_Text spdText;

    // Start is called before the first frame update
    void Start()
    {
        UpdateStatusPlayer();
    }

    private void Update()
    {
        hpText.text = "HP : " + GameManager.Instance.playerHP + " / " + levelData.levelStat[levelData.currentLevel].max_HP;
    }

    public void UpdateStatusPlayer()
    {
        levelText.text = levelData.levelStat[levelData.currentLevel].level;
        hpText.text = GameManager.Instance.playerHP + " / " + levelData.levelStat[levelData.currentLevel].max_HP;
        atkText.text = "" + levelData.levelStat[levelData.currentLevel].atk;
        spdText.text = "" + levelData.levelStat[levelData.currentLevel].move_spd;


    }
}
