using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class StateManager : MonoBehaviour
{
    #region Singleton
    public static StateManager instance { get { return _instance; } }
    private static StateManager _instance;

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

    int _enemyCount;

    public PlayerMovement_LST playerSc;
    [Header("Stamina")]
    public float staminaCurrent;
    public TMP_Text staminaText;
    public Image staminaImage;
    [Header("Lock Actions")]
    public List<LockActionManager> lockActions = new List<LockActionManager>();
    public int lockIndex;
    public int enemyCount
    {
        get { return _enemyCount; }
        set
        {
            _enemyCount = value;

            if(lockIndex!= -1)
                lockActions[lockIndex].LockAction();

            GameManager.Instance.enemyCount = _enemyCount;
        }
    }

    // Update is called once per frame
    void Update()
    {
        staminaCurrent = playerSc.staminaCurrent;
        staminaText.text = ""+staminaCurrent;
        staminaImage.fillAmount = staminaCurrent / 100;
    }
}
