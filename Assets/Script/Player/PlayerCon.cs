using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCon : MonoBehaviour
{
    private TestAnimAttack testAnimAttack;

    void Start()
    {
        testAnimAttack = GetComponent<TestAnimAttack>();
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            testAnimAttack.Attack();
        }


        if (Input.GetKeyDown(KeyCode.S))
        {
            testAnimAttack.CastSkill();
        }


        if (Input.GetKeyDown(KeyCode.D))
        {
            testAnimAttack.Dash();
        }
    }
}
