using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Air : Player, IMove, IJump , IDied
{
    public float currentHealthHP = 1;

    public override float CurrentHealthHP
    {
        get =>currentHealthHP;
        set
        {
            currentHealthHP = value;
            if (currentHealthHP == 0)
                currentHealthHP = 10;
        }
    } 
    public override float MaxHealthHP  => 1;
    
    public override float CurrentBoostHP { get; set; } = 10;
    public override float MaxBoostHP  => 10;
    public override event Action<float> TakeDamageEvent;
    public override event Action<float> TakeHealthEvent;
    public override event Action<float> LoseBoostEvent;
    
    [SerializeField] private float MoveAcceleration;
    [SerializeField] private float FlyAcceleration;
    [SerializeField] private float FlyAccelerationWhenMoove;
    [Range(0, 1)] public float airControl = 0.3f;
    private Rigidbody airRigidbody;

    private void Awake()
    {
        airRigidbody = GetComponent<Rigidbody>();
    }


    public void Move(PlayerDirection direction, Player air, RotateDirection rotationDirection)
    {
        airRigidbody.AddRelativeForce(direction.Lateral * MoveAcceleration, 0, direction.Forward * MoveAcceleration);
    }

    public void Jump(PlayerDirection direction, Player air)
    {
        Debug.Log("AirJump");

        if (direction.Forward != 1 && direction.Forward != -1 && direction.Lateral != 1 && direction.Lateral != -1)
            airRigidbody.AddForce(0, FlyAcceleration, 0, ForceMode.Force);

        else
            airRigidbody.AddForce(0, FlyAccelerationWhenMoove, 0, ForceMode.Force);
        LoseBoostEvent?.Invoke(0.1f);
    }

    public override void TakeDamage(float airDamageValue)
    {
        Debug.LogError("Air TakeDamage");
    }

    public override void TakeHealth(float damageValue)
    {
        Debug.LogError("Air TakeHealth");
    }

    private void OnCollisionEnter(Collision player)
    {
      //  if (player.gameObject.TryGetComponent(out Enemy enemy))
      {
          TakeDamage(100);
          TakeDamageEvent?.Invoke(100);
      }
    }
    public void Died()
    {
        Debug.LogError("AirDied");
    }

    public void AddBoostHp()
        => CurrentBoostHP = 10;
}