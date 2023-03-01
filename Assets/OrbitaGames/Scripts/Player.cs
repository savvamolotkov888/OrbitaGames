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
    
  

}