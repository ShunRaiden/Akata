using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerPlayerS : MonoBehaviour
{
    [SerializeField] GameObject _player;

    [Header("Current")]
    public float xOffset;
    public float yOffset;
    public float speedLerp;
    public float zoomOffset;
    public bool isLock;
    public GameObject newTarget;

    [Header("Default")]
    public float defaultZoom;
    public float default_xOffset = 0;
    public float default_yOffset = 2;
    public float default_speedLerp = 2;

    Vector3 target;

    private void Start()
    {
        zoomOffset = defaultZoom;
        xOffset = default_xOffset;
        yOffset = default_yOffset;
        speedLerp = default_speedLerp;
    }

    // Update is called once per frame
    void Update()
    {
        if (isLock)
        {
            transform.position = Vector3.Lerp(transform.position, newTarget.transform.position , speedLerp * Time.deltaTime);
        }
        else
        {
            target = new Vector3(_player.transform.position.x + xOffset, _player.transform.position.y + yOffset, zoomOffset);
            transform.position = Vector3.Lerp(transform.position, target, speedLerp * Time.deltaTime);
        }
        
    }

    public void BackToDefault()
    {
        zoomOffset = defaultZoom;
        xOffset = default_xOffset;
        yOffset = default_yOffset;
        speedLerp = default_speedLerp;
    }
}
