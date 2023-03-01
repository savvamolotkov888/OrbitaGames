using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    protected float currentHealthHP;
    public abstract float CurrentHealthHP { get; protected set; }
    
    
    public abstract float CurrentBoostHP { get; protected set;}
    protected abstract void Died();
    // public abstract float BoostHPTakenValue { get; set;}
    // public abstract float BoostHPAddValue { get; set;}
    public abstract  float MaxBoostHP { get;} 
    public abstract  float MaxHealthHP { get;}
    
  

}