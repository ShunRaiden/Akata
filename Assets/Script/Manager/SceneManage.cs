using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    #region Singleton
    public static SceneManage instance { get { return _instance; } }
    private static SceneManage _instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Destroy the new instance, not this script
            return;
        }

        _instance = this;
    }
    #endregion

    public LoadingScreen loadingScreen;

    //NewGameStart With BackStory
    #region NewGameStart
    public void NewGameStart(string sceneName)
    {
        if (!PlayerPrefs.HasKey("NewGame"))
        {
            PlayerPrefs.SetInt("NewGame", 0);
            PlayerPrefs.Save();
            StartCoroutine(loadingScreen.LoadingScreenTimer(sceneName)); //Load Cutscene
        }
        else
        {
            PlayerPrefs.SetInt("NewGame", 1);
            PlayerPrefs.Save();
            StartCoroutine(loadingScreen.LoadingScreenTimer(sceneName)); //Load Game
        }
    }
    #endregion

    //NoLoadingScreen
    #region NextScene
    public void NextScene()
    {
        GameManager.Instance._gamePause = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);       
    }
    #endregion  

    //WithLoadingScreen
    #region LoadTargetScene
    public void LoadTargetScene(string sceneName)
    {
        GameManager.Instance._gamePause = false;
        SavePlayerPos(true);
        StartCoroutine(loadingScreen.LoadingScreenTimer(sceneName));       
    }

    public void LoadTargetScene2(string sceneName)
    {
        StartCoroutine(loadingScreen.LoadingScreenTimer(sceneName));
    }

    public void GoToCenter()
    {
        GameManager.Instance._gamePause = false;
        StartCoroutine(loadingScreen.LoadingScreenTimer("Center"));
    }

    // Get State Selected index
    public void SetStateIndex(int stateInt)
    {
        GameManager.Instance.stateIndex = stateInt;
    }
    #endregion


    #region ReStartGame
    public void ReStartGame()
    {
        GameManager.Instance._gamePause = false;
        SavePlayerPos(false);
        StartCoroutine(loadingScreen.LoadingScreenTimer(SceneManager.GetActiveScene().name));
        Time.timeScale = 1;
    }
    #endregion
    #region BackToMenu
    public void BackToMenu()
    {
        GameManager.Instance._gamePause = false;
        SavePlayerPos(false);
        //PlayerPrefs.SetInt("State", 0);
        //ayerPrefs.SetInt("StoryCurrent", 0);
        StartCoroutine(loadingScreen.LoadingScreenTimer("MainMenu"));
    }
    #endregion

    public void ExitGame()
    {
        SavePlayerPos(false);//Build Del it**
        Application.Quit();
    }

    public void DeleteAllSave()
    {
        PlayerPrefs.DeleteAll();
    }

    public void SavePlayerPos(bool isState)
    {
        CenterManager centerManager = FindObjectOfType<CenterManager>();

        if(centerManager != null)
        {
            CenterManager.instance._isState = isState;
            CenterManager.instance.SaveData();
            
        }
        
    }
}
