using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private const string airTag = "Air";
    private const string waterTag = "Water";
    private const string iceTag = "Ice";

    public float iceDamage;
    public float waterDamage;

    [Header("Для всех состояний одинаковый урон")] [SerializeField]
    private float universalDamageValue;

    private void Awake()
    {
        if (universalDamageValue != 0)
            iceDamage = waterDamage = universalDamageValue;
        
    }
}