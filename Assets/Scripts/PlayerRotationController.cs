using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerRotationController : MonoBehaviour
{
    public Transform Target;

    public RotateDirection rorator;
    private float angle;

    void Update()
    {
        angle = -Vector3.SignedAngle(Target.position - transform.position, transform.forward, Vector3.up);

        if (angle > 5 && angle < 180)
        {
            rorator = RotateDirection.Right;
        }
        else if (angle < -5 && angle > -180)
        {
            rorator = RotateDirection.Left;
        }
        else
        {
            rorator = RotateDirection.DontRotate;
        }
    }
}

public enum RotateDirection
{
    Left = -1,
    Right = 1,
    DontRotate = 0
}