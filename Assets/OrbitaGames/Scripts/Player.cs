using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    public abstract float CurrentHealthHP { get; set;}

    public abstract float CurrentBoostHP { get; set;}
    // public abstract float BoostHPTakenValue { get; set;}
    // public abstract float BoostHPAddValue { get; set;}
    public abstract  float MaxBoostHP { get;} 
    public abstract  float MaxHealthHP { get;}
    
    public abstract void TakeDamage(float damageValue);
    public abstract void TakeHealth(float damageValue);

    public abstract event Action<float> TakeDamageEvent;
    public abstract event Action<float> TakeHealthEvent;
    public abstract event Action<float> LoseBoostEvent;
  //  public abstract event Action<float> TakeHealthEvent;
}