using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;
using UniRx;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using Zenject;

public class Water : Player, IMove, IJump, IShift, IDied
{
    private HUD_Service _HUDService;
    [SerializeField] private PlayerController playerController;

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
                Debug.Log("FULL HEALTH");
            }
            else
            {
                currentHealthHP = _HUDService.WaterHealthHP = value;
            }

            Debug.LogError(CurrentHealthHP);
        }
    }

    public override float MaxHealthHP => 100;

    private float currentBoostHP = 100;

    public override float CurrentBoostHP
    {
        get => currentBoostHP;
        set
        {
            if (value < 0)
            {
                UnSticking();
                currentBoostHP = _HUDService.WaterBoostHP = 0;
                Debug.LogError("Water NO Boost");
            }

            if (value > MaxBoostHP)
            {
                currentBoostHP = _HUDService.WaterBoostHP = MaxBoostHP;
                Debug.LogError("FULL Boost");
            }
            else
            {
                currentBoostHP = _HUDService.WaterBoostHP = value;
            }
        }
    }

    public override float MaxBoostHP => 10;


    [SerializeField] private float moveAcceleration;
    [SerializeField] private float jumpAcceleration;
    [SerializeField] private ObiCollisionMaterial stickinessMaterial;
    private ObiCollisionMaterial defaultMaterial;

    public Transform referenceFrame;

    private bool isStickiness;

    private bool IsStickiness
    {
        get => isStickiness;
        set
        {
            isStickiness = value;
            if (IsStickiness)
            {
                this.water.collisionMaterial = stickinessMaterial;
                BoostHPTake();
            }
            else
            {
                BoostHPAdd();
                this.water.collisionMaterial = defaultMaterial;
            }
        }
    }


    private ObiSoftbody water;
    private ObiSolver waterSolver;
    [SerializeField] private float boostGettingSpeed;
    private CompositeDisposable compositeDisposable = new();

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
        if (!IsStickiness)
        {
            Sticking();
        }
        else
        {
            UnSticking();
        }
    }

    private void UnSticking()
    {
        IsStickiness = false;
    }

    private void Sticking()
    {
        IsStickiness = true;
    }


    public void Jump(PlayerDirection direction, Player water)
    {
        Debug.LogError("WaterJump");
        this.water.AddForce(new Vector3(0, jumpAcceleration, 0), ForceMode.Impulse);
    }

    public void BoostHPAdd()
    {
        Observable.EveryUpdate().Subscribe(_ =>
        {
            CurrentBoostHP += boostGettingSpeed;
            Debug.LogError(CurrentBoostHP);

            if (CurrentBoostHP >= MaxBoostHP)
            {
                Debug.LogError("Regeniration Ended!!!");
                compositeDisposable.Clear();
               // IsStickiness = false;
            }
        }).AddTo(compositeDisposable);
    }

    private void BoostHPTake()
    {
        if (CurrentBoostHP <= 0)
            return;
        
        Observable.EveryUpdate().Subscribe(_ =>
        {
            CurrentBoostHP -= boostGettingSpeed;

            Debug.LogError(CurrentBoostHP);

            if (CurrentBoostHP <= 0)
            {
                Debug.LogError("Regeniration Ended");
                compositeDisposable.Clear();
            }
        }).AddTo(compositeDisposable);
    }


    public void TakeDamage(float waterDamageValue)
    {
        Debug.LogError("Water TakeDamage" + waterDamageValue);
    }

    public void TakeHealth(float waterDamageValue)
    {
        Debug.LogError("Water TakeHealth" + waterDamageValue);
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
                    if (col.gameObject.TryGetComponent(out DamagePlatfom damagePlatfom))
                    {
                        TakeDamage(damagePlatfom.waterDamage);
                        CurrentHealthHP -= damagePlatfom.waterDamage;
                        //   TakeDamageEvent?.Invoke(damagePlatfom.waterDamage);
                    }

                    else if (col.gameObject.TryGetComponent(out HealthPlatform healthPlatform))
                    {
                        TakeHealth(healthPlatform.waterHealth);
                        CurrentHealthHP += healthPlatform.waterHealth;
                        // TakeHealthEvent?.Invoke(healthPlatform.waterHealth);
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