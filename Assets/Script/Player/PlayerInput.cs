//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Script/Player/PlayerInput.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInput: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""PlayerS_Movement"",
            ""id"": ""c8b987d2-91e5-4f1b-9193-fb9e8d4937c7"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""b0ad8b6f-487b-4f9e-8d1c-44e71e7c336c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""PassThrough"",
                    ""id"": ""71302029-2f97-4c89-a8eb-d4c0f5dcf78d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""PassThrough"",
                    ""id"": ""89ddb59b-ef07-484d-96cf-7fd41609dae2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""PassThrough"",
                    ""id"": ""927defea-9554-4f50-acad-bcc3f09b0af8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""PassThrough"",
                    ""id"": ""de4149f5-99bf-47b7-8c1d-286e40b715a8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""QuickHeal"",
                    ""type"": ""PassThrough"",
                    ""id"": ""387a687d-c31e-45ab-b8fa-a302b305ce52"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""UseDM"",
                    ""type"": ""PassThrough"",
                    ""id"": ""83273dce-a153-49b0-81f3-e88e0600412d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Skill"",
                    ""type"": ""PassThrough"",
                    ""id"": ""09703eee-6de6-4bd4-8786-c34f064c318b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Pairry"",
                    ""type"": ""Button"",
                    ""id"": ""accd70a4-e0f3-4df1-8be9-065312fc6386"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""AD"",
                    ""id"": ""647d2dec-a006-4229-ba54-4b76e63b3a7c"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""left"",
                    ""id"": ""6d859610-5651-42d3-8f0c-b7fefa6ac2a8"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""80f55ab0-f09c-401e-a814-36fe88686c41"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left Stick"",
                    ""id"": ""afa18c39-3f7b-4174-805e-549e376b69d7"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""left"",
                    ""id"": ""d873edf0-4077-4e16-a7e0-f84d6e2642e6"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""f55d32d2-d5f4-4174-9102-6268e1a267d0"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""455fa2a3-4866-4731-bd18-df9abe001122"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1a0da0a5-c934-4748-9799-1c1079592ed4"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""65429481-df4f-47e2-9a9c-a06a6a22877c"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0410cb10-527b-4831-9eeb-883e08c85477"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5a3a2a2e-da8e-4e80-8806-bbf0c45fd3d7"",
                    ""path"": ""<Keyboard>/u"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""QuickHeal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aa5a890e-6481-4b42-ba25-98f9966a1b29"",
                    ""path"": ""<Keyboard>/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UseDM"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0d82484e-09ce-42fb-af64-570c5675b480"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skill"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""20c1de17-9831-4f40-b273-628bc942664e"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pairry"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Player3rd Movement"",
            ""id"": ""225e1e64-a8a2-40bd-9c2d-e9d9217aaf23"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""1c6803f5-14e6-4380-847e-0a02a60ffb3f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""PassThrough"",
                    ""id"": ""7dae1b74-9b0f-45e0-bd5b-22f0d661c43c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Inventory"",
                    ""type"": ""PassThrough"",
                    ""id"": ""5e151b63-fa8e-44c2-8657-f2507c1f6d1b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""OpenShop"",
                    ""type"": ""PassThrough"",
                    ""id"": ""f9e4f178-e6e0-4b37-9f0b-a9e1965561da"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""StatusUI"",
                    ""type"": ""PassThrough"",
                    ""id"": ""b9c0460b-58b7-4d92-8284-3788e714b1db"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""0d5dce8c-f62a-4d12-902e-e48d366b1628"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""f28a7aee-3dca-4fdb-9622-f1830afdb144"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""af9a61d0-7efc-4951-bcdc-3a30625ae76d"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""f1d7bcfc-273d-433f-b465-2965133ae639"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""c7dd9654-e8b8-422a-90c4-45ba57cc32b2"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left Stick"",
                    ""id"": ""2aac83e1-52da-4e31-b8af-e7bbaafa37ca"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""be1fc6e7-0539-4fdd-a8d1-edce82013fcc"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""8839b589-7c2d-4ac5-940c-c283c3886f2f"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""3f5d13e5-f5fb-4a9c-83a8-0991c20e24a3"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""4eab6726-576e-4e25-a3fa-4bb883078436"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""1328d21a-18d7-476d-96f3-30e953595668"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5e9bbe4a-c0e8-469f-bd0d-5976a2c1184d"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b1a221c0-6130-4b9c-a36a-76475b5a111d"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenShop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dc312a5c-a103-4dbb-a9b1-1d3d6b542371"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""StatusUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerS_Movement
        m_PlayerS_Movement = asset.FindActionMap("PlayerS_Movement", throwIfNotFound: true);
        m_PlayerS_Movement_Movement = m_PlayerS_Movement.FindAction("Movement", throwIfNotFound: true);
        m_PlayerS_Movement_Jump = m_PlayerS_Movement.FindAction("Jump", throwIfNotFound: true);
        m_PlayerS_Movement_Dash = m_PlayerS_Movement.FindAction("Dash", throwIfNotFound: true);
        m_PlayerS_Movement_Attack = m_PlayerS_Movement.FindAction("Attack", throwIfNotFound: true);
        m_PlayerS_Movement_Interact = m_PlayerS_Movement.FindAction("Interact", throwIfNotFound: true);
        m_PlayerS_Movement_QuickHeal = m_PlayerS_Movement.FindAction("QuickHeal", throwIfNotFound: true);
        m_PlayerS_Movement_UseDM = m_PlayerS_Movement.FindAction("UseDM", throwIfNotFound: true);
        m_PlayerS_Movement_Skill = m_PlayerS_Movement.FindAction("Skill", throwIfNotFound: true);
        m_PlayerS_Movement_Pairry = m_PlayerS_Movement.FindAction("Pairry", throwIfNotFound: true);
        // Player3rd Movement
        m_Player3rdMovement = asset.FindActionMap("Player3rd Movement", throwIfNotFound: true);
        m_Player3rdMovement_Movement = m_Player3rdMovement.FindAction("Movement", throwIfNotFound: true);
        m_Player3rdMovement_Interact = m_Player3rdMovement.FindAction("Interact", throwIfNotFound: true);
        m_Player3rdMovement_Inventory = m_Player3rdMovement.FindAction("Inventory", throwIfNotFound: true);
        m_Player3rdMovement_OpenShop = m_Player3rdMovement.FindAction("OpenShop", throwIfNotFound: true);
        m_Player3rdMovement_StatusUI = m_Player3rdMovement.FindAction("StatusUI", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // PlayerS_Movement
    private readonly InputActionMap m_PlayerS_Movement;
    private List<IPlayerS_MovementActions> m_PlayerS_MovementActionsCallbackInterfaces = new List<IPlayerS_MovementActions>();
    private readonly InputAction m_PlayerS_Movement_Movement;
    private readonly InputAction m_PlayerS_Movement_Jump;
    private readonly InputAction m_PlayerS_Movement_Dash;
    private readonly InputAction m_PlayerS_Movement_Attack;
    private readonly InputAction m_PlayerS_Movement_Interact;
    private readonly InputAction m_PlayerS_Movement_QuickHeal;
    private readonly InputAction m_PlayerS_Movement_UseDM;
    private readonly InputAction m_PlayerS_Movement_Skill;
    private readonly InputAction m_PlayerS_Movement_Pairry;
    public struct PlayerS_MovementActions
    {
        private @PlayerInput m_Wrapper;
        public PlayerS_MovementActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_PlayerS_Movement_Movement;
        public InputAction @Jump => m_Wrapper.m_PlayerS_Movement_Jump;
        public InputAction @Dash => m_Wrapper.m_PlayerS_Movement_Dash;
        public InputAction @Attack => m_Wrapper.m_PlayerS_Movement_Attack;
        public InputAction @Interact => m_Wrapper.m_PlayerS_Movement_Interact;
        public InputAction @QuickHeal => m_Wrapper.m_PlayerS_Movement_QuickHeal;
        public InputAction @UseDM => m_Wrapper.m_PlayerS_Movement_UseDM;
        public InputAction @Skill => m_Wrapper.m_PlayerS_Movement_Skill;
        public InputAction @Pairry => m_Wrapper.m_PlayerS_Movement_Pairry;
        public InputActionMap Get() { return m_Wrapper.m_PlayerS_Movement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerS_MovementActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerS_MovementActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerS_MovementActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerS_MovementActionsCallbackInterfaces.Add(instance);
            @Movement.started += instance.OnMovement;
            @Movement.performed += instance.OnMovement;
            @Movement.canceled += instance.OnMovement;
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
            @Dash.started += instance.OnDash;
            @Dash.performed += instance.OnDash;
            @Dash.canceled += instance.OnDash;
            @Attack.started += instance.OnAttack;
            @Attack.performed += instance.OnAttack;
            @Attack.canceled += instance.OnAttack;
            @Interact.started += instance.OnInteract;
            @Interact.performed += instance.OnInteract;
            @Interact.canceled += instance.OnInteract;
            @QuickHeal.started += instance.OnQuickHeal;
            @QuickHeal.performed += instance.OnQuickHeal;
            @QuickHeal.canceled += instance.OnQuickHeal;
            @UseDM.started += instance.OnUseDM;
            @UseDM.performed += instance.OnUseDM;
            @UseDM.canceled += instance.OnUseDM;
            @Skill.started += instance.OnSkill;
            @Skill.performed += instance.OnSkill;
            @Skill.canceled += instance.OnSkill;
            @Pairry.started += instance.OnPairry;
            @Pairry.performed += instance.OnPairry;
            @Pairry.canceled += instance.OnPairry;
        }

        private void UnregisterCallbacks(IPlayerS_MovementActions instance)
        {
            @Movement.started -= instance.OnMovement;
            @Movement.performed -= instance.OnMovement;
            @Movement.canceled -= instance.OnMovement;
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
            @Dash.started -= instance.OnDash;
            @Dash.performed -= instance.OnDash;
            @Dash.canceled -= instance.OnDash;
            @Attack.started -= instance.OnAttack;
            @Attack.performed -= instance.OnAttack;
            @Attack.canceled -= instance.OnAttack;
            @Interact.started -= instance.OnInteract;
            @Interact.performed -= instance.OnInteract;
            @Interact.canceled -= instance.OnInteract;
            @QuickHeal.started -= instance.OnQuickHeal;
            @QuickHeal.performed -= instance.OnQuickHeal;
            @QuickHeal.canceled -= instance.OnQuickHeal;
            @UseDM.started -= instance.OnUseDM;
            @UseDM.performed -= instance.OnUseDM;
            @UseDM.canceled -= instance.OnUseDM;
            @Skill.started -= instance.OnSkill;
            @Skill.performed -= instance.OnSkill;
            @Skill.canceled -= instance.OnSkill;
            @Pairry.started -= instance.OnPairry;
            @Pairry.performed -= instance.OnPairry;
            @Pairry.canceled -= instance.OnPairry;
        }

        public void RemoveCallbacks(IPlayerS_MovementActions instance)
        {
            if (m_Wrapper.m_PlayerS_MovementActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerS_MovementActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerS_MovementActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerS_MovementActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerS_MovementActions @PlayerS_Movement => new PlayerS_MovementActions(this);

    // Player3rd Movement
    private readonly InputActionMap m_Player3rdMovement;
    private List<IPlayer3rdMovementActions> m_Player3rdMovementActionsCallbackInterfaces = new List<IPlayer3rdMovementActions>();
    private readonly InputAction m_Player3rdMovement_Movement;
    private readonly InputAction m_Player3rdMovement_Interact;
    private readonly InputAction m_Player3rdMovement_Inventory;
    private readonly InputAction m_Player3rdMovement_OpenShop;
    private readonly InputAction m_Player3rdMovement_StatusUI;
    public struct Player3rdMovementActions
    {
        private @PlayerInput m_Wrapper;
        public Player3rdMovementActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player3rdMovement_Movement;
        public InputAction @Interact => m_Wrapper.m_Player3rdMovement_Interact;
        public InputAction @Inventory => m_Wrapper.m_Player3rdMovement_Inventory;
        public InputAction @OpenShop => m_Wrapper.m_Player3rdMovement_OpenShop;
        public InputAction @StatusUI => m_Wrapper.m_Player3rdMovement_StatusUI;
        public InputActionMap Get() { return m_Wrapper.m_Player3rdMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Player3rdMovementActions set) { return set.Get(); }
        public void AddCallbacks(IPlayer3rdMovementActions instance)
        {
            if (instance == null || m_Wrapper.m_Player3rdMovementActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_Player3rdMovementActionsCallbackInterfaces.Add(instance);
            @Movement.started += instance.OnMovement;
            @Movement.performed += instance.OnMovement;
            @Movement.canceled += instance.OnMovement;
            @Interact.started += instance.OnInteract;
            @Interact.performed += instance.OnInteract;
            @Interact.canceled += instance.OnInteract;
            @Inventory.started += instance.OnInventory;
            @Inventory.performed += instance.OnInventory;
            @Inventory.canceled += instance.OnInventory;
            @OpenShop.started += instance.OnOpenShop;
            @OpenShop.performed += instance.OnOpenShop;
            @OpenShop.canceled += instance.OnOpenShop;
            @StatusUI.started += instance.OnStatusUI;
            @StatusUI.performed += instance.OnStatusUI;
            @StatusUI.canceled += instance.OnStatusUI;
        }

        private void UnregisterCallbacks(IPlayer3rdMovementActions instance)
        {
            @Movement.started -= instance.OnMovement;
            @Movement.performed -= instance.OnMovement;
            @Movement.canceled -= instance.OnMovement;
            @Interact.started -= instance.OnInteract;
            @Interact.performed -= instance.OnInteract;
            @Interact.canceled -= instance.OnInteract;
            @Inventory.started -= instance.OnInventory;
            @Inventory.performed -= instance.OnInventory;
            @Inventory.canceled -= instance.OnInventory;
            @OpenShop.started -= instance.OnOpenShop;
            @OpenShop.performed -= instance.OnOpenShop;
            @OpenShop.canceled -= instance.OnOpenShop;
            @StatusUI.started -= instance.OnStatusUI;
            @StatusUI.performed -= instance.OnStatusUI;
            @StatusUI.canceled -= instance.OnStatusUI;
        }

        public void RemoveCallbacks(IPlayer3rdMovementActions instance)
        {
            if (m_Wrapper.m_Player3rdMovementActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayer3rdMovementActions instance)
        {
            foreach (var item in m_Wrapper.m_Player3rdMovementActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_Player3rdMovementActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public Player3rdMovementActions @Player3rdMovement => new Player3rdMovementActions(this);
    public interface IPlayerS_MovementActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnQuickHeal(InputAction.CallbackContext context);
        void OnUseDM(InputAction.CallbackContext context);
        void OnSkill(InputAction.CallbackContext context);
        void OnPairry(InputAction.CallbackContext context);
    }
    public interface IPlayer3rdMovementActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnInventory(InputAction.CallbackContext context);
        void OnOpenShop(InputAction.CallbackContext context);
        void OnStatusUI(InputAction.CallbackContext context);
    }
}
