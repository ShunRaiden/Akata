using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController;
using KinematicCharacterController.Examples;
using UnityEngine.Windows;

namespace KinematicCharacterController.Player3rd
{
    public class Player3rd : MonoBehaviour
    {
        public ExampleCharacterController Character;
        public PlayerInteraction interaction;
        public bool canJump;
        public bool canMove;

        PlayerInput p_Input;

        public Vector2 movementInput;
        public bool _interact;
        public bool _OpenShop;
        public bool _canInteract;
        public bool _canOpenShop;
        public Animator anim;

        public AudioSource walkSFX;

        void OnEnable()
        {
            p_Input = new PlayerInput();
            p_Input.Player3rdMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            p_Input.Player3rdMovement.Interact.performed += i => _interact = i.ReadValue<float>() > 0.5f;
            p_Input.Player3rdMovement.OpenShop.performed += i => _OpenShop = i.ReadValue<float>() > 0.5f;

            p_Input.Enable();
        }

        void OnDisable()
        {
            // Disable the Input Actions
            p_Input.Disable();
        }

        private void Update()
        {
            if (GameManager.Instance._gamePause)
            {
                walkSFX.enabled = false;
                return;
            }
            else
            {
                HandleCharacterInput();
            }               
            
        }
        private void HandleCharacterInput()
        {
            PlayerCharacterInputs characterInputs = new PlayerCharacterInputs();
            PlayerCompanent companent = new PlayerCompanent();
            characterInputs.MoveAxisRight = movementInput.x;
            characterInputs.MoveAxisForward = movementInput.y;
            characterInputs.canJump = canJump;
            characterInputs.canMove = canMove;
            companent.anim = anim;

            _canInteract = interaction._CanInteract;
            _canOpenShop = interaction._CanOpenShop;
            //
            if (_canInteract)
            {
                interaction._interact = _interact;
            }
            else
            {
                interaction._interact = _canInteract;
            }
            //
            if (_canOpenShop)
            {
                interaction._OpenShop = _OpenShop;
            }
            else
            {
                interaction._OpenShop = _canOpenShop;
            }

            if (!canMove)
            {
                characterInputs.MoveAxisRight = 0;
                characterInputs.MoveAxisForward = 0;
            }

            // Apply inputs to character
            Character.SetInputs(ref characterInputs, ref companent);

            if(characterInputs.MoveAxisForward != 0 || characterInputs.MoveAxisRight != 0)
            {
                walkSFX.enabled = true;
            }
            else
            {
                walkSFX.enabled = false;
            }

        }
    }

}