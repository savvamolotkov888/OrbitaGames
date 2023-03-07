using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetVerticalPositioner : MonoBehaviour
{
    [SerializeField] private short YDistance;
    [SerializeField] private Transform playerPosition;

    private Vector3 targetVector;


    private void Update()
    {
        targetVector = new Vector3(playerPosition.position.x, playerPosition.position.y + YDistance,
            playerPosition.position.z);
    }

    private void LateUpdate()
    {
        transform.position = targetVector;
    }
}