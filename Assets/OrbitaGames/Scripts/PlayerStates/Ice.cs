using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ice : Player, IMove, IJump, IShift, IDoubleShift, IDied
{
    [SerializeField] private float MoveAcceleration;
    [SerializeField] private float RotationAcceleration;
    [SerializeField] private float JumpAcceleration;
    [SerializeField] private float ShiftAcceleration;
    [SerializeField] private float ShiftImpulseAcceleration;
    private float shiftAcceleration = 1f;

    public override float CurrentHealthHP { get; set; } = 100;
    public override float MaxHealthHP => 100;
    public override float CurrentBoostHP { get; set; } = 10;
    public override float MaxBoostHP => 10;
    public override event Action<float> TakeDamageEvent;
    public override event Action<float> TakeHealthEvent;
    public override event Action<float> LoseBoostEvent;


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
        if ((direction.Forward == 1 || direction.Lateral == 0) && direction.Shift > 0 && CurrentBoostHP > 0)
        {
            shiftAcceleration = ShiftAcceleration;
            CurrentBoostHP--;
            Debug.LogError(CurrentBoostHP);
            LoseBoostEvent?.Invoke(5);
        }

        else if (direction.Shift <= 0)
            shiftAcceleration = 1;
    }

    public void DoubleShift(PlayerDirection direction, Player ice, float IceAcelerationTime)
    {
        if (direction.Forward != 0 && direction.Lateral == 0)
        {
            iceRigidbody.AddRelativeForce(0, 0,
                direction.Forward * ShiftImpulseAcceleration, ForceMode.Impulse);
            LoseBoostEvent?.Invoke(5);
        }
    }

    public override void TakeDamage(float iceDamageValue)
    {
        Debug.LogError("Ice TakeDamage");
    }

    public override void TakeHealth(float iceHealthValue)
    {
        Debug.LogError("Ice TakeHealth");
    }

    private void OnCollisionEnter(Collision player)
    {
        if (player.gameObject.TryGetComponent(out DamagePlatfom enemy))
        {
            TakeDamage(enemy.iceDamage);
            TakeDamageEvent?.Invoke(enemy.iceDamage);
        }

        else if (player.gameObject.TryGetComponent(out HealthPlatform healthPlatform))
        {
            TakeHealth(healthPlatform.waterHealth);
            TakeHealthEvent?.Invoke(healthPlatform.waterHealth);
        }
    }

    public void Died()
    {
        Debug.LogError("IceDied");
    }
}