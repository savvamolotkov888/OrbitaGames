using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceDouble : MonoBehaviour
{
    [SerializeField] private Transform player;

    // Update is called once per frame
    void Update()
    {
        
        transform.rotation = player.rotation * Quaternion.Euler(0,90,0);
        Debug.DrawRay(transform.position, transform .position + transform.up*200, Color.white);
    }
}