using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusPlayerManager : MonoBehaviour
{
    public static StatusPlayerManager instance { get { return _instance; } }
    private static StatusPlayerManager _instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }

        _instance = this;

    }

    public Image hpBar;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.gameEnd = false;
        GameManager.Instance.playerHP = GameManager.Instance.playerMaxHP;
        HPPlayerBar(GameManager.Instance.playerHP);
    }

    private void Update()
    {
        hpText.text = GameManager.Instance.playerHP.ToString();
    }

    public void HPPlayerBar(float hp)
    {
        hpBar.fillAmount = hp/100;
    }

    public void TakeDamage(int damage)
    {     
        Debug.Log("Damage : " + damage);
        GameManager.Instance.playerHP -= damage;
        HPPlayerBar(GameManager.Instance.playerHP);
    }

    public void GetHeal()
    {       
        HPPlayerBar(GameManager.Instance.playerHP);
    }

    public TMP_Text hpText;
}
