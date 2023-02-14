using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;
using UnityEngine.EventSystems;

public class Water : Player, IMove, IJump
{
    [SerializeField] private float MoveAcceleration;
    [SerializeField] private float JumpAcceleration;
    [Range(0, 1)] public float airControl = 0.3f;
    public Transform referenceFrame;


    private ObiSoftbody obi;

    public void Move(PlayerDirection direction, Player water, RotateDirection rotationDirection)
    {
        if (!obi)
            obi = water.GetComponent<ObiSoftbody>();

//        Debug.Log("WaterMoove");
        var forceDirection = Vector3.zero;

        if (direction.Forward != 0)
        {
            forceDirection += referenceFrame.forward * MoveAcceleration;
            obi.AddForce(forceDirection.normalized * MoveAcceleration * direction.Forward, ForceMode.Acceleration);
           // Debug.Log(TargetB.position + "   " + TargetB.position.normalized);
        }
        if (direction.Lateral != 0)
        {
            forceDirection += referenceFrame.right * MoveAcceleration;
            obi.AddForce(forceDirection.normalized * MoveAcceleration * direction.Lateral, ForceMode.Acceleration);
            // Debug.Log(TargetB.position + "   " + TargetB.position.normalized);
        }

        //   if (direction.Lateral != 0)
        //    obi.AddForce(new Vector3(TargetB.position.x ,0,0)*MoveAcceleration, ForceMode.Force);
    }

    public void Jump(PlayerDirection direction,Player water)
    {
        if (!obi)
            obi = water.GetComponent<ObiSoftbody>();
        Debug.Log("WaterJump");
        obi.AddForce(new Vector3(0, JumpAcceleration, 0), ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.LogError("s");
    }
}