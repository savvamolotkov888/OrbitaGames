using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    [SerializeField] private Transform Target;

    void Update()
    {
        transform.LookAt(Target);
    }
}