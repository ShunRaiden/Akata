using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakeDamge : MonoBehaviour
{
    [SerializeField]EnemyType type;

    public void TakeDamage()
    {
        
        switch(type)
        {
            case EnemyType.aaaa:
                gameObject.GetComponent<EnemyTwo>().TakeDamage();
                break;
            case EnemyType.bbbbb:
                break;
            case EnemyType.ccccc: 
                break;

        }
    }

    [System.Serializable]
    enum EnemyType
    {
        aaaa,
        bbbbb,
        ccccc,
    }
}


