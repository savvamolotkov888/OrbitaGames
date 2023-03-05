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
    private Transform Target;
    public Transform Camera;
    public Transform _targetPoz;
    [SerializeField] private RotateDirection rotator;
    [SerializeField] private Transform IceY_Rotator;

    public RotateDirection Rorator
    {
        get => rotator;
        set
        {
            rotator = value;
            switch (rotator)
            {
                case RotateDirection.Left:
                    playerController.Direction.Rotation = RotateDirection.Left;
                    break;
                case RotateDirection.Right:
                    playerController.Direction.Rotation = RotateDirection.Right;
                    break;
                case RotateDirection.DontRotate:
                    playerController.Direction.Rotation = RotateDirection.DontRotate;
                    break;
            }
        }
    }


    private float angle;
    public float rayDist;
    public Ice Ice;
    private PlayerController playerController;

    private bool rayCheck = true;

    [SerializeField] private float jumpPressedDellayTime;
    public bool canJump;

    public bool CanJump
    {
        get => canJump;
        set
        {
            canJump = value;

            if (!canJump)
            {
                canJump = false;
                JumpPressedDelay();
            }
        }
    }

    private void Awake()
    {
        CanJump = true;
        playerController = GetComponent<PlayerController>();
        Target = playerController.targetB;
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
                if (hit.distance < 0.5)
                    CanJump = true;

                //else
                //CanJump = false;
            }
        }
    }

    private async Task JumpPressedDelay()
    {
        rayCheck = false;
        await Task.Delay(TimeSpan.FromSeconds(jumpPressedDellayTime));
        rayCheck = true;
    }

    private void IceRotationCheck()
    {
        angle = -Vector3.SignedAngle(Target.position - Ice.transform.position, IceY_Rotator.transform.forward,
            Vector3.up);
        _targetPoz.position = - new Vector3(IceY_Rotator.transform.position.x + Camera.position.x, 0,
            IceY_Rotator.transform.position.z + Camera.position.z);

        //Debug.DrawRay(transform.position, _targetPoz * 2, Color.black);

        //   var angle2 = -Vector3.SignedAngle(_targetPoz - Ice.transform.position, Ice.transform.forward, Vector3.up);

       Debug.DrawRay(Ice.transform.position ,-_targetPoz.position ,Color.black);

        if (angle > 5 && angle < 180)
        {
            Rorator = RotateDirection.Right;
        }
        else if (angle < -5 && angle > -180)
        {
            Rorator = RotateDirection.Left;
        }
        else
        {
            Rorator = RotateDirection.DontRotate;
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