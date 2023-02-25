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
            obi.AddForce(forceDirection.normalized * moveAcceleration * direction.Forward * direction.AirControll, ForceMode.Acceleration);
        }

        if (direction.Lateral != 0)
        {
            forceDirection += referenceFrame.right * moveAcceleration;
            obi.AddForce(forceDirection.normalized * moveAcceleration * direction.Lateral * direction.AirControll, ForceMode.Acceleration);
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
        Debug.LogError("WaterJump");
        obi.AddForce(new Vector3(0, jumpAcceleration, 0), ForceMode.Impulse);
    }
    public override void TakeDamage(float waterDamageValue)
    {
        Debug.LogError("W" + waterDamageValue);
    }
    
    void Solver_OnCollision (object sender, Obi.ObiSolver.ObiCollisionEventArgs e)
    {
        var world = ObiColliderWorld.GetInstance();
        // just iterate over all contacts in the current frame:
        foreach (Oni.Contact contact in e.contacts)
        {
            // if this one is an actual collision:
            if (contact.distance < 0.01)
            {
                ObiColliderBase col = world.colliderHandles[contact.bodyB].owner;
                if (col != null)
                {
                    if (col.gameObject.TryGetComponent(out Enemy enemy))
                        TakeDamage(enemy.iceDamage);
                    // do something with the collider.
                }
            }
        }
    }
}