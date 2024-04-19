using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static CenterManager;
using static SaveItemInventoryData;

public class CenterManager : MonoBehaviour
{
    #region Singleton
    public static CenterManager instance { get { return _instance; } }
    private static CenterManager _instance;

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

    [Header("NPC")]
    public GameObject[] _npcInteractControll;
    public int firstTalkCurrent;
    public int _dialogUpdate;
    [SerializeField] int deBugDialogue;
    [SerializeField] bool isDebug;

    public Vector3 playerTrans;
    public Vector3 afterStateTrans;
    public bool _isState;
    public GameObject player;
    [SerializeField]string transData;

    private void Start()
    {
        if(isDebug)
            dialogUpdate = deBugDialogue;

        if (PlayerPrefs.HasKey("LastPlayerPosition"))
        {
            LoadData();           
        }
        else
        {
            SaveData();
            player.transform.position = playerTrans;
        }
        player.SetActive(true);
    }

    public void SaveData()
    {
        if(_isState)
            playerTrans = afterStateTrans;
        else
            playerTrans = player.transform.position;

        transData = JsonUtility.ToJson(playerTrans);
        PlayerPrefs.SetString("LastPlayerPosition", transData);
    }
    public void LoadData()
    {
        string data = PlayerPrefs.GetString("LastPlayerPosition");
        playerTrans = JsonUtility.FromJson<Vector3>(data);
        player.transform.position = playerTrans;
    }

    public int dialogUpdate
    {
        get
        {
            if (!PlayerPrefs.HasKey("DialogIndex"))
            {
                _dialogUpdate = 0;
                Debug.Log("Why Me");
                PlayerPrefs.SetInt("DialogIndex", _dialogUpdate);
            }
            else
            {
                _dialogUpdate = PlayerPrefs.GetInt("DialogIndex");
            }

            return _dialogUpdate;
        }
        set
        {

            _dialogUpdate = value;
            Debug.Log("_dialogUpdate :" + _dialogUpdate);
            PlayerPrefs.SetInt("DialogIndex", _dialogUpdate);
            PlayerPrefs.Save(); // Ensure you save the changes to PlayerPrefs
        }
    }
    public void CheckFT()
    {
        if (PlayerPrefs.HasKey("FirstTalkNPC"))
            return;

        if (firstTalkCurrent < _npcInteractControll.Length)
        {
            firstTalkCurrent++;
            QuestManager.instance.questNavInfo.text = firstTalkCurrent.ToString() + " / " + _npcInteractControll.Length;
            Debug.Log("FT : " + firstTalkCurrent);
            Debug.Log("NPC L : " + _npcInteractControll.Length);

            if (firstTalkCurrent >= _npcInteractControll.Length)
            {
                Debug.Log("FT : Done");
                if (!PlayerPrefs.HasKey("FirstTalkNPC"))
                {
                    QuestManager.instance.questNavUI.SetActive(false);
                    QuestManager.instance.questNavUI2.SetActive(true);
                    dialogUpdate++;
                    PlayerPrefs.SetInt("FirstTalkNPC", 1);
                    PlayerPrefs.Save();
                    Debug.Log("FT : Save");
                }
            }
        }
    }
}
