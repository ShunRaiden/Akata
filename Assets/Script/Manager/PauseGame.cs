using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [Header("Pause Game")]
    public GameObject pauseMenu;
    public GameObject settingMenu;
    public SelectStateManager selectStateManager;
    public KeyCode pauseGameKey;
    public GameObject[] shopAll;

    private void Update()
    {        
        if (Input.GetKeyDown(pauseGameKey))
        {
            if(selectStateManager != null)
            {
                if (selectStateManager.isSelectStateOpen)
                    return;
            }

            if (GameManager.Instance._OpenShopUI)
            {
                for (int i = 0; i < shopAll.Length; i++)
                {
                    shopAll[i].SetActive(false);
                }
                GameManager.Instance._OpenShopUI = false;
                return;
            }

            if (GameManager.Instance._isStatusOpen || GameManager.Instance._isInvOpen)
            {
                GameManager.Instance._isStatusOpen = false;
                GameManager.Instance._isInvOpen = false;
                GameManager.Instance.ToggleInv(false);
                GameManager.Instance.ToggleStatusUI(false);
            }

            GameManager.Instance._gamePause = !GameManager.Instance._gamePause;

            if (GameManager.Instance._gamePause)
            {
                GamePause();
            }
            else
            {
                GameResume();
            }

        }

    }
    public void GamePause()
    {
        GameManager.Instance._gamePause = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void GameResume()
    {
        GameManager.Instance._gamePause = false;
        pauseMenu.SetActive(false);
        settingMenu.SetActive(false);
        Time.timeScale = 1;
    }

}
