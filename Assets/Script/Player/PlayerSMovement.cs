using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSMovement : MonoBehaviour
{

    public bool canMove = true;

    CharacterController chaCon;
    PlayerSInput p_Input;

    [Header("Movement")]
    public float speed = 6f;
    public float jumpSpeed = 8f;
    public float gravity = 20f;
    Vector3 moveDir = Vector3.zero;
    public Vector2 movementInput;
    public float horizontalInput;
    public bool jump;
    [Header("Dash")]
    public bool dash;
    public float dashSpeed;
    public float dashTime;
    public float lastMove;
    public float dashCDR;
    public float airTime;
    bool isAirDash;
    float dashCDRcurrent;
    bool canDash;

    [Header("Attack")]
    [SerializeField] private AttackAnimation attackAnim;
    public bool attack;
    public bool onAttack;

    [Header("Rotation")]
    public Transform camObj;
    [SerializeField] private float rotationSpeed = 150f;

    [Header("Animation")]
    public Animator anim;

    [Header("Debug")]
    public bool debugGround;

    [Header("Take Damage")]
    public StatusPlayerManager _statusPlayerManager;
    public float immuTime;

    void OnEnable()
    {
        // Enable the Input Actions
        if (chaCon == null)
        {
            p_Input = new PlayerSInput();

            if (!onAttack)
            {
                p_Input.PlayerS_Movement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
                p_Input.PlayerS_Movement.Jump.performed += i => jump = i.ReadValue<float>() > 0.5f;//¶éÒâ´´ºÑ¤Åº¹Õè¡Ñºjump
            }
            p_Input.PlayerS_Movement.Dash.performed += i => dash = i.ReadValue<float>() > 0.5f;
            p_Input.PlayerS_Movement.Attack.performed += i => attack = i.ReadValue<float>() > 0.5f;
        }

        p_Input.Enable();
    }

    void OnDisable()
    {
        // Disable the Input Actions
        p_Input.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        chaCon = GetComponent<CharacterController>();
        camObj = Camera.main.transform;
        anim = GetComponent<Animator>();
        canDash = true;
        isAirDash = true;

    }

    // Update is called once per frame
    void Update()
    {
        ExtraTime();

        debugGround = chaCon.isGrounded;

        HandleMovementInput();
        moveDir.x = +horizontalInput * speed;

        HandleDash();
        HandleAttack();

        if (chaCon.isGrounded)
        {
            //¡Ñ¹ºÑ¤
            /*if (Input.GetButton("Jump"))
            {
                moveDir.y = jumpSpeed;
            }*/

            if (jump)
            {
                anim.SetBool("Jump", true);
                moveDir.y = jumpSpeed;
                jump = false; // Reset jump after using it

            }
            anim.SetBool("Ground", true);
        }
        else
        {
            anim.SetBool("Jump", false);
            anim.SetBool("Ground", false);
            moveDir.y -= gravity * Time.deltaTime;
        }


        HandleRotation();

        chaCon.Move(moveDir * Time.deltaTime);

    }

    private void HandleMovementInput()
    {
        horizontalInput = movementInput.x;

        if (!canMove)
            horizontalInput = 0;

        if (movementInput.x != 0)
            lastMove = movementInput.x;


        if (horizontalInput > 0 || horizontalInput < 0)
        {
            anim.SetFloat("Horizontal", 1, 0.2f, Time.deltaTime);

        }
        else
        {
            anim.SetFloat("Horizontal", 0, 0.1f, Time.deltaTime);
        }
    }

    private void HandleRotation()
    {
        Vector3 targetDir = Vector3.zero;

        targetDir = targetDir + camObj.right * horizontalInput;
        targetDir.Normalize();
        targetDir.y = 0;

        if (targetDir == Vector3.zero)
            targetDir = transform.forward;

        Quaternion targetRotate = Quaternion.LookRotation(targetDir);
        Quaternion playerRotate = Quaternion.Slerp(transform.rotation, targetRotate, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotate;

    }

    private void HandleDash()
    {
        if (dashCDRcurrent > dashCDR)
        {
            canDash = true;
        }
        else
        {
            dashCDRcurrent += Time.deltaTime;
        }

        if (dash && canDash)
        {
            canDash = false;
            dashCDRcurrent = 0;
            StartCoroutine(Dash());
            dash = false;
        }

    }

    IEnumerator Dash()
    {
        float startTime = Time.time;
        while (Time.time < startTime + dashTime)
        {
            moveDir.x = +lastMove * dashSpeed;

            if (!chaCon.isGrounded)
            {
                moveDir.y = 0;
            }

            chaCon.Move(moveDir * Time.deltaTime);
            canMove = false;
            isAirDash = true;

            yield return null;
        }
        yield return new WaitForSeconds(airTime / 2);
        moveDir.y -= gravity * Time.deltaTime;
        isAirDash = false;

        yield return new WaitForSeconds(airTime / 2);
        canMove = true;
    }

    private void HandleAttack()
    {
        if (attack)
        {
            canMove = false;
            attackAnim.Attack();

            dash = false;
        }
    }

    public void ExtraTime()
    {
        immuTime -= Time.deltaTime;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (immuTime < 0)
        {
            if (other.gameObject.tag == "EnemyHit")
            {
                immuTime = 0.1f;
                int dm = other.GetComponent<EnemyStatus>().damge;
                _statusPlayerManager.TakeDamage(dm);
            }

        }
    }
}
