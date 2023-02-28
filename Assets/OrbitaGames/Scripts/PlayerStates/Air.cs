using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class Air : Player, IMove, IJump, IDied
{
    private HUD_Service _HUDService;

    public bool CanTakeDamage = false;

    [SerializeField] private PlayerController playerController;

    private float currentHealthHP = 1;
    public override float CurrentHealthHP
    {
        get => currentHealthHP;
        set => currentHealthHP = _HUDService.AirHealthHP = value;
    }

    public override float MaxHealthHP => 1;
    
    private float currentBoostHP = 10;
    public override float CurrentBoostHP 
    {
        get => currentBoostHP;
        set => currentBoostHP = _HUDService.AirBoostHP = value;
    }
    public override float MaxBoostHP => 10;
    public override event Action<float> TakeDamageEvent;
    public override event Action<float> TakeHealthEvent;
    public override event Action<float> LoseBoostEvent;

    [SerializeField] private float MoveAcceleration;
    [SerializeField] private float FlyAcceleration;
    [SerializeField] private float FlyAccelerationWhenMoove;
    [SerializeField] private float ImortalityTime;
    [Range(0, 1)] public float airControl = 0.3f;
    private Rigidbody airRigidbody;


    [Inject]
    private void Construct(HUD_Service _HUD_Service)
    {
        _HUDService = _HUD_Service;
    }

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
        if (CanTakeDamage)
        {
            Debug.LogError(player.gameObject.name + "!!");
            TakeDamage(100);
            TakeDamageEvent?.Invoke(100);
        }
    }

    public void Died()
    {
        Debug.LogError("AirDied");
    }

    private async void OnEnable()
    {
        await Task.Delay(TimeSpan.FromSeconds(ImortalityTime));
        CanTakeDamage = true;
    }

    private void OnDisable()
    {
        CanTakeDamage = false;
    }

    public void AddBoostHp()
        => CurrentBoostHP = 10;
}