using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel_Service : MonoBehaviour
{
    private InputSystem inputSystem;

    private void Awake()
    {
        inputSystem = new InputSystem();
        inputSystem.UI.Pause.performed += context => Debug.LogError("Pause");
    }

    private void OnEnable()
    {
        inputSystem.Enable();
    }

    private void OnDisable()
    {
        inputSystem.Disable();
    }
}