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
        obi = GetComponent<ObiSoftbody>();
    }


    public void Move(PlayerDirection direction, Player water, RotateDirection rotationDirection)
    {
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
        }
    }

    public void Shift(PlayerDirection direction, Player water, float time)
    {
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
        Debug.Log("WaterJump");
        obi.AddForce(new Vector3(0, jumpAcceleration, 0), ForceMode.Impulse);
    }
}