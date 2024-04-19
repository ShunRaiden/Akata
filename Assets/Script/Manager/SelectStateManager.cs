using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SelectStateManager : MonoBehaviour
{
    public GameObject selectStateUI;
    public List<GameObject> infoSelectSateUI = new List<GameObject>();
    public PauseGame pauseGame;

    public bool isSelectStateOpen;

    int currentInfo;
    int newInfo;


    [Header("Lock State")]
    public List<Button> startStateBtn = new List<Button>();
    public List<GameObject> lockStateImg = new List<GameObject>();

    public int passedState = 0;

    private void Start()
    {
        GameManager.Instance._selectStateManager = this;
        isSelectStateOpen = false;
        selectStateUI.gameObject.SetActive(false);

        //Lock State
        if (!PlayerPrefs.HasKey("StatePass"))
        {
            PlayerPrefs.SetInt("StatePass", 0);
            PlayerPrefs.Save();
        }
        passedState = PlayerPrefs.GetInt("StatePass");

        for (int i = 0; i < startStateBtn.Count; i++)
        {
            if (i <= passedState)
            {
                lockStateImg[i].SetActive(false);
                startStateBtn[i].interactable = true;
            }
            else
            {
                lockStateImg[i].SetActive(true);
                startStateBtn[i].interactable = false;
            }
        }
        //End
    }

    private void Update()
    {
        if(isSelectStateOpen)
        {
            if(Input.GetKeyDown(pauseGame.pauseGameKey))
            {
                CloseUI();
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            selectStateUI.gameObject.SetActive(true);
            isSelectStateOpen = true;
            PlayerInteraction.instance._playerControll.canMove = false;
        }
    }

    public void CloseUI()
    {
        selectStateUI.gameObject.SetActive(false);
        PlayerInteraction.instance._playerControll.canMove = true;
        isSelectStateOpen = false;
    }

    public void OnClickInfo(int state)
    {
        
        if (currentInfo == 0)
        {
            currentInfo = state;
            infoSelectSateUI[state].SetActive(true);
        }

        newInfo = state;

        if (currentInfo == newInfo)
            return;   

        infoSelectSateUI[newInfo].SetActive(true);
        infoSelectSateUI[currentInfo].SetActive(false);
        currentInfo = newInfo;

    }
}

