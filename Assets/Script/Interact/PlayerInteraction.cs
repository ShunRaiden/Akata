using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using KinematicCharacterController.Player3rd;

public class PlayerInteraction : MonoBehaviour
{
    #region Singleton
    public static PlayerInteraction instance { get { return _instance; } }
    private static PlayerInteraction _instance;

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

    PlayerInput p_Input;

    InteractionUI interactionUI;
    public bool _interact;
    public bool _CanInteract;
    public bool _OpenShop;
    public bool _CanOpenShop;

    [Header("Camera Cinemachine")]
    public GameObject defaultCam;
    public GameObject zoomCam;
    public GameObject mikoCam;

    public PlayerCenterMovement_LTS _playerControll;

    [Header("Debug")]
    public string debugText;

    private void Start()
    {
        interactionUI = FindAnyObjectByType<InteractionUI>();
        interactionUI.Hide();
    }

    public void OnTriggerStay(Collider other)
    {
        debugText = other.name;

        if (other.gameObject.tag == "Interact")
        {
            var interactable = other.gameObject.GetComponent<IInteractable>();

            if(_CanInteract)
            {
                interactionUI.Show(interactable.GetInteractionText(), interactable.GetInteractionUIText());
            }
            else
            {
                interactionUI.Hide();
            }
            
            if (_interact)
            {
                interactable.Interact();
                _CanInteract = false;
            }

            if (_OpenShop)
            {
                interactable.OpenNPCUI();
                _OpenShop = false;
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Interact")
        {
            interactionUI.Hide();
        }
    }

}
