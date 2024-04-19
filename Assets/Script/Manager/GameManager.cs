using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController.Player3rd;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance { get { return _instance; } }
    private static GameManager _instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy the new instance, not this script
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        for (int i = 0; i < dontDeleteOBJ.Count; i++) // Fix the loop condition
        {
            DontDestroyOnLoad(dontDeleteOBJ[i].gameObject);
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        playerMaxHP = _playerStatus.levelData.levelStat[_playerStatus.levelData.currentLevel].max_HP;
        playerHP = playerMaxHP;
        gameEnd = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(openInvKey))
        {
            _isInvOpen = !_isInvOpen;
            ToggleInv(_isInvOpen);
            _isStatusOpen = false;
            ToggleStatusUI(_isStatusOpen);
            _inventoryScreen.UpdateScreen();
        }

        if (Input.GetKeyDown(openStatusKey))
        {
            _isStatusOpen = !_isStatusOpen;
            ToggleStatusUI(_isStatusOpen);
            _isInvOpen = false;
            ToggleInv(_isInvOpen);
        }
    }


    public void ToggleInv(bool isOpen)
    {
        invScreen.SetActive(isOpen);
        
        if (isOpen)
        {
            _inventoryScreen.infoDisplays.SetActive(false);
            _inventoryScreen.UpdateScreen();
        }            
        else
            _inventoryScreen.ClearItemInfo();
    }

    public void ToggleStatusUI(bool isOpen)
    {
        statusScreen.SetActive(_isStatusOpen);

        if(isOpen)
        _statusPlayerInfo.UpdateStatusPlayer();
    }

    public int enemyCount
    {
        get { return _enemyCount; }
        set
        {
            _enemyCount = value;
            if (_enemyCount == 0)
            {
                gameEnd = true;
                win_Canvas.SetActive(true);
                _dropItemManager.ItemRandomDrop();
            }
        }
    }

    public int playerHP
    {
        get { return _playerHP; }
        set
        {
            _playerHP = value;
            if (_playerHP <= 0)
            {
                gameEnd = true;
                lose_Canvas.SetActive(true);
            }
        }
    }


    public void WinCanVas(string targetScene)
    {
        int index = PlayerPrefs.GetInt("StatePass");

        if (index < stateIndex)
        {
            PlayerPrefs.SetInt("StatePass", stateIndex);
            PlayerPrefs.Save();
        }

        gameEnd = false;
        win_Canvas.SetActive(false);

        _questManager.UpdatePassQuest();
        _questManager.ShowQuestNPCSentUI(_questManager.currentData);
        _dropItemManager.ItemStateDrop();
        _inventoryScreen.UpdateScreen();
        SceneManage.instance.LoadTargetScene(targetScene);

    }

    public void BackToCenter(string targetScene)
    {
        SceneManage.instance.LoadTargetScene(targetScene);
    }

    public void ReStartState()
    {
        SceneManage.instance.ReStartGame();
    }

    [Header("Player")]
    public PlayerStatus _playerStatus;
    public int _playerHP;
    public int playerMaxHP;
    public int levelPlayer;
    public StatusPlayerInfo _statusPlayerInfo;
    public bool _OpenShopUI;

    [Header("Manager")]
    public AudioManager _audioManager;
    public QuestManager _questManager;
    public List<DontDelete> dontDeleteOBJ = new List<DontDelete>();

    [Header("Dialogue")]
    public DialogManager _dialogManager;
    public int _dialogUpdate;

    [Header("Inventory")]
    public InventoryScreen _inventoryScreen;
    public ItemInventory _itemInventory;
    public SaveItemInventoryData _saveItemInvData;
    public GameObject invScreen;
    public bool _isInvOpen;
    public KeyCode openInvKey;
    public bool _CanUseItem;

    [Header("Status")]
    public KeyCode openStatusKey;
    public GameObject statusScreen;
    public bool _isStatusOpen;

    [Header("State")]
    public SelectStateManager _selectStateManager;
    public bool gameWin;
    public int _enemyCount;
    public DropItemManager _dropItemManager;
    public GameObject win_Canvas;
    public GameObject lose_Canvas;
    public int stateIndex;

    [Header("Timing Controll")]
    public bool _gamePause;
    public bool gameEnd;

    public void DeleteALLPref()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

[System.Serializable]
public class PlayerStatus
{
    public LevelData levelData;

}

[System.Serializable]
public class DontDelete
{
    public GameObject gameObject;
}

