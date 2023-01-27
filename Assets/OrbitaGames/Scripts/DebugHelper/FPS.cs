using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS : MonoBehaviour
{
    public int _FPS;

    private void OnValidate()
    {
        Application.targetFrameRate = _FPS;
    }
}
