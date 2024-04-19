using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCam : MonoBehaviour
{
    Animator anim;
    bool fpCheck = false;
    public GameObject mainMenu;
    public float lerpTime;

    private void Start()
    {
        anim = GetComponent<Animator>();
        mainMenu.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (fpCheck)
            return;

        if (Input.anyKeyDown)
        {           
            fpCheck = true;
            anim.SetTrigger("Start");
            StartCoroutine(WaitLerp());
        }
    }

    IEnumerator WaitLerp()
    {
        yield return new WaitForSeconds(lerpTime);
        mainMenu.SetActive(true);
    }
}
