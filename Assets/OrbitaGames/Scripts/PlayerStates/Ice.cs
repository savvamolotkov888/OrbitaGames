using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ice : Player, IMove, IJump, IShift, IDoubleShift
{
    [SerializeField] private float MoveAcceleration;
    [SerializeField] private float RotationAcceleration;
    [SerializeField] private float JumpAcceleration;
    [SerializeField] private float ShiftAcceleration;
    [SerializeField] private float ShiftImpulseAcceleration;
    private float shiftAcceleration = 1f;

    private Rigidbody iceRigidbody;

    private void Awake()
    {
        iceRigidbody = GetComponent<Rigidbody>();
    }

    public void Move(PlayerDirection direction, Player ice,
        RotateDirection rotationDirection)
    {
        iceRigidbody.AddRelativeForce(direction.Lateral * MoveAcceleration, 0, 0);

        if (direction.Forward != 1 && direction.Forward != -1)
            return;


        if (rotationDirection != RotateDirection.DontRotate)
            iceRigidbody.AddTorque(0, RotationAcceleration * (float)rotationDirection * shiftAcceleration, 0);

        if (rotationDirection == RotateDirection.DontRotate)
        {
            iceRigidbody.AddRelativeForce(direction.Lateral * MoveAcceleration, 0,
                direction.Forward * MoveAcceleration * shiftAcceleration);
        }
    }

    public void Jump(PlayerDirection direction, Player ice)
    {
        Debug.Log("IceJump");
        iceRigidbody.AddForce(0, JumpAcceleration, 0, ForceMode.Impulse);
    }

    public void Shift(PlayerDirection direction, Player ice, float IceAcelerationTime)
    {
        if ((direction.Forward == 1 || direction.Lateral == 0) && direction.Shift > 0)
            shiftAcceleration = ShiftAcceleration;

        else if (direction.Shift <= 0)
            shiftAcceleration = 1;
    }

    public void DoubleShift(PlayerDirection direction, Player ice, float IceAcelerationTime)
    {
        if (direction.Forward != 0 && direction.Lateral == 0)
        {
            iceRigidbody.AddRelativeForce(0, 0,
                direction.Forward * ShiftImpulseAcceleration, ForceMode.Impulse);
        }
    }
    public override void TakeDamage(float iceDamageValue)
    {
        Debug.LogError("I");
    }

    private void OnCollisionEnter(Collision player)
    {
        if (player.gameObject.TryGetComponent(out Enemy enemy))
            TakeDamage(enemy.iceDamage);
    }
}