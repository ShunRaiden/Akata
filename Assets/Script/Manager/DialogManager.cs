using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Rendering;
using System;
using UnityEngine.Playables;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public GameObject dialogUI;
    public TMP_Text speakerNameText;
    public TextMeshProUGUI dialogText;
    public float typingSpeed = 0.05f;
    public DialogData data;
    public Image speakerImage;
    public Sprite samuraiImage;

    private Queue<string> sentences;

    public PlayableDirector aeTimeline;

    [HideInInspector] public bool _isDialogueStart;
    [SerializeField] bool stopDialogue;
    bool conDialogue;

    [Header("Audio")]
    public bool stopAuido;
    AudioSource audioSource;
    AudioClip audioClip;

    [Range(1, 5)]
    [SerializeField] int frequencyOfSound = 2;

    void Start()
    {
        sentences = new Queue<string>();
        audioSource = AudioManager.instance.d_AudioSource;
        audioClip = AudioManager.instance.s_Dialouge;

    }

    private void Update()
    {
        if (Input.anyKeyDown && _isDialogueStart)
        {
            stopDialogue = true;
        }

        if (Input.anyKeyDown && !conDialogue)
        {
            conDialogue = true;
        }
    }

    #region Start Dialog
    public void StartDialogue(DialogData dialogData, string speakerName, Sprite speakerImg, bool isMiko)
    {
        _isDialogueStart = true;

        GameManager.Instance._questManager.questUICanvas.SetActive(false);
        //ZoomCam
        PlayerInteraction.instance.defaultCam.SetActive(false);

        if (isMiko)
            PlayerInteraction.instance.mikoCam.SetActive(true);
        else
            PlayerInteraction.instance.zoomCam.SetActive(true);

        PlayerInteraction.instance._playerControll.canMove = false;

        dialogUI.SetActive(true);
        data = dialogData;

        speakerNameText.text = speakerName;
        speakerImage.sprite = speakerImg;


        sentences.Clear();

        foreach (string sentence in data.data)
        {
            sentences.Enqueue(sentence);
        }

        if (dialogData.haveAE)
            StartCoroutine(DisplayDialogAE(dialogData, speakerName, speakerImg));
        else
            StartCoroutine(DisplayDialog(dialogData, speakerName, speakerImg));
    }

    #region Default Dialog
    IEnumerator DisplayDialog(DialogData dialogData, string speakerName, Sprite speakerImg)
    {
        Queue<string> sentencesCopy = new Queue<string>(sentences);
        foreach (string sentence in sentencesCopy)
        {
            stopDialogue = false;

            dialogText.text = "";  // Clear the text before typing each sentence
            dialogText.maxVisibleCharacters = 0;
            foreach (char letter in sentence.ToCharArray())
            {
                //New
                if (stopDialogue)
                {
                    dialogText.text = sentence;
                    dialogText.maxVisibleCharacters = sentence.Length;
                    break;
                }

                Debug.Log(dialogText.maxVisibleCharacters);
                PlayDialogueSound(dialogText.maxVisibleCharacters);

                dialogText.text += letter;
                dialogText.maxVisibleCharacters++;

                yield return new WaitForSeconds(typingSpeed);

            }

            yield return new WaitForSeconds(0.1f);
            conDialogue = false;
            yield return new WaitUntil(() => conDialogue);

            if (dialogData.switchChat && speakerName == speakerNameText.text)
            {
                speakerNameText.text = dialogData.samuraiName;
                speakerImage.sprite = samuraiImage;
            }
            else if (dialogData.switchChat)
            {
                speakerNameText.text = speakerName;
                speakerImage.sprite = speakerImg;
            }


            yield return new WaitForSeconds(0.1f);
        }

        dialogUI.SetActive(false);
        //backToMain Cam
        PlayerInteraction.instance.defaultCam.SetActive(true);
        PlayerInteraction.instance.zoomCam.SetActive(false);
        PlayerInteraction.instance.mikoCam.SetActive(false);

        yield return new WaitForSeconds(1f);
        PlayerInteraction.instance._playerControll.canMove = true;
        PlayerInteraction.instance._CanInteract = true;

        GameManager.Instance._questManager.questUICanvas.SetActive(true);
        _isDialogueStart = false;
        // Dialog is complete, you can add logic for when the conversation ends.
    }
    #endregion

    #region AE Dialog
    IEnumerator DisplayDialogAE(DialogData dialogData, string speakerName, Sprite speakerImg)
    {
        int index = 0;

        Queue<string> sentencesCopy = new Queue<string>(sentences);
        foreach (string sentence in sentencesCopy)
        {
            stopDialogue = false;

            dialogText.text = "";  // Clear the text before typing each sentence
            dialogText.maxVisibleCharacters = 0;
            foreach (char letter in sentence.ToCharArray())
            {
                //New
                if (stopDialogue)
                {
                    dialogText.text = sentence;
                    dialogText.maxVisibleCharacters = sentence.Length;
                    break;
                }

                Debug.Log(dialogText.maxVisibleCharacters);
                PlayDialogueSound(dialogText.maxVisibleCharacters);

                dialogText.text += letter;
                dialogText.maxVisibleCharacters++;

                yield return new WaitForSeconds(typingSpeed);

            }

            yield return new WaitForSeconds(0.1f);
            conDialogue = false;
            yield return new WaitUntil(() => conDialogue);

            if (index == dialogData.aeOpenIndex)
            {
                aeTimeline.Play();

                while (aeTimeline.state == PlayState.Playing)
                {
                    yield return null;
                }
                Debug.Log("end");
            }

            index++;

            yield return new WaitForSeconds(0.1f);

            if (dialogData.switchChat && speakerName == speakerNameText.text)
            {
                speakerNameText.text = dialogData.samuraiName;
                speakerImage.sprite = samuraiImage;
            }
            else if (dialogData.switchChat)
            {
                speakerNameText.text = speakerName;
                speakerImage.sprite = speakerImg;
            }


        }

        dialogUI.SetActive(false);
        //backToMain Cam
        PlayerInteraction.instance.defaultCam.SetActive(true);
        PlayerInteraction.instance.zoomCam.SetActive(false);

        yield return new WaitForSeconds(1f);
        PlayerInteraction.instance._playerControll.canMove = true;
        PlayerInteraction.instance._CanInteract = true;

        GameManager.Instance._questManager.questUICanvas.SetActive(true);
        _isDialogueStart = false;
        // Dialog is complete, you can add logic for when the conversation ends.        
    }
    #endregion

    #region Sound
    private void PlayDialogueSound(int currentCharacterCount)
    {
        if (currentCharacterCount % frequencyOfSound == 0)
        {
            if (stopAuido)
            {
                audioSource.Stop();
            }
            audioSource.PlayOneShot(audioClip);
        }
    }
    #endregion

    #endregion
}
