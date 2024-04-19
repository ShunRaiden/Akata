using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockActionManager : MonoBehaviour
{
    public int enemyInAction;
    public GameObject walls;
    public bool isPass;
    public int indexLock;

    private void Start()
    {
        isPass = false;
        walls.SetActive(false);
    }

    public void LockAction()
    {
        enemyInAction--;
        if (enemyInAction <= 0)
        {
            isPass = true;
            walls.SetActive(false);
            StateManager.instance.lockIndex = -1;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!isPass)
            {
                walls.SetActive(true);
                StateManager.instance.lockIndex = indexLock;
            }
        }
    }
}
