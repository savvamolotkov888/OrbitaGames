using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class Ice : Player, IMove, IJump, IShift, IDoubleShift, IDied
{
    [SerializeField] private float MoveAcceleration;
    [SerializeField] private float RotationAcceleration;
    [SerializeField] private float JumpAcceleration;
    [SerializeField] private float ShiftAcceleration;
    [SerializeField] private float ShiftImpulseAcceleration;
    private float shiftAcceleration = 1f;
    private HUD_Service _HUDService;

    [Inject]
    private void Construct(HUD_Service _HUD_Service)
    {
        _HUDService = _HUD_Service;
    }

    private float currentHealthHP = 100;

    public override float CurrentHealthHP
    {
        get => currentHealthHP;
        set
        {
            if (value < 0)
            {
                currentHealthHP = _HUDService.WaterHealthHP = 0;
                Died();
            }
            else if (value > MaxHealthHP)
            {
                currentHealthHP = _HUDService.WaterHealthHP = MaxHealthHP;
                Debug.LogError("Ice FULL HEALTH");
            }
            else
                currentHealthHP = _HUDService.IceHealthHP = value;
        }
    }

    public override float MaxHealthHP => 100;
    public override float CurrentBoostHP { get; set; } = 10;
    public override float MaxBoostHP => 10;
    public event Action<float> TakeDamageEvent;
    public event Action<float> TakeHealthEvent;
    public event Action<float> LoseBoostEvent;


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
        //  iceRigidbody.AddForce(0, JumpAcceleration, 0, ForceMode.Impulse);
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

    public void TakeDamage(float iceDamageValue)
    {
        Debug.LogError("Ice TakeDamage");
    }

    public void TakeHealth(float iceHealthValue)
    {
        Debug.LogError("Ice TakeHealth");
    }

    private void OnCollisionStay(Collision player)
    {
        if (player.gameObject.TryGetComponent(out DamagePlatfom enemy))
        {
            TakeDamage(enemy.iceDamage);
            CurrentHealthHP -= enemy.iceDamage;
        }

        else if (player.gameObject.TryGetComponent(out HealthPlatform healthPlatform))
        {
            TakeHealth(healthPlatform.waterHealth);
            //  TakeHealthEvent?.Invoke(healthPlatform.waterHealth);
            CurrentHealthHP += healthPlatform.iceHealth;
        }
    }

    public void Died()
    {
        Debug.LogError("IceDied");
    }

    public void BoostHealthRegeniration()
    {
    }
}