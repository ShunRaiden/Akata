using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class CameraLock : MonoBehaviour
{
    CameraControllerPlayerS camCon;
    ColliderLockCamOffset camZoomOffset;

    // Start is called before the first frame update
    void Start()
    {
        camCon = GetComponent<CameraControllerPlayerS>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Camlock(Collider other , GameObject colli , bool isStay , GameObject lockCamObj)
    {
        if (other.gameObject.tag == "Player")
        {
            camZoomOffset = colli.GetComponent<ColliderLockCamOffset>();

            if (isStay)
            {
                camCon.isLock = true;
                camCon.newTarget = lockCamObj;
                camCon.speedLerp = camZoomOffset.followSpeed;

            }
            else
            {
                camCon.zoomOffset = camZoomOffset.zoomOffset;
                camCon.xOffset = camZoomOffset.xOffeset;
                camCon.yOffset = camZoomOffset.yOffeset;
                camCon.speedLerp = camZoomOffset.followSpeed;
            }
            
        }
    }
    public void CamUnlock(Collider other , bool isStay)
    {
        if (other.gameObject.tag == "Player")
        {
            if (isStay)
            {
                camCon.isLock = false;
            }

            camCon.zoomOffset = camCon.defaultZoom;
            camCon.xOffset = camCon.default_xOffset;
            camCon.yOffset = camCon.default_yOffset;
            camCon.speedLerp = camCon.default_speedLerp;

        }
    }
}
