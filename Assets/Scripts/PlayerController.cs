using System;
using Obi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    public PlayerDirection Direction;
    public Transform Target;
    public PlayerRotationController playerRotationController;

    [SerializeField] private float waterMoveAcceleration;
    [SerializeField] private float waterRotationAcceleration;
     
    [SerializeField] private float iceMoveAcceleration;
    [SerializeField] private float iceRotationAcceleration;
    
    [SerializeField] private float airMoveAcceleration;
    [SerializeField] private float airRotationAcceleration;
    
    [SerializeField] private float waterJumpBoost;
    [SerializeField] private float iceJumpBoost;
    [SerializeField] private float airJumpBoost;

    [SerializeField] private Transform targetDirection;

    private ObiActor obiActor;

    private GameObject Water;
    private GameObject Ice;
    private GameObject Air;
    private GameObject currentGameobjectState;

    private float moveAcceleration;
    private float RotationAcceleration;
    private float jumpBoost;

    private Vector3 lastPosition;

    private InputSystem inputSystem;
    private PlayerState currentState;

    public PlayerState CurrentState
    {
        get => currentState;
        set
        {
            currentState = value;
            water.gameObject.SetActive(false);
            ice.gameObject.SetActive(false);
            air.gameObject.SetActive(false);

            switch (currentState)
            {
                case PlayerState.Water:
                    water.transform.position = lastPosition;
                    water.gameObject.SetActive(true);
                    currentGameobjectState = Water;
                    break;
                case PlayerState.Ice:
                    ice.transform.position = lastPosition;
                    ice.gameObject.SetActive(true);
                    currentGameobjectState = Ice;
                    break;
                case PlayerState.Air:
                    air.transform.position = lastPosition;
                    air.gameObject.SetActive(true);
                    currentGameobjectState = Air;
                    break;
            }
        }
    }


    public Water water;
    public Ice ice;
    public Air air;


    public IJump jump;
    private Vector3 LookAtpoz;
    void Jump(IJump jump) => jump.Jump(currentGameobjectState, jumpBoost);

    void MoveForward(IMove movable) =>
        movable.Move(Direction, currentGameobjectState, moveAcceleration,RotationAcceleration , playerRotationController.rorator);

    private void Awake()
    {
        Direction = new PlayerDirection();
        //Cursor.lockState = CursorLockMode.Locked;
        Water = water.gameObject;
        Ice = ice.gameObject;
        Air = air.gameObject;

        currentState = PlayerState.Ice;
        currentGameobjectState = Ice;

        inputSystem = new InputSystem();
        inputSystem.Control.Jump.performed += context => Jump();

        inputSystem.Transformation.ToWater.performed += context => TransformaitionToWater();
        inputSystem.Transformation.ToIce.performed += context => TransformaitionToIce();
        inputSystem.Transformation.ToAir.performed += context => TransformaitionToAir();
    }

    private void Update()
    {


        Direction.Forward = inputSystem.Control.MoveVertical.ReadValue<float>();
        Direction.Lateral = inputSystem.Control.MoveGorizontal.ReadValue<float>();
        
        Debug.Log(Direction.Forward );
        
        Direction.TargetDirection.x = Target.position.x;
        Direction.TargetDirection.z = Target.position.z;

    }

    private void FixedUpdate()
    {
        LookAtpoz = new Vector3(targetDirection.position.x, ice.transform.position.y, targetDirection.position.z);
        Target.position = LookAtpoz;
        MoveForward();


    }

    private void LateUpdate()
    {
        UpdatePosition();
    }

    void MoveForward()
    {
        switch (currentState)
        {
            case PlayerState.Water:
                moveAcceleration = waterMoveAcceleration;
                RotationAcceleration = waterRotationAcceleration;
                MoveForward(water);
                break;
            case PlayerState.Ice:
                moveAcceleration = iceMoveAcceleration;
                RotationAcceleration = iceRotationAcceleration;
                MoveForward(ice);
                break;
            case PlayerState.Air:
                moveAcceleration = airMoveAcceleration;
                RotationAcceleration = airRotationAcceleration;
                MoveForward(air);
                break;
        }
    }

    void Jump()
    {
        switch (currentState)
        {
            case PlayerState.Water:
                jumpBoost = waterJumpBoost;
                Jump(water);
                break;
            case PlayerState.Ice:
                jumpBoost = iceJumpBoost;
                Jump(ice);
                break;
            case PlayerState.Air:
                jumpBoost = airJumpBoost;
                Jump(air);
                break;
        }
    }

    void UpdatePosition()
    {
        switch (currentState)
        {
            case PlayerState.Water:
                lastPosition = water.transform.position;
                break;
            case PlayerState.Ice:
                lastPosition = ice.transform.position;

                break;
            case PlayerState.Air:
                lastPosition = air.transform.position;

                break;
        }
    }

    void TransformaitionToWater()
    {
        Debug.Log("TransformaitionToWater");
        gameObject.transform.position = water.transform.position;
        CurrentState = PlayerState.Water;
    }

    void TransformaitionToIce()
    {
        Debug.Log("TransformaitionToIce");
        gameObject.transform.position = ice.transform.position;
        CurrentState = PlayerState.Ice;
    }

    void TransformaitionToAir()
    {
        gameObject.transform.position = air.transform.position;
        Debug.Log("TransformaitionToAire");
        CurrentState = PlayerState.Air;
    }


    private void OnEnable()
    {
        inputSystem.Enable();
    }

    private void OnDisable()
    {
        inputSystem.Disable();
    }
}

public enum PlayerState
{
    Water,
    Ice,
    Air
}