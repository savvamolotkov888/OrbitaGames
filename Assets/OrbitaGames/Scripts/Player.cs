using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    public abstract void TakeDamage(float damageValue);

    public abstract event Action<float> TakeDamageEvent;
}