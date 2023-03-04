using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CursorSettings : MonoBehaviour
{
    private void Start()
    {
#if !UNITY_EDITOR
        Cursor.lockState = CursorLockMode.Locked;
#endif
    }

    public static void ShowCursor(bool show)
    {
        if (show)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
#if !UNITY_EDITOR
        Cursor.lockState = CursorLockMode.Locked;
#endif
        }
    }
}