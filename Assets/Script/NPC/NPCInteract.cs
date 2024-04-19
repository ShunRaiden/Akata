using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class NPCInteract : MonoBehaviour, IInteractable
{
    public QuestData.NPC_All nPCName;
    string npcName;
    public string npcNameOutput;
    public int npcId;
    public Sprite npcImage;

    public bool isMiko;

    public DialogManager dialogManager;
    public List<DialogData> data = new List<DialogData>();
    public int currentDialog;

    public bool _firstTalk;
    public bool _ShopOpen;
    int _iSshopOpen;
    public GameObject shopUI;
    public GameObject prayUI;
    public GameObject cookUI;

    public Transform pivotPos;
    public Transform playerTransform; // Assign the player's GameObject's transform to this in the Unity Editor
    public float rotationSpeed = 5f;
    public bool isLooking = false;

    private Quaternion defaultRotation;

    private void Start()
    {
        npcName = nPCName.ToString();
        CenterManager.instance._npcInteractControll[npcId] = gameObject;

        if(PlayerPrefs.HasKey(nPCName.ToString() + "OpenUI"))
        {
            _iSshopOpen = PlayerPrefs.GetInt(nPCName.ToString() + "OpenUI");
            if (_iSshopOpen == 1)
            {
                _ShopOpen = true;
            }
        }

        defaultRotation = pivotPos.rotation;
    }

    private void Update()
    {
        if (!isLooking) return;

        if(dialogManager._isDialogueStart)
        {
            // Calculate direction to look at player, only rotating on the Y-axis
            Vector3 direction = playerTransform.position - pivotPos.position;
            direction.y = 0f; // Locking the direction to the horizontal plane
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Smoothly rotate towards the target rotation
            pivotPos.rotation = Quaternion.Slerp(pivotPos.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            // Reset rotation to default direction
            pivotPos.rotation = Quaternion.Slerp(pivotPos.rotation, defaultRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public string GetInteractionText()
    {
        return npcNameOutput + " : พูดคุย[E]";
    }

    public string GetInteractionUIText()
    {
        if (!_ShopOpen)
            return "";

        if (nPCName == QuestData.NPC_All.Boy)
        {
            return "[F] แลกเปลี่ยนของ";
        }
        else if (nPCName == QuestData.NPC_All.Chef)
        {
            return "[F] ทำอาหาร";
        }
        else if (nPCName == QuestData.NPC_All.Miko)
        {
            return "[F] สักการะเทพเจ้า";
        }
        else
        {
            return "";
        }


    }

    public void Interact()
    {
        currentDialog = CenterManager.instance.dialogUpdate;

        npcName = nPCName.ToString();

        if (currentDialog < data.Count)
        {
            dialogManager.StartDialogue(data[currentDialog], npcNameOutput , npcImage , isMiko);

            if (PlayerPrefs.HasKey("FirstTalkNPC"))
            {
                _firstTalk = true;
            }

            if (!_firstTalk || !PlayerPrefs.HasKey("FirstTalkNPC"))
            {
                if (_firstTalk)
                {
                    return;
                }
                CenterManager.instance.CheckFT();
                _firstTalk = true;
                return;
            }

            GameManager.Instance._questManager.npcTalkingCurrent = npcName;
            QuestManager qm = GameManager.Instance._questManager;

            if (qm.currentData == null || qm.currentData.questStatus == QuestData.QuestStatus.Completed)
            {
                qm.StartQuest();

                if (qm.currentData == null)
                    return;

                if (qm.currentData.questType == QuestData.QuestType.Talk && qm.currentData != null)
                {
                    qm.UpdateTalkQuest();
                    qm.StartQuest();
                }
            }
            else
            {
                qm.UpdateTalkQuest();
                if (qm.currentData.questType == QuestData.QuestType.Talk)
                {
                    qm.UpdateTalkQuest();
                    qm.StartQuest();
                    qm.UpdateQuestProgress(qm.currentData);
                }
            }
            

        }
        else
        {
            Debug.Log("Not Have Dialog");
            StartCoroutine(DisplayDialog());
        }


    }

    public void OpenNPCUI()
    {
        if (!_ShopOpen || GameManager.Instance._OpenShopUI)
            return;

        GameManager.Instance._OpenShopUI = true;

        if (nPCName == QuestData.NPC_All.Boy)
        {
            shopUI.SetActive(true);
        }
        else if (nPCName == QuestData.NPC_All.Chef)
        {
            cookUI.SetActive(true);
        }
        else if (nPCName == QuestData.NPC_All.Miko)
        {
            prayUI.SetActive(true);
            prayUI.GetComponent<ShopDisplays>().PrayUpdateInfo();        
        }

    }

    IEnumerator DisplayDialog()
    {
        yield return new WaitForSeconds(1f);
        PlayerInteraction.instance._CanInteract = true;
        // Dialog is complete, you can add logic for when the conversation ends.
    }
}
