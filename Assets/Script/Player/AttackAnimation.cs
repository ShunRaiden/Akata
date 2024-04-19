using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimation : MonoBehaviour
{
    private bool isAttacking = false;
    private float lastAttackTime = 0f;
    int currentNormalAtt = 0;
    [SerializeField] private Animator anim;
    [SerializeField] private Collider swordCollider;
    [SerializeField] private PlayerSMovement playerSMovement;

    private void Start()
    {
        currentNormalAtt = 0;
        swordCollider.enabled = false;
    }

    public void Attack()
    {
        if (!isAttacking && Time.time - lastAttackTime < 1f)
        {
            StartCoroutine(AttackAnimCycle(currentNormalAtt));
            lastAttackTime = Time.time;
            currentNormalAtt++;

            if (currentNormalAtt >= 3)
            {
                currentNormalAtt = 0;
            }

        }
        else if (!isAttacking && Time.time - lastAttackTime > 1f)
        {
            currentNormalAtt = 0;
            StartCoroutine(AttackAnimCycle(currentNormalAtt));
            lastAttackTime = Time.time;
            currentNormalAtt++;
        }
    }

    IEnumerator AttackAnimCycle(int cycle)
    {
        isAttacking = true;
        // เริ่มอนิเมชั่น 1
        anim.SetTrigger("Attack");
        anim.SetInteger("AttackRoutine", cycle);
        yield return new WaitForSeconds(0.1f);
        swordCollider.enabled = true;
        yield return new WaitForSeconds(0.1f);
        swordCollider.enabled = false;
        isAttacking = false;
        playerSMovement.canMove = true;
        yield return null;

    }

}
