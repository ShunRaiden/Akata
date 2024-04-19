using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox_Movement : BaseEnemy
{
    private void Update()
    {
        if (die)
            return;

        if (isStunned)
        {
            // Enemy is stunned, decrement the stun timer
            stunTimer -= Time.deltaTime;
            canAttack = false;

            if (stunTimer <= 0f)
            {
                // Stun duration is over, reset the stun state
                Debug.Log("stun done");
                isStunned = false;
                anim.SetBool("stun", isStunned);
                canAttack = true;
                isResetAtt = true;
            }
            return;
        }

        // Enemy is not stunned
        // Check if the player is within the detection range
        if (Vector3.Distance(transform.position, player.position) < detectionRange || isCombat)
        {
            DetectedPlayer();
        }

        if (!isResetAtt)
        {
            CooldownAttack();
        }

    }

    void DetectedPlayer()
    {
        if (die) return;

        isCombat = true;

        RotateTowardsPlayer();

        if (isAttacking || !isResetAtt)
            return;

        anim.SetBool("isCombat", true);
        // Check if the player is within attack range
        
        if (Vector3.Distance(transform.position, player.position) < walkforAttRange && !isAttacking && isResetAtt)
        {
            if (!canAttack)
            {
                return;
            }

            StartCoroutine(ChargerForAttack());
        }
        else
        {
            anim.SetTrigger("Run");
            // Move towards the player when not attacking
            MoveTowardsPlayer();
        }

        //if (Vector3.Distance(transform.position, player.position) > detectionRange*2)
        //isCombat = false;

    }

    void MoveTowardsPlayer()
    {
        if(die) return;

        // Calculate the direction to the player on the X and Z axes only
        Vector3 direction = new Vector3(player.position.x - transform.position.x, 0f, player.position.z - transform.position.z).normalized;

        // Apply gravity to maintain falling speed
        rb.velocity += Physics.gravity * Time.deltaTime * fallSpeed;

        // Limit maximum speed
        Vector3 targetVelocity = direction * speed;
        targetVelocity.y = rb.velocity.y; // Maintain current vertical velocity
        rb.velocity = Vector3.ClampMagnitude(targetVelocity, speed);
    }

    void RotateTowardsPlayer()
    {
        if(die) return;
        // Rotate to face the player
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
    }

    public void TakeDamage()
    {
        if (die)
            return;

        LevelData levelData = GameManager.Instance._playerStatus.levelData;
        float getDamage = levelData.levelStat[levelData.currentLevel].atk;
        health -= levelData.levelStat[levelData.currentLevel].atk;
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

    IEnumerator ChargerForAttack()
    {
        if(skillByPlayer)
            yield break;

        if (!isAttacking)
        {
            anim.SetTrigger("Chager");
        }
        isAttacking = true;

        float startCharge = Time.time;
        while (Time.time < startCharge + attackCharger)
        {
            yield return null;
        }

        isResetAtt = false;

        if (!isStunned && canAttack)
        {
            anim.SetTrigger("attack");
        }        
        yield return new WaitForSeconds(attackDamageTimer);
        if (Vector3.Distance(transform.position, player.position) < attackRange && !isStunned && canAttack)
        {
            //isAttacking = true;
            player.GetComponent<PlayerMovement_LST>().TakeDamgae(damge);
            Debug.Log("Att");
            yield return null;
        }
        isAttacking = false;
    }
}
