using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.Collections.AllocatorManager;
using UnityEngine.ParticleSystemJobs;

public class PlayerMovement_LST : MonoBehaviour
{
    #region Get Method
    [Header("[ Other Component ]")]
    [SerializeField] CharacterController chaCon;
    [SerializeField] CameraControllerPlayerS camCtrl;
    PlayerInput p_Input;
    #endregion

    #region Movement Paramiter
    [Header("[ Movement ]")]
    public bool canMove = true;
    public float speed = 6f;
    public float idleAnimTimer = 6f;
    public float timer;

    [Header("[ Rotation ]")]
    public Transform camObj;
    [SerializeField] private float rotationSpeed = 150f;
    #endregion

    #region Jump Paramiter
    [Header("[ Jump ]")]
    public float jumpSpeed = 8f;
    Rigidbody rb;
    public float gravity;
    Vector3 moveDir = Vector3.zero;
    public Vector2 movementInput;
    public float horizontalInput;
    public bool jump;
    public bool _canJump;
    #endregion

    #region Dash Paramiter
    [Header("[ Dash ]")]
    public bool dash;
    public bool _Candash;
    bool _dashing;
    public float dashSpeed;
    public float dashTime;
    public float lastMove;
    public float dashCDR;
    public float airTime;
    bool isAirDash;
    float dashCDRcurrent;
    bool canDash;
    public ParticleSystem dashParti;
    #endregion

    #region Attack Paramiter
    [Header("[ Attack ]")]
    public bool attack;
    public float attackCdr;
    bool _canAttack;
    public bool onAttack;
    [SerializeField] private bool isAttacking = false;
    [SerializeField] private float lastAttackTime = 0f;
    int currentNormalAtt = 0;
    [SerializeField] private Collider swordCollider;
    public List<GameObject> slash = new List<GameObject>();

    float han4;
    float han2;
    #endregion

    #region Skill
    [Header("[ Skill ]")]
    public bool skill;
    public bool skilling;
    public bool _canSkill;
    float skillTimer;
    public float skillCdr;
    public float skillPlaytime;
    public Vector3 camSkillOffset;
    public float camLerp;
    #endregion

    #region Block and Parry
    [Header("[ Block ]")]
    public bool block;
    public bool isBlocking;
    public bool _canBlock;
    public float blockCdr;
    public float blockLongTime;
    public GameObject blockPat;
    [SerializeField] float startBlock;
    float blockTimer;
    [Header("[ Parry ]")]
    public bool parry;
    public bool isParring;
    public float parryTime;
    public GameObject parryPat;
    #endregion

    #region QuickHeal Paramiter
    [Header("[ Heal ]")]
    public bool heal;
    bool _canHeal;
    #endregion

    #region Stun
    [Header("[ Take Damage ]")]
    bool stun;
    public float stunTimer;
    public float immuTime;
    public Collider playerHitBox;
    public StatusPlayerManager _statusPlayerManager;
    bool isDie;
    #endregion

    #region Anim and Sound
    [Header("[ Animation ]")]
    public Animator anim;

    [Header("[ Audio ]")]
    public AudioSource walk_audioSource;

    [Header("[ Debug ]")]
    public bool debugGround;
    #endregion

    #region Stamina
    [Header("[ Stamina ]")]
    public bool useStamina = true;
    public float staminaCurrent;
    [SerializeField] private float staminaMax;
    [SerializeField] private float timeBeforeStaminaRegenStarts;
    [SerializeField] private float staminaTimeIncrement = 1f; //Wait For Next Regen
    [SerializeField] private float staminaValueIncrement = 25; // regen Stamina Rate
    private Coroutine regenStamina;
    public static Action<float> OnStaminaChange;

    public float dashStaminaUse;
    public float attackStaminaUse;
    #endregion

    #region Heal
    [Header("Heal")]
    public GameObject healPat;
    public Material healMat;
    public float intensityColor;
    public float healCdr;
    float healTimer;
    #endregion

    void OnEnable()
    {
        // Enable the Input Actions
        if (chaCon == null)
        {
            p_Input = new PlayerInput();

            if (!onAttack)
            {
                p_Input.PlayerS_Movement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
                p_Input.PlayerS_Movement.Jump.performed += i => jump = i.ReadValue<float>() > 0.5f;//ถ้าโดดบัคลบนี่กับjump
            }
            p_Input.PlayerS_Movement.Dash.performed += i => dash = i.ReadValue<float>() > 0.5f;
            p_Input.PlayerS_Movement.Attack.performed += i => attack = i.ReadValue<float>() > 0.5f;
            p_Input.PlayerS_Movement.QuickHeal.performed += i => heal = i.ReadValue<float>() > 0.5f;
            p_Input.PlayerS_Movement.Skill.performed += i => skill = i.ReadValue<float>() > 0.5f;
            p_Input.PlayerS_Movement.Pairry.performed += i => block = i.ReadValue<float>() > 0.5f;



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
        isAirDash = true;
        _canAttack = true;
        _Candash = true;
        _canHeal = true;
        _canBlock = true;
        _canJump = true;
        _canSkill = true;


        currentNormalAtt = 0;
        swordCollider.enabled = false;
        walk_audioSource.enabled = false;

        skillTimer = 0;

        han4 = attackCdr / 4;
        han2 = attackCdr / 2;

        staminaCurrent = staminaMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDie)
            return;

        if (GameManager.Instance.gameEnd && GameManager.Instance.playerHP <= 0)
        {
            Die();
            return;
        }

        if (useStamina)
            HandleStamina();

        HandleBlock();
        HandleSkill();

        ExtraTime();
        HandleQuickHeal();

        debugGround = chaCon.isGrounded;

        HandleMovementInput();
        moveDir.x = +horizontalInput * speed;

        HandleDash();
        HandleAttack();

        if (chaCon.isGrounded)
        {
            //กันบัค
            /*if (Input.GetButton("Jump"))
            {
                moveDir.y = jumpSpeed;
            }*/

            if (jump && _canJump)
            {
                HandleJumpInput();
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

        moveDir.z = 0;

        if (_dashing)
            return;

        chaCon.Move(moveDir * Time.deltaTime);

    }

    #region Movement Method
    private void HandleMovementInput()
    {
        horizontalInput = movementInput.x;

        if (!canMove)
            horizontalInput = 0;

        if (movementInput.x != 0)
            lastMove = movementInput.x;



        if (horizontalInput > 0 || horizontalInput < 0)
        {
            timer = 0f;
            anim.SetFloat("Horizontal", 1, 0.1f, Time.deltaTime);
        }
        else
        {
            // เพิ่มเวลา idle
            timer += Time.deltaTime;

            // ตรวจสอบว่า idle นานเกิน 6 วินาที
            if (timer >= idleAnimTimer)
            {
                anim.SetFloat("Horizontal", -1, 0.1f, Time.deltaTime);
            }
            else
            {
                anim.SetFloat("Horizontal", 0, 0.1f, Time.deltaTime);
            }
        }

        if ((chaCon.isGrounded && horizontalInput > 0 && !_dashing) || (chaCon.isGrounded && horizontalInput < 0 && !_dashing))
        {
            walk_audioSource.enabled = true;
        }
        else
        {
            walk_audioSource.enabled = false;
        }

    }

    private void HandleJumpInput()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.s_Jump);
        anim.SetBool("Jump", true);
        moveDir.y = jumpSpeed;
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
        if (!_Candash)
            return;

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
        dashParti.Play();
        anim.SetTrigger("Dash");
        AudioManager.instance.PlaySFX(AudioManager.instance.s_Dash);
        StaminaUse(dashStaminaUse);
        float startTime = Time.time;
        while (Time.time < startTime + dashTime)
        {
            moveDir.x = +lastMove * dashSpeed;
            if (!chaCon.isGrounded)
            {
                moveDir.y = 0;
            }

            moveDir.z = 0;
            chaCon.Move(moveDir * Time.deltaTime);
            anim.SetBool("Dashing", true);
            _dashing = true;
            _canAttack = false;
            _canBlock = false;
            _canSkill = false;
            _canJump = false;
            playerHitBox.enabled = false;
            swordCollider.enabled = false;
            canMove = false;
            isAirDash = true;

            yield return null;
        }
        yield return new WaitForSeconds(airTime);
        moveDir.y -= gravity * Time.deltaTime;
        isAirDash = false;

        anim.SetBool("Dashing", false);
        dashParti.Stop();

        _dashing = false;
        canMove = true;
        _canAttack = true;
        _canBlock = true;
        _canSkill = true;
        _canJump = true;
        playerHitBox.enabled = true;
    }

    #endregion

    #region Action Method

    private void HandleStamina()
    {
        //
        if (staminaCurrent < dashStaminaUse)
            canDash = false;
        else
            canDash = true;
        //
        if (staminaCurrent < attackStaminaUse)
            _canAttack = false;
        else
            _canAttack = true;
        //

        if (staminaCurrent < staminaMax && regenStamina == null)
        {
            regenStamina = StartCoroutine(RegenStamina());
        }
    }

    private void StaminaUse(float totolUse)
    {
        if (regenStamina != null)
        {
            StopCoroutine(regenStamina);
            regenStamina = null;
        }

        staminaCurrent -= totolUse;

        if (staminaCurrent < 0)
            staminaCurrent = 0;

        OnStaminaChange?.Invoke(staminaCurrent);

        if (staminaCurrent <= 0)
        {
            canDash = false;
            _canAttack = false;
        }
    }

    private IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(timeBeforeStaminaRegenStarts);
        WaitForSeconds timeToWait = new WaitForSeconds(staminaTimeIncrement);

        while (staminaCurrent < staminaMax)
        {
            if (staminaCurrent > 0)
            {
                canDash = true;
                _canAttack = true;
            }


            staminaCurrent += staminaValueIncrement;

            if (staminaCurrent > staminaMax)
                staminaCurrent = staminaMax;

            OnStaminaChange?.Invoke(staminaCurrent);

            yield return timeToWait;
        }
        regenStamina = null;
    }

    private void HandleAttack()
    {
        if (_dashing)
        {
            anim.SetBool("Dashing", true);
            return;
        }

        if (attack && _canAttack)
        {
            canMove = false;
            _canSkill = false;
            Attack();
            attack = false;
        }
    }

    public void Attack()
    {

        if (!isAttacking && Time.time - lastAttackTime > 1f)
        {
            currentNormalAtt = 0;
        }
        lastAttackTime = Time.time;

        if (isAttacking)
            return;

        StartCoroutine(AttackAnimCycle(currentNormalAtt));
        currentNormalAtt++;

        if (currentNormalAtt >= 3)
        {
            currentNormalAtt = 0;
        }

        timer = 0f;
    }

    IEnumerator AttackAnimCycle(int cycle)
    {
        Debug.Log("Attack");
        isAttacking = true;
        StaminaUse(attackStaminaUse);
        // เริ่มอนิเมชั่น 1
        AudioManager.instance.PlaySFX(AudioManager.instance.s_Slash[cycle]);
        anim.SetInteger("AttackRoutine", cycle);
        anim.SetTrigger("Attack");
        
        yield return new WaitForSeconds(han4);
        slash[cycle].SetActive(true);
        swordCollider.enabled = true;
        yield return new WaitForSeconds(han4);
        slash[cycle].SetActive(false);
        swordCollider.enabled = false;
        yield return new WaitForSeconds(han2);
        isAttacking = false;
        canMove = true;
        _canSkill = true;
    }

    public void HandleSkill()
    {
        if (!_canSkill) return;

        skillTimer += Time.deltaTime;

        if (skill && skillTimer >= skillCdr && !skilling && _canSkill)
        {
            skill = false;
            StartCoroutine(SkillAction());
        }
    }

    IEnumerator SkillAction()
    {
        skilling = true;
        anim.SetTrigger("Skill");
        //Cam
        camCtrl.xOffset = camSkillOffset.x;
        camCtrl.yOffset = camSkillOffset.y;
        camCtrl.zoomOffset = camSkillOffset.z;
        camCtrl.speedLerp = camLerp;
        //
        playerHitBox.enabled = false;
        swordCollider.enabled = true;
        canMove = false;
        _Candash = false;
        _canAttack = false;
        _canBlock = false;
        _canJump = false;
        immuTime = 100f;

        yield return new WaitForSeconds(0.1f);
        swordCollider.enabled = false;
        yield return new WaitForSeconds(skillPlaytime - 0.1f);

        camCtrl.BackToDefault();
        skillTimer = 0f;
        playerHitBox.enabled = true;
        canMove = true;
        _Candash = true;
        _canAttack = true;
        skilling = false;
        _canBlock = true;
        _canJump = true;
        immuTime = 0f;

    }

    public void TakeDamgae(int damage)
    {
        if (isBlocking)
        {
            parry = true;
            HandleParry();
            return;
        }

        if (immuTime < 0)
        {
            immuTime = stunTimer;
            _statusPlayerManager.TakeDamage(damage);
            StartCoroutine(StunTimer());
        }
    }

    IEnumerator StunTimer()
    {
        if (skilling)
            yield break;

        stun = true;
        _Candash = false;
        _canAttack = false;
        _canSkill = false;
        _canBlock = false;
        _canJump = false;
        playerHitBox.enabled = false;
        swordCollider.enabled = false;
        canMove = false;
        anim.SetBool("stuning", stun);
        anim.SetTrigger("stun");
        yield return new WaitForSeconds(stunTimer);
        _Candash = true;
        _canAttack = true;
        _canSkill = true;
        _canBlock = true;
        _canJump = true;
        playerHitBox.enabled = true;
        canMove = true;
        stun = false;
        anim.SetBool("stuning", stun);

    }

    public void HandleBlock()
    {
        if (!_canBlock)
            return;

        blockTimer += Time.deltaTime;

        if (block && blockTimer >= blockCdr && !isParring && !isBlocking)
        {
            StartCoroutine(OnBlocking());
        }
    }
    public void HandleParry()
    {
        if (parry && isBlocking && !isParring)
        {
            parry = false;
            StartCoroutine(OnParrying());
        }
    }

    IEnumerator OnParrying()
    {
        isParring = true;
        isBlocking = false;
        anim.SetBool("Blocking", isBlocking);
        immuTime = 100;
        anim.SetTrigger("Parry");
        AudioManager.instance.PlaySFX(AudioManager.instance.s_Parry);

        yield return new WaitForSeconds(parryTime / 2);
        parryPat.SetActive(true);
        swordCollider.enabled = true;
        yield return new WaitForSeconds(parryTime / 2);
        immuTime = 0;
        swordCollider.enabled = false;
        parryPat.SetActive(false);
        blockTimer = 0f;
        canDash = true;
        canMove = true;
        _canAttack = true;
        _canSkill = true;
        isParring = false;
        _canJump = true;

        block = false;

    }

    IEnumerator OnBlocking()
    {
        isBlocking = true;
        canDash = false;
        canMove = false;
        _canAttack = false;
        _canSkill = false;
        _canJump = false;
        anim.SetTrigger("Block");
        anim.SetBool("Blocking", isBlocking);
        blockPat.SetActive(true);
        AudioManager.instance.PlaySFX(AudioManager.instance.s_Block);
        bool bolckBreak = false;
        startBlock = Time.time;
        while (Time.time < startBlock + blockLongTime)
        {
            if (isParring)
            {
                bolckBreak = true;
                
                break;
            }
            yield return null;
        }

        if (bolckBreak)
        {
            blockPat.SetActive(false);
            yield break;
        }
            


        blockTimer = 0f;
        isBlocking = false;
        anim.SetBool("Blocking", isBlocking);
        blockPat.SetActive(false);
        canDash = true;
        canMove = true;
        _canAttack = true;
        _canSkill = true;
        _canJump = true;

        block = false;



    }

    public void Die()
    {
        anim.SetTrigger("Die");

        canDash = false;
        _canAttack = false;
        playerHitBox.enabled = false;
        swordCollider.enabled = false;
        canMove = false;
        _canJump = false;

        isDie = true;
    }
    #endregion

    #region Status Player Method
    private void HandleQuickHeal()
    {
        if (!_canHeal) return;

        skillTimer += Time.deltaTime;
        if (heal && skillTimer >= skillCdr && _canHeal)
        {
            QuickHeal();
            heal = false;
        }
    }

    private void QuickHeal()
    {
        Debug.Log("Heal!!");
        InventoryScreen.instance.QuickHeal();
        StartCoroutine(HealShad());

    }

    public void TestHealEffect()
    {
        StartCoroutine(HealShad());
    }
    IEnumerator HealShad()
    {
        skillTimer = 0;
        healMat.SetColor("_EmissionColor", healMat.color * intensityColor);
        healMat.SetFloat("_Power", 1);
        healPat.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        healMat.SetColor("_EmissionColor", healMat.color*1);
        healMat.SetFloat("_Power", 100);

        yield return new WaitForSeconds(1.5f);
        healPat.SetActive(false);
    }

    public void ExtraTime()
    {
        immuTime -= Time.deltaTime;
        
    }
    #endregion
}
