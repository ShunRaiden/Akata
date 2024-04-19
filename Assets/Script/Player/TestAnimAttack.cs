using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnimAttack : MonoBehaviour
{
	private bool isAttacking = false;
	private bool isDashing = false;
	private float dashCooldown = 1f;
	private float lastAttackTime = 0f;
	int currentNormalAtt = 0;
	public Animator anim;
	public Collider swordCollider;

	private void Start()
	{
		currentNormalAtt = 0;
		swordCollider.enabled = false;
	}

	public void Attack()
	{
		if (!isAttacking && Time.time - lastAttackTime < 1f)
		{
			StartCoroutine(AttackAnimation(currentNormalAtt));
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
			StartCoroutine(AttackAnimation(currentNormalAtt));
			lastAttackTime = Time.time;
			currentNormalAtt++;
		}
	}

	public void CastSkill()
	{
		if (!isAttacking && Time.time - lastAttackTime > 2f)
		{
			// เรียกใช้งานอนิเมชั่นสกิล
			StartCoroutine(SkillAnimation());
			lastAttackTime = Time.time;
		}
	}

	public void Dash()
	{
		if (!isDashing && Time.time - lastAttackTime > dashCooldown)
		{
			// เรียกใช้งานอนิเมชั่น Dash
			StartCoroutine(DashAnimation());
			lastAttackTime = Time.time;
		}
	}

	IEnumerator AttackAnimation(int cycle)
	{
		isAttacking = true;

		
		// เริ่มอนิเมชั่น 1
		anim.SetTrigger("Attack");
		anim.SetInteger("NormalAttack", cycle);
		yield return new WaitForSeconds(0.2f);
		swordCollider.enabled = true;
		yield return new WaitForSeconds(0.3f);
		if (cycle == 2)
		{
			yield return new WaitForSeconds(.5f);
		}
		swordCollider.enabled = false;
		isAttacking = false;

	}

	IEnumerator SkillAnimation()
	{
		isAttacking = true;
		// เริ่มอนิเมชั่น 1
		yield return new WaitForSeconds(1f);
		isAttacking = false;
	}

	IEnumerator DashAnimation()
	{
		isDashing = true;
		// เริ่มอนิเมชั่น Dash
		yield return new WaitForSeconds(0.5f);
		// สิ้นสุดการ Dash
		isDashing = false;
	}
}
