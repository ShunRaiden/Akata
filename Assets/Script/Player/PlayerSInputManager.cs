using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    PlayerSInput p_Input;

    void OnEnable()
    {
        // Enable the Input Actions
        p_Input = new PlayerSInput();
        p_Input.Enable();
    }

    void OnDisable()
    {
        // Disable the Input Actions
        p_Input.Disable();
    }
}
