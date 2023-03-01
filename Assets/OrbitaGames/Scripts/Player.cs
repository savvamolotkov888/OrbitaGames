using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    public float MaxHealthHP;

    public float currentHealthHP;
    public abstract float CurrentHealthHP { get; protected set; }


    //---------------------------------------------------------


    public float MaxBoostHP;

    public float currentBoostHP;
    public abstract float CurrentBoostHP { get; protected set; }


    //---------------------------------------------------------

    public abstract void Died();

    public void Awake()
    {
        currentHealthHP = CurrentHealthHP = MaxHealthHP;
        currentBoostHP = CurrentBoostHP = MaxBoostHP;
    }
}