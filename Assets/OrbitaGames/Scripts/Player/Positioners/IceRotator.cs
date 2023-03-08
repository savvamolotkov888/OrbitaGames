using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceRotator : MonoBehaviour
{
    [SerializeField] private Transform player;

    private void Update()
    {
        transform.position = player.transform.position;
            //    transform.rotation = Quaternion.Euler(0, player.rotation.eulerAngles.y, player.rotation.eulerAngles.z);
    }
}