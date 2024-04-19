using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BaseEnemy : MonoBehaviour
{
    [Header("Status")]
    public int health;
    public int damge;
    public bool die;

    [Header("MoveMent")]
    public float speed = 3f;
    public float fallSpeed;
    public float detectionRange = 5f;
    [HideInInspector] public Animator anim;
    [HideInInspector] public Transform player;
    [HideInInspector] public Rigidbody rb;

    [Header("Attack")]
    public float attackRange = 1.5f;
    public float walkforAttRange;
    public bool isAttacking;
    public float attackCooldownTimer;
    public float attackCharger = 0.5f;
    public float attackDamageTimer;
    public float attackCooldown = 2f; // Time between consecutive attacks
    public bool isCombat = false;
    public bool canAttack;
    public bool isResetAtt;
    public LayerMask playerLayer;
    public Transform attackPivot;

    [Header("Paticle")]
    public GameObject boomPat;
    public float timePat;

    [Header("Stun")]
    [HideInInspector] public float stunTimer = 0f;
    [HideInInspector] public bool isStunned = false;
    public float stunDuration = 1.5f; // Duration of stun when hit by the player
    public bool skillByPlayer;
    public float playerSkillTime;

    [Header("Die")]
    public List<Collider> enemyCollider = new List<Collider>();

    [SerializeField] EnemyType type;

    [Header("HUD")]
    public GameObject hp_UI;
    public Vector3 hpCurrentUI;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        isAttacking = false;
        canAttack = true;
        StateManager.instance.enemyCount++;
        hpCurrentUI = new Vector3(1, 1, 1);
        hp_UI.transform.localScale = hpCurrentUI;
    }

    void OnDrawGizmosSelected()
    {
        

        Gizmos.color = Color.red;
        //Vector3 dir = transform.forward * detectionRange;
        //Gizmos.DrawRay(transform.position, dir);

        //Gizmos.DrawRay(attackPivot.position, transform.TransformDirection(Vector3.forward) * attackRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(attackPivot.position, attackRange);
    }

    public void GetAttack(bool isSkillPlayer , bool isPlayerParry)
    {

        if (isSkillPlayer)
        {
            if (die)
                return;
            StartCoroutine(SkillByPlayer());

        }else if (isPlayerParry)
        {
            if (die)
                return;
            Stun();
            SwitchDamage();
        }
        else
        {
            SwitchDamage();
        }

    }

    public void SwitchDamage()
    {
        switch (type)
        {
            case EnemyType.Radish:
                gameObject.GetComponent<Radish_Movement>().TakeDamage();
                break;
            case EnemyType.Fox:
                gameObject.GetComponent<Fox_Movement>().TakeDamage();
                break;
            case EnemyType.Pork:
                break;
        }
        StartCoroutine(BoomPaticle());
    }

    IEnumerator BoomPaticle()
    {
        boomPat.SetActive(true);
        yield return new WaitForSeconds(timePat);
        boomPat.SetActive(false);
    }
    public IEnumerator SkillByPlayer()
    {
        skillByPlayer = true;
        anim.SetTrigger("stunSkill");
        yield return new WaitForSeconds(playerSkillTime);
        skillByPlayer = false;

        StartCoroutine(BoomPaticle());

        LevelData levelData = GameManager.Instance._playerStatus.levelData;
        float getDamage = levelData.levelStat[levelData.currentLevel].skill_atk;
        health -= levelData.levelStat[levelData.currentLevel].skill_atk;
        Debug.Log(health);

        hpCurrentUI.x -= (getDamage / 100);

        if (hpCurrentUI.x < 0)
            hpCurrentUI.x = 0;

        hp_UI.transform.localScale = hpCurrentUI;

        if (health <= 0)
        {
            Die();
        }

        AudioManager.instance.PlaySFX(AudioManager.instance.s_EnemyHit);

    }

    public void CooldownAttack()
    {
        attackCooldownTimer -= Time.deltaTime;

        if (attackCooldownTimer <= 0f)
        {
            // Stop attacking and reset the cooldown timer
            anim.SetTrigger("Idle");
            isResetAtt = true;
            attackCooldownTimer = attackCooldown;
        }
    }

    public void Die()
    {
        die = true;
        canAttack = false;

        anim.SetTrigger("die");
        anim.SetBool("isDie", die);

        for (int i = 0; i < enemyCollider.Count; i++)
        {
            enemyCollider[i].enabled = false;
        }

        StateManager.instance.enemyCount--;
        Destroy(gameObject, 5f);
    }

    public void Stun()
    {       
        Debug.Log("Stun");
        // Method to stun the enemy
        isStunned = true;
        anim.SetBool("stun", isStunned);
        anim.SetTrigger("isStun");
        stunTimer = stunDuration;
        // Add any additional effects or animations for the stun
        if(die)
            anim.SetTrigger("die");

    }

    [System.Serializable]
    enum EnemyType
    {
        Radish,
        Fox,
        Pork,
    }
}
