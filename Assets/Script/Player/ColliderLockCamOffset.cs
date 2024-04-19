using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderLockCamOffset : MonoBehaviour
{
    CameraLock cameraLock;

    public float zoomOffset;
    public float xOffeset;
    public float yOffeset = 2;
    public float followSpeed = 2;
    public bool isStay;
    public GameObject lockCamObj;

    private void Start()
    {
        cameraLock = FindAnyObjectByType<CameraLock>();
    }

    public void OnTriggerEnter(Collider other)
    {
        cameraLock.Camlock(other , gameObject , isStay, lockCamObj);
    }

    public void OnTriggerExit(Collider other)
    {
        cameraLock.CamUnlock(other , isStay);
    }
}
