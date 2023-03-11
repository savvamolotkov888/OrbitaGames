using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class IceDouble : MonoBehaviour
{
    public static float angle;
    public Transform Target1;
    public Transform Target2;
    [SerializeField] private Transform up;

    [SerializeField] private Transform IceDoubleDirection;
    [SerializeField] private Transform player;
    [SerializeField] private Transform playerDoubleTarget;
    [SerializeField] private Transform playerDoubleDirection;
    [SerializeField] private float DebugLength;

    [SerializeField] private Transform X;
    [SerializeField] private Transform Xm;
    [SerializeField] private Transform Y;
    [SerializeField] private Transform Ym;
    [SerializeField] private Transform Z;
    [SerializeField] private Transform Zm;


    [SerializeField] private LayerMask DebugLayerMask;

    private RaycastHit _X;
    private RaycastHit _Xm;
    private RaycastHit _Y;
    private RaycastHit _Ym;
    private RaycastHit _Z;
    private RaycastHit _Zm;

    private Ray __X;
    private Ray __Xm;
    private Ray __Y;
    private Ray __Ym;
    private Ray __Z;
    private Ray __Zm;

    [SerializeField] private TMP_Text CurrentIceAxisText;
    private CurrentIceAxis _currentIceAxis;

    private CurrentIceAxis _CurrentIceAxis
    {
        get => _currentIceAxis;
        set
        {
            _currentIceAxis = value;
            CurrentIceAxisText.text = value.ToString();

            if (_currentIceAxis == CurrentIceAxis.X)
            {
                IceDoubleDirection.position = Y.position;
                up.position = Xm.position;
            }
            else if (_currentIceAxis == CurrentIceAxis.Xm)
            {
                IceDoubleDirection.position = Ym.position;
                up.position = X.position;
            }

            else if (_currentIceAxis == CurrentIceAxis.Y)
            {
                IceDoubleDirection.position = Xm.position;
                up.position = Ym.position;
            }
            else if (_currentIceAxis == CurrentIceAxis.Ym)
            {
                IceDoubleDirection.position = Z.position;
                up.position = Y.position;
            }
            else if (_currentIceAxis == CurrentIceAxis.Z)
            {
                IceDoubleDirection.position = Y.position;
                up.position = Zm.position;
            }
            else if (_currentIceAxis == CurrentIceAxis.Zm)
            {
                IceDoubleDirection.position = Ym.position;
                up.position = Z.position;
            }
        }
    }

    private Quaternion quaternion;


    void FixedUpdate()
    {
        transform.rotation = player.rotation;

        RayUpdate();
        RaycastHitCheck();

        Debug.DrawRay(transform.position, X.position - transform.position, Color.white);
        Debug.DrawRay(transform.position, Xm.position - transform.position, Color.red);
        Debug.DrawRay(transform.position, Y.position - transform.position, Color.green);
        Debug.DrawRay(transform.position, Ym.position - transform.position, Color.green);
        Debug.DrawRay(transform.position, Z.position - transform.position, Color.blue);
        Debug.DrawRay(transform.position, Zm.position - transform.position, Color.blue);

        //
        // var angle = Vector3.SignedAngle(transform.position + playerDoubleDirection.position,
        //     transform.position +playerDoubleTarget.position,
        //     Vector3.up);

        Debug.DrawLine(transform.position, playerDoubleDirection.position, Color.white);
        Debug.DrawLine(transform.position, playerDoubleTarget.position, Color.yellow);

        angle = (-Vector3.SignedAngle(playerDoubleTarget.position - transform.position,
            playerDoubleDirection.position - transform.position, up.position -transform.position));

Debug.LogError(angle);
        // angle = -Vector3.SignedAngle(Target.position - Ice.transform.position, IceDirection.transform.forward,
        //     Vector3.up);
    }

    private void RayUpdate()
    {
        __X = new Ray(transform.position, X.position - transform.position);
        __Xm = new Ray(transform.position, Xm.position - transform.position);
        __Y = new Ray(transform.position, Y.position - transform.position);
        __Ym = new Ray(transform.position, Ym.position - transform.position);
        __Z = new Ray(transform.position, Z.position - transform.position);
        __Zm = new Ray(transform.position, Zm.position - transform.position);
    }

    private void RaycastHitCheck()
    {
        if (Physics.Raycast(__X, out RaycastHit XInfo, 1, DebugLayerMask))
            _CurrentIceAxis = CurrentIceAxis.X;
        else if (Physics.Raycast(__Xm, out RaycastHit XmInfo, 1, DebugLayerMask))
            _CurrentIceAxis = CurrentIceAxis.Xm;
        else if (Physics.Raycast(__Y, out RaycastHit YInfo, 1, DebugLayerMask))
            _CurrentIceAxis = CurrentIceAxis.Y;
        else if (Physics.Raycast(__Ym, out RaycastHit YmInfo, 1, DebugLayerMask))
            _CurrentIceAxis = CurrentIceAxis.Ym;
        else if (Physics.Raycast(__Z, out RaycastHit ZInfo, 1, DebugLayerMask))
            _CurrentIceAxis = CurrentIceAxis.Z;
        else if (Physics.Raycast(__Zm, out RaycastHit ZmInfo, 1, DebugLayerMask))
            _CurrentIceAxis = CurrentIceAxis.Zm;
    }

    private enum CurrentIceAxis
    {
        X,
        Xm,
        Y,
        Ym,
        Z,
        Zm
    }
}