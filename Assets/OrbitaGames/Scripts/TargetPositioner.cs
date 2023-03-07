using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TargetPositioner : MonoBehaviour
{
    [SerializeField] private short ZDistance;
    [SerializeField] private Transform playerPosition;
    private HUD_Service _HUDService;
    private Camera camera;
    
    
    private Vector3 _targetVector;
    private Vector3 targetVector;


    [Inject]
    private void Construct(HUD_Service _HUD_Service)
    {
        _HUDService = _HUD_Service;
    }

    private void Start()
    {
        camera = _HUDService.GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        _targetVector = camera.transform.TransformPoint(0, 0, ZDistance);
        targetVector = new Vector3(_targetVector.x, playerPosition.position.y, _targetVector.z);
    }

    private void LateUpdate()
    {
        transform.position = targetVector;
    }
}