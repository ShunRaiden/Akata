using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTwo : MonoBehaviour
{
    [Header("MoveMent")]
    public float speed = 3f;
    public float detectionRange = 5f;
    [SerializeField] private Animator anim;
    private Transform player;
    Rigidbody rb;

    [Header("Attack")]
    public float attackRange = 1.5f;
    private bool isAttacking;
    private float attackCooldownTimer;
    public float attackCharger = 0.5f;
    public float attackCooldown = 2f; // Time between consecutive attacks

    [Header("Stun")]
    private float stunTimer = 0f;
    private bool isStunned = false;
    public float stunDuration = 1.5f; // Duration of stun when hit by the player

    [Header("Status")]
    public EnemyStatus enemyStatus;

    [Header("Die")]
    public List<Collider> enemyCollider = new List<Collider>();

    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        isAttacking = false;
        GameManager.Instance.enemyCount++;
    }

    private void Update()
    {
        if (enemyStatus.die)
            return;

        if (isStunned)
        {
            // Enemy is stunned, decrement the stun timer
            stunTimer -= Time.deltaTime;

            if (stunTimer <= 0f)
            {
                // Stun duration is over, reset the stun state
                Debug.Log("stun done");
                isStunned = false;
            }
            return;
        }

        // Enemy is not stunned
        // Check if the player is within the detection range
        if (Vector3.Distance(transform.position, player.position) < detectionRange)
        {
            DetectedPlayer();
        }

        // Cooldown timer for attacks
        if (isAttacking)
        {
            CooldownAttack();
        }
    }

    void DetectedPlayer()
    {
        if (isAttacking)
            return;

        anim.SetBool("isCombat", true);

        

        // Check if the player is within attack range
        if (Vector3.Distance(transform.position, player.position) < attackRange && !isAttacking)
        {
            StartCoroutine(ChargerForAttack());
        }
        else
        {
            // Move towards the player when not attacking
            MoveTowardsPlayer();
        }
        

    }

    void MoveTowardsPlayer()
    {
        // Calculate the direction to the player on the X and Z axes only
        Vector3 direction = new Vector3(player.position.x - transform.position.x, 0f, player.position.z - transform.position.z).normalized;

        // Limit maximum speed
        Vector3 targetVelocity = direction * speed;
        targetVelocity.y = rb.velocity.y; // Maintain current vertical velocity
        rb.velocity = Vector3.ClampMagnitude(targetVelocity, speed);

        // Rotate to face the player
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
    }
    void CooldownAttack()
    {

        attackCooldownTimer -= Time.deltaTime;

        if (attackCooldownTimer <= 0f)
        {
            // Stop attacking and reset the cooldown timer
            anim.SetTrigger("cdrReset");
            isAttacking = false;
            attackCooldownTimer = attackCooldown;
        }
    }
    public void Stun()
    {
        anim.SetTrigger("isStun");
        Debug.Log("Stun");
        // Method to stun the enemy
        isStunned = true;
        stunTimer = stunDuration;
        // Add any additional effects or animations for the stun
    }

    public void Die()
    {
        enemyStatus.die = true;
        anim.SetTrigger("die");

        for (int i = 0; i < enemyCollider.Count; i++)
        {
            enemyCollider[i].enabled = false;
        }

        GameManager.Instance.enemyCount--;
    }

    public void TakeDamage()
    {
        if (enemyStatus.die)
            return;

        LevelData levelData = GameManager.Instance._playerStatus.levelData;

        enemyStatus.health -= levelData.levelStat[levelData.currentLevel].atk;
        Debug.Log(enemyStatus.health);

        if (enemyStatus.health > 0)
        {
            Debug.Log("Bef Stun");            
            Stun();
        }
        else
        {
            Die();
        }

        AudioManager.instance.PlaySFX(AudioManager.instance.s_EnemyHit);
    }

    IEnumerator ChargerForAttack()
    {
        float startCharge = Time.time;
        while (Time.time < startCharge + attackCharger)
        {
            yield return null;
        }

        if (Vector3.Distance(transform.position, player.position) < attackRange && !isAttacking && !isStunned)
        {
            isAttacking = true;
            player.GetComponent<PlayerMovement_LST>().TakeDamgae(enemyStatus.damge);
            Debug.Log("Att");
            anim.SetTrigger("attack");
            yield return null;
        }
    }
}
