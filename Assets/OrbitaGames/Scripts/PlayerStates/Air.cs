using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using Zenject;

public class Air : Player, IMove, IJump, IHealthRegeneration, IDied
{
    private CompositeDisposable compositeDisposable = new();
    private HUD_Service _HUDService;
    public bool CanTakeDamage;
    [SerializeField] private float boostLoseSpeed; //с какой скоростью теряет буст
    [SerializeField] private float boostGettingSpeed;

    [SerializeField] private PlayerController playerController;

    private float currentHealthHP; // кол во здоровья

    public override float CurrentHealthHP
    {
        get => currentHealthHP;
        set => currentHealthHP = _HUDService.AirHealthHP = value;
    }

    [SerializeField] private float maxHealthHP;
    public override float MaxHealthHP => maxHealthHP;

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
        PlayerController.ToIce += Regeniration;
        PlayerController.ToWater += Regeniration;
        currentHealthHP = maxHealthHP;
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

        //   LoseBoostEvent?.Invoke(0.1f);
        CurrentBoostHP -= boostLoseSpeed;
    }

    public void Regeniration()
    {
        Debug.LogError("Regeniration");
        Observable.EveryUpdate().Subscribe(_ =>
        {
            CurrentBoostHP += boostGettingSpeed;
            Debug.LogError(CurrentBoostHP);

            if (CurrentBoostHP > MaxBoostHP || playerController.currentState == PlayerState.Air)
            {
                Debug.LogError("Regeniration Ended");
                compositeDisposable.Clear();
            }
        }).AddTo(compositeDisposable);
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
            TakeDamage(1);
            TakeDamageEvent?.Invoke(1);
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
}