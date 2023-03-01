using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    public float MaxHealthHP;

    protected float currentHealthHP;
    public abstract float CurrentHealthHP { get; protected set; }


    //---------------------------------------------------------


    public float MaxBoostHP;

    protected float currentBoostHP;
    public abstract float CurrentBoostHP { get; protected set; }


    //---------------------------------------------------------

    protected abstract void Died();


    private void Awake()
    {
        currentHealthHP = MaxHealthHP;
        currentBoostHP = MaxBoostHP;
    }
}