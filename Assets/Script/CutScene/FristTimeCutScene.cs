using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class FristTimeCutScene : MonoBehaviour
{
    public PlayableDirector director;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManage.instance.LoadTargetScene2("Center");
        }

        if(director.state != PlayState.Playing)
        {
            SceneManage.instance.LoadTargetScene2("Center");
        }
    }
}
