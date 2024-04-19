using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public PlayerMovement_LST player;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
                other.GetComponent<BaseEnemy>().GetAttack(player.skilling ,player.isParring);
        }
    }
}
