using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class Water : Player, IMove, IJump, IShift , IDied
{
    public override event Action<float> TakeDamageEvent;

    [SerializeField] private float moveAcceleration;
    [SerializeField] private float jumpAcceleration;
    [SerializeField] private ObiCollisionMaterial stickinessMaterial;
    private ObiCollisionMaterial defaultMaterial;

    public Transform referenceFrame;

    private bool isStickiness;


    private ObiSoftbody water;
    private ObiSolver waterSolver;

    private void Start()
    {
        defaultMaterial = GetComponent<ObiSoftbody>().collisionMaterial;
        water = GetComponent<ObiSoftbody>();
        waterSolver = GetComponentInParent<ObiSolver>();
        waterSolver.OnCollision += WaterSolver_OnCollision;
    }


    public void Move(PlayerDirection direction, Player water, RotateDirection rotationDirection)
    {
        var forceDirection = Vector3.zero;

        if (direction.Forward != 0)
        {
            forceDirection += referenceFrame.forward * moveAcceleration;
            this.water.AddForce(
                forceDirection.normalized * moveAcceleration * direction.Forward * direction.AirControll,
                ForceMode.Acceleration);
        }

        if (direction.Lateral != 0)
        {
            forceDirection += referenceFrame.right * moveAcceleration;
            this.water.AddForce(
                forceDirection.normalized * moveAcceleration * direction.Lateral * direction.AirControll,
                ForceMode.Acceleration);
        }
    }

    public void Shift(PlayerDirection direction, Player water, float time)
    {
        if (!isStickiness)
        {
            this.water.collisionMaterial = stickinessMaterial;
            isStickiness = true;
        }
        else
        {
            this.water.collisionMaterial = defaultMaterial;
            isStickiness = false;
        }
    }


    public void Jump(PlayerDirection direction, Player water)
    {
        Debug.LogError("WaterJump");
        this.water.AddForce(new Vector3(0, jumpAcceleration, 0), ForceMode.Impulse);
    }

    public override void TakeDamage(float waterDamageValue)
    {
        Debug.LogError("W" + waterDamageValue);
    }

    void WaterSolver_OnCollision(object sender, Obi.ObiSolver.ObiCollisionEventArgs e)
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
                    {
                        TakeDamage(enemy.waterDamage);
                        TakeDamageEvent?.Invoke(enemy.waterDamage);
                    }
                }
            }
        }
    }

    public void Died()
    {
        Debug.LogError("WaterDied");
    }
}