using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController;
using KinematicCharacterController.Examples;
using UnityEngine.Windows;

namespace KinematicCharacterController.Examples
{
    public class ExamplePlayer : MonoBehaviour
    {
        public ExampleCharacterController Character;
        public PlayerInteraction interaction;
        public bool canJump;
        public bool canMove;

        PlayerInput p_Input;

        public Vector2 movementInput;
        public bool _interact;
        public bool _canInteract;
        public Animator anim;

        //public ExampleCharacterCamera CharacterCamera;

        /*private const string MouseXInput = "Mouse X";
        private const string MouseYInput = "Mouse Y";
        private const string MouseScrollInput = "Mouse ScrollWheel";*/
        private const string HorizontalInput = "Horizontal";
        private const string VerticalInput = "Vertical";

        void OnEnable()
        {
            p_Input = new PlayerInput();
            p_Input.Player3rdMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            p_Input.Player3rdMovement.Interact.performed += i => _interact = i.ReadValue<float>() > 0.5f;

            p_Input.Enable();
        }

        void OnDisable()
        {
            // Disable the Input Actions
            p_Input.Disable();
        }

        private void Start()
        {
            
            //Cursor.lockState = CursorLockMode.Locked;

            // Tell camera to follow transform
            //CharacterCamera.SetFollowTransform(Character.CameraFollowPoint);

            // Ignore the character's collider(s) for camera obstruction checks
            //CharacterCamera.IgnoredColliders.Clear();
            //CharacterCamera.IgnoredColliders.AddRange(Character.GetComponentsInChildren<Collider>());
        }

        private void Update()
        {
            /*if (Input.GetMouseButtonDown(0))
            {
                Cursor.lockState = CursorLockMode.Locked;
            }*/

            HandleCharacterInput();
        }

        private void LateUpdate()
        {
            // Handle rotating the camera along with physics movers
            /*if (CharacterCamera.RotateWithPhysicsMover && Character.Motor.AttachedRigidbody != null)
            {
                CharacterCamera.PlanarDirection = Character.Motor.AttachedRigidbody.GetComponent<PhysicsMover>().RotationDeltaFromInterpolation * CharacterCamera.PlanarDirection;
                CharacterCamera.PlanarDirection = Vector3.ProjectOnPlane(CharacterCamera.PlanarDirection, Character.Motor.CharacterUp).normalized;
            }*/

            HandleCameraInput();
        }

        private void HandleCameraInput()
        {
            // Create the look input vector for the camera
            //float mouseLookAxisUp = Input.GetAxisRaw(MouseYInput);
            //float mouseLookAxisRight = Input.GetAxisRaw(MouseXInput);
            //Vector3 lookInputVector = new Vector3(mouseLookAxisRight, mouseLookAxisUp, 0f);

            // Prevent moving the camera while the cursor isn't locked
            /*if (Cursor.lockState != CursorLockMode.Locked)
            {
                lookInputVector = Vector3.zero;
            }*/

            // Input for zooming the camera (disabled in WebGL because it can cause problems)
            //float scrollInput = -Input.GetAxis(MouseScrollInput);
#if UNITY_WEBGL
        scrollInput = 0f;
#endif

            // Apply inputs to the camera
            //CharacterCamera.UpdateWithInput(Time.deltaTime, scrollInput, lookInputVector);

            // Handle toggling zoom level
            /*if (Input.GetMouseButtonDown(1))
            {
                CharacterCamera.TargetDistance = (CharacterCamera.TargetDistance == 0f) ? CharacterCamera.DefaultDistance : 0f;
            }*/
        }

        private void HandleCharacterInput()
        {
            PlayerCharacterInputs characterInputs = new PlayerCharacterInputs();
            PlayerCompanent companent = new PlayerCompanent();

            // Build the CharacterInputs struct
            characterInputs.MoveAxisRight = movementInput.x;
            characterInputs.MoveAxisForward = movementInput.y;          
            //characterInputs.CameraRotation = CharacterCamera.Transform.rotation;
            //characterInputs.JumpDown = Input.GetKeyDown(KeyCode.Space);
            //characterInputs.CrouchDown = Input.GetKeyDown(KeyCode.C);
            //characterInputs.CrouchUp = Input.GetKeyUp(KeyCode.C);
            characterInputs.canJump = canJump;
            characterInputs.canMove = canMove;
            companent.anim = anim;

            _canInteract = interaction._CanInteract;
            if (_canInteract)
            {
                interaction._interact = _interact;
            }
            else
            {
                interaction._interact = _canInteract;
            }

            // Apply inputs to character
            Character.SetInputs(ref characterInputs, ref companent);
        }
    }
}