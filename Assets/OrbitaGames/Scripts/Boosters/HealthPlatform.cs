using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPlatform : MonoBehaviour
{
    public float iceHealth;
    public float waterHealth;

    [Header("Для всех состояний одинаковая прибавка")] [SerializeField]
    private float universalDamageValue;

    private void Awake()
    {
        if (universalDamageValue != 0)
            iceHealth = waterHealth = universalDamageValue;
    }
}
