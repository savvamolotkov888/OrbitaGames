using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class Air : Player, IMove, IJump
{
    private CompositeDisposable compositeDisposable = new();
    private HUD_Service _HUDService;

    [SerializeField] private bool canTakeDamage;

    [SerializeField] private float boostLoseSpeed; //с какой скоростью теряет буст
    [SerializeField] private float boostGettingSpeed;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private float maxHealthHP;
    [SerializeField] private float MoveAcceleration;
    [SerializeField] private float FlyAcceleration;
    [SerializeField] private float FlyAccelerationWhenMoove;
    [SerializeField] private float ImortalityTime;
    [Range(0, 1)] public float airControl = 0.3f;


    public override float CurrentHealthHP
    {
        get => currentHealthHP;
        protected set
        {
            currentHealthHP = _HUDService.AirHealthHP = value;
            if (value < 0)
            {
                currentHealthHP = _HUDService.AirHealthHP = 0;
                Died();
            }
        }
    }

    public override float CurrentBoostHP
    {
        get => currentBoostHP;
        protected set
        {
            currentBoostHP = _HUDService.AirBoostHP = value;
            if (value < 0)
            {
                playerController.TransformaitionToPreviousState();
                Debug.LogError("NO Boost");
            }

            if (value > MaxBoostHP)
            {
                CurrentBoostHP = MaxBoostHP;
                Debug.LogError("FULL Boost");
            }
        }
    }

    private Rigidbody airRigidbody;


    [Inject]
    private void Construct(HUD_Service _HUD_Service)
    {
        _HUDService = _HUD_Service;
    }

    private void Awake()
    {
        airRigidbody = GetComponent<Rigidbody>();
        PlayerController.ToIce += BoostHealthRegeniration;
        PlayerController.ToWater += BoostHealthRegeniration;
        currentHealthHP = maxHealthHP;
        currentBoostHP = MaxBoostHP;
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


        CurrentBoostHP -= boostLoseSpeed;
    }

    public void BoostHealthRegeniration()
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

    private void OnCollisionEnter(Collision player)
    {
        if (canTakeDamage)
        {
            Debug.LogError(player.gameObject.name + "!!");
            CurrentHealthHP -= 1;
        }
    }

    public override void Died()
    {
        Debug.LogError("AirDied");
    }

    private async void OnEnable()
    {
        canTakeDamage = false;
        await Task.Delay(TimeSpan.FromSeconds(ImortalityTime));
        canTakeDamage = true;
        Debug.LogError(canTakeDamage);
    }

    public void OnDisable()
    {
        canTakeDamage = false;
    }
}