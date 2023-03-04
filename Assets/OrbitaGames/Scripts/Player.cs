using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using Task = System.Threading.Tasks.Task;

public abstract class Player : MonoBehaviour
{
    [SerializeField] private float HealhChangeTime;
    public float MaxHealthHP;

    protected float currentHealthHP;
    public abstract float CurrentHealthHP { get; protected set; }

    // protected abstract void HealthRegeneration();


    //---------------------------------------------------------


    public float MaxBoostHP;

    protected float currentBoostHP;
    public abstract float CurrentBoostHP { get; protected set; }

    protected abstract void BoostRegeneration();
    protected abstract void LosingHealthHP(float damage);
    protected abstract void AddingHealthHP(float addedHealth);


    //---------------------------------------------------------

    protected abstract void Died();

    protected void Initialize()
    {
        currentHealthHP = CurrentHealthHP = MaxHealthHP;
        currentBoostHP = CurrentBoostHP = MaxBoostHP;
    }

    protected bool canChangeHealthHP;
    protected async void FixedChangeHealthHP()
    {
        canChangeHealthHP = false;
        await Task.Delay(TimeSpan.FromSeconds(HealhChangeTime));
        canChangeHealthHP = true;
    }
}