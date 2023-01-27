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

    public PlayerSensor playerSensor;

    [SerializeField] private float waterMoveAcceleration;
    [SerializeField] private float waterRotationAcceleration;

    [SerializeField] private float iceMoveAcceleration;
    [SerializeField] private float iceRotationAcceleration;

    [SerializeField] private float airMoveAcceleration;
    [SerializeField] private float airRotationAcceleration;

    [SerializeField] private float waterJumpBoost;
    [SerializeField] private float iceJumpBoost;
    [SerializeField] private float airJumpBoost;

    public float smoothFacor;
    [SerializeField] private Transform targetA;
    public Transform targetB;

    public ObiSolver WaterSolwer;
    public ObiActor waterActor;

    public ObiSoftbody softbody;


    private GameObject Water;
    private GameObject Ice;
    private GameObject Air;
    private GameObject currentGameobjectState;

    private float moveAcceleration;
    private float RotationAcceleration;
    private float jumpBoost;

    private InputSystem inputSystem;
    private PlayerState currentState;

    public PlayerState CurrentState
    {
        get => currentState;
        set
        {
            currentState = value;
            WaterSolwer.gameObject.SetActive(false);
            ice.gameObject.SetActive(false);
            air.gameObject.SetActive(false);

            switch (currentState)
            {
                case PlayerState.Water:
                    break;
                case PlayerState.Ice:
                    ice.transform.position = transform.position;
                    ice.gameObject.SetActive(true);
                    currentGameobjectState = Ice;
                    break;
                case PlayerState.Air:
                    air.transform.position = transform.position;
                    air.gameObject.SetActive(true);
                    currentGameobjectState = Air;
                    break;
            }
        }
    }


    public Water water;
    public Ice ice;
    public Air air;


    private Vector3 LookAtpoz;
    void Jump(IJump jump) => jump.Jump(currentGameobjectState, jumpBoost);

    void Move(IMove movable) =>
        movable.Move(Direction, currentGameobjectState, moveAcceleration, RotationAcceleration,
            playerSensor.rorator);

    private void Awake()
    {
        Direction = new PlayerDirection();
        //Cursor.lockState = CursorLockMode.Locked;
        // Water = water.gameObject;

        Ice = ice.gameObject;
        Air = air.gameObject;

        CurrentState = PlayerState.Ice;
        currentGameobjectState = Ice;

        inputSystem = new InputSystem();


        inputSystem.Transformation.ToIce.performed += context => TransformaitionToIce();
        inputSystem.Transformation.ToAir.performed += context => TransformaitionToAir();
    }

    private void Update()
    {
        Direction.Forward = inputSystem.Control.MoveVertical.ReadValue<float>();
        Direction.Lateral = inputSystem.Control.MoveGorizontal.ReadValue<float>();
        Direction.Up = inputSystem.Control.Jump.ReadValue<float>();

        if (inputSystem.Transformation.ToWater.ReadValue<float>() > 0)
            TransformaitionToWater();
        if (inputSystem.Transformation.ToIce.ReadValue<float>() > 0)
            TransformaitionToIce();
        if (inputSystem.Transformation.ToAir.ReadValue<float>() > 0)
            TransformaitionToAir();
        LookAtpoz = Vector3.Lerp(targetB.position,
            new Vector3(targetA.position.x, ice.transform.position.y, targetA.position.z), smoothFacor);
        targetB.position = LookAtpoz;
        UpdatePosition();
    }

    private void FixedUpdate()
    {    
        
        Move();
        Jump();
    }

    private void LateUpdate()
    {
   
    }

    void Move()
    {
        switch (currentState)
        {
            case PlayerState.Water:
                moveAcceleration = waterMoveAcceleration;
                RotationAcceleration = waterRotationAcceleration;
                Move(water);
                break;
            case PlayerState.Ice:
                if (playerSensor.OnTheFloar)
                {
                    moveAcceleration = iceMoveAcceleration;
                    RotationAcceleration = iceRotationAcceleration;
                    Move(ice);
                }

                break;
            case PlayerState.Air:
                moveAcceleration = airMoveAcceleration;
                RotationAcceleration = airRotationAcceleration;
                air.transform.LookAt(targetB);
                Move(air);
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
                if (playerSensor.OnTheFloar && Direction.Up > 0)
                {
                    jumpBoost = iceJumpBoost;
                    Jump(ice);
                }

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
                transform.position = water.transform.position;
                break;
            case PlayerState.Ice:
                transform.position = ice.transform.position;

                break;
            case PlayerState.Air:
                transform.position = air.transform.position;

                break;
        }
    }

    void TransformaitionToWater()
    {
        Debug.Log("TransformaitionToWater");
        Ice.SetActive(false);
        Air.SetActive(false);
        currentState = PlayerState.Water;
        WaterSolwer.gameObject.SetActive(true);
        softbody.Teleport(transform.position, transform.rotation);
        gameObject.transform.position = water.transform.position;
    }

    void TransformaitionToIce()
    {
        
        Debug.Log("TransformaitionToIce");
        WaterSolwer.gameObject.SetActive(false);
        air.gameObject.SetActive(false);
        
        currentState = PlayerState.Ice;
        ice.transform.position = transform.position;
        ice.gameObject.SetActive(true);
        currentGameobjectState = Air;
    }

    void TransformaitionToAir()
    {
        Debug.Log("TransformaitionToAire");
        WaterSolwer.gameObject.SetActive(false);
        Ice.gameObject.SetActive(false);
        
        currentState = PlayerState.Air;
        air.transform.position = transform.position;
        air.gameObject.SetActive(true);
        currentGameobjectState = Air;
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