using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;
using UniRx;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using Zenject;

public class Water : Player, IMove, IJump, IShift
{
    private HUD_Service _HUDService;

    [Inject]
    private void Construct(HUD_Service _HUD_Service)
    {
        _HUDService = _HUD_Service;
    }


    public override float CurrentHealthHP
    {
        get => currentHealthHP;
        protected set
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

    public override float CurrentBoostHP
    {
        get => currentBoostHP;
        protected set
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
                BoostHPMinus();
            }
            else
            {
                BoostRegeneration();
            }
        }
    }


    private ObiSoftbody water;
    private ObiSolver waterSolver;

    [FormerlySerializedAs("boostGettingSpeed")] [SerializeField]
    private float boostSpeed;

    private CompositeDisposable compositeDisposable1 = new();
    private CompositeDisposable compositeDisposable2 = new();

    private void Start()
    {
        defaultMaterial = GetComponent<ObiSoftbody>().collisionMaterial;
        water = GetComponent<ObiSoftbody>();
        waterSolver = GetComponentInParent<ObiSolver>();
        waterSolver.OnCollision += WaterSolver_OnCollision;

        Initialize();
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
        Debug.LogError("UnSticking");
        this.water.collisionMaterial = defaultMaterial;
        IsStickiness = false;
    }

    private void Sticking()
    {
        this.water.collisionMaterial = stickinessMaterial;
        IsStickiness = true;
    }


    public void Jump(PlayerDirection direction, Player water)
    {
        Debug.LogError("WaterJump");
        this.water.AddForce(new Vector3(0, jumpAcceleration, 0), ForceMode.Impulse);
    }

    protected override void BoostRegeneration()
    {
        if (CurrentBoostHP >= MaxBoostHP)
            return;
        Debug.LogError("BoostHPAdd");

        Observable.EveryUpdate().Subscribe(_ =>
        {
            Debug.LogError("1");
            CurrentBoostHP += boostSpeed;
            Debug.LogError(CurrentBoostHP);

            if (CurrentBoostHP >= MaxBoostHP)
            {
                Debug.LogError("Regeniration Ended!!!");
                compositeDisposable2.Clear();
            }
        }).AddTo(compositeDisposable2);
    }

    private void BoostHPMinus()
    {
        compositeDisposable1.Clear();
        if (CurrentBoostHP <= 0)
            return;
        compositeDisposable1 = new CompositeDisposable();
        Observable.EveryUpdate().Subscribe(_ =>
        {
            CurrentBoostHP -= boostSpeed;

            Debug.LogError(CurrentBoostHP);

            if (CurrentBoostHP <= 0 || IsStickiness == false)
            {
                Debug.LogError("Regeniration Ended");
                UnSticking();
                compositeDisposable1.Clear();
            }
        }).AddTo(compositeDisposable1);
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
                    }
                }
            }
        }
    }

    protected override void Died()
    {
        Debug.LogError("WaterDied");
    }
}