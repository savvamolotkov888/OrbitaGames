using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class Water : Player, IMove, IJump, IShift
{
    [SerializeField] private float moveAcceleration;
    [SerializeField] private float jumpAcceleration;
    [SerializeField] private ObiCollisionMaterial stickinessMaterial;
    private ObiCollisionMaterial defaultMaterial;
    [Range(0, 1)] public float airControl = 0.3f;
    public Transform referenceFrame;

    private bool isStickiness;


    private ObiSoftbody obi;

    private void Start()
    {
        defaultMaterial = GetComponent<ObiSoftbody>().collisionMaterial;
    }


    public void Move(PlayerDirection direction, Player water, RotateDirection rotationDirection)
    {
        if (!obi)
            obi = water.GetComponent<ObiSoftbody>();

//        Debug.Log("WaterMoove");
        var forceDirection = Vector3.zero;

        if (direction.Forward != 0)
        {
            forceDirection += referenceFrame.forward * moveAcceleration;
            obi.AddForce(forceDirection.normalized * moveAcceleration * direction.Forward, ForceMode.Acceleration);
            // Debug.Log(TargetB.position + "   " + TargetB.position.normalized);
        }

        if (direction.Lateral != 0)
        {
            forceDirection += referenceFrame.right * moveAcceleration;
            obi.AddForce(forceDirection.normalized * moveAcceleration * direction.Lateral, ForceMode.Acceleration);
            // Debug.Log(TargetB.position + "   " + TargetB.position.normalized);
        }

        //   if (direction.Lateral != 0)
        //    obi.AddForce(new Vector3(TargetB.position.x ,0,0)*MoveAcceleration, ForceMode.Force);
    }

    public void Shift(PlayerDirection direction, Player water, float time)
    {
        if (!obi)
            obi = water.GetComponent<ObiSoftbody>();
        if (!isStickiness)
        {
            obi.collisionMaterial = stickinessMaterial;
            isStickiness = true;
        }
        else
        {
            obi.collisionMaterial = defaultMaterial;
            isStickiness = false;
        }
    }


    public void Jump(PlayerDirection direction, Player water)
    {
        if (!obi)
            obi = water.GetComponent<ObiSoftbody>();
        Debug.Log("WaterJump");
        obi.AddForce(new Vector3(0, jumpAcceleration, 0), ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.LogError("s");
    }
}