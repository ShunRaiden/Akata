using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCenterMovement_LTS : MonoBehaviour
{
    public PlayerInteraction interaction;
    [SerializeField] CharacterController chaCon;

    public bool canJump;
    public bool canMove;

    PlayerInput p_Input;

    [Header("Movement")]
    public float speed = 6f;
    public float gravity = 20f;
    Vector3 moveDir = Vector3.zero;
    public Vector2 movementInput;
    public float horizontalInput;
    public float verticalInput;

    [Header("Rotation")]
    public Transform camObj;
    [SerializeField] private float rotationSpeed = 10f;

    [Header("Audio")]
    public AudioSource walk_audioSource;

    public bool _interact;
    public bool _OpenShop;
    public bool _canInteract;
    public bool _canOpenShop;
    public Animator anim;

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

    private void Start()
    {

        chaCon = GetComponent<CharacterController>();
        camObj = Camera.main.transform;
        anim = GetComponent<Animator>();

        walk_audioSource.enabled = false;
    }

    private void Update()
    {
        if (GameManager.Instance._gamePause)
        {
            walk_audioSource.enabled = false;
            return;
        }
        else
        {
            HandleCharacterInput();
            HandleMovementInput();
            moveDir.x = +horizontalInput * speed;
            moveDir.z = +verticalInput * speed;
            moveDir.y =- gravity * Time.deltaTime;
            HandleRotation();
            chaCon.Move(moveDir * Time.deltaTime);
        }

    }
    private void HandleCharacterInput()
    {
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

    }

    private void HandleMovementInput()
    {
        horizontalInput = movementInput.x;
        verticalInput = movementInput.y;

        if (!canMove)
        {
            horizontalInput = 0;
            verticalInput = 0;
        }
            

        if (horizontalInput > 0 || horizontalInput < 0 || verticalInput > 0 || verticalInput < 0)
        {

            anim.SetFloat("Move", 1, 0.2f, Time.deltaTime);
            walk_audioSource.enabled = true;

        }
        else
        {

            anim.SetFloat("Move", 0, 0.1f, Time.deltaTime);
            walk_audioSource.enabled = false;
        }
    }

    private void HandleRotation()
    {
        if (verticalInput == 0 && horizontalInput == 0)
            return;

        Vector3 targetDir = Vector3.zero;

        targetDir = camObj.forward * verticalInput;
        targetDir = targetDir+camObj.right* horizontalInput;
        targetDir.Normalize();
        targetDir.y = 0;

        Quaternion targetRotate = Quaternion.LookRotation(targetDir);
        Quaternion playerRotate = Quaternion.Slerp(transform.rotation, targetRotate, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotate;
    }
}
