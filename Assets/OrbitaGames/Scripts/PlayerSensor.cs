using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

/// <summary>
/// Датчики (поворот , высота от пола и т.д.)
/// </summary>
public class PlayerSensor : MonoBehaviour
{
    public Transform Target;
    public RotateDirection rorator;
    private float angle;
    public float rayDist;
    public Ice Ice;

    private bool rayCheck = true;

    [SerializeField] private float jumpPressedDellayTime;

    [FormerlySerializedAs("jumpPressed")] public bool canJump;

    public bool CanJump
    {
        get => canJump;

        set
        {
            canJump = value;
            if (canJump)
            {
                canJump = true;
             
            }
            else
                JumpPressedDelay();
                
        }
    }

    void FixedUpdate()
    {
        FloarCheck();
        IceRotationCheck();
    }

    private void FloarCheck()
    {
        if (rayCheck)
        {
            Ray ray = new Ray(transform.position, -Vector3.up);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, rayDist))
            {
//            Debug.Log(hit.distance);

                if (hit.distance < 0.5)
                    CanJump = true;

                else
                    CanJump = false;
            }
        }
    }

    private async Task JumpPressedDelay()
    {
        rayCheck = false;
        Debug.Log(" rayCheck = false");
        await Task.Delay(TimeSpan.FromSeconds(jumpPressedDellayTime));
        rayCheck = true;
        Debug.Log(" rayCheck = true");
    }

    private void IceRotationCheck()
    {
        angle = -Vector3.SignedAngle(Target.position - Ice.transform.position, Ice.transform.forward, Vector3.up);
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

    public void OnTriggerStay(Collider other)
    {
        //  Debug.Log(other.name);
    }
}


public enum RotateDirection
{
    Left = -1,
    Right = 1,
    DontRotate = 0
}