using System;
using Obi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Task = System.Threading.Tasks.Task;


public class PlayerController : MonoBehaviour
{
    public PlayerDirection Direction;
    [SerializeField] private PlayerSensor playerSensor;
    public ActorCOMTransform softbodyCOM;


    public PlayerState PlayerStateAtStart;

    [SerializeField] private float IceAcelerationTime; // пока не используем
    [SerializeField] private float IceDoubleShiftDelay;
    private short clickCount;

    #region CameraDebug

    [SerializeField] private Transform targetA;
    public Transform targetB;
    public float smoothFacor;

    #endregion

    public ObiSolver WaterSolwer;
    private ObiActor waterActor;

    private Player currentGameobjectState;

    private float moveAcceleration;
    private float RotationAcceleration;
    private float jumpBoost;

    private InputSystem inputSystem;

    private PlayerState currentState;

    private PlayerState CurrentState
    {
        get => currentState;
        set
        {
            currentState = value;
            switch (currentState)
            {
                case PlayerState.Water:
                    currentGameobjectState = water;
                    break;
                case PlayerState.Ice:
                    currentGameobjectState = ice;
                    break;

                case PlayerState.Air:
                    currentGameobjectState = air;
                    break;
            }
        }
    }


    #region PlayerTypes

    [SerializeField] private Water water;
    [SerializeField] private Ice ice;
    [SerializeField] private Air air;

    # endregion


    private Vector3 LookAtpoz;
    void Jump(IJump jump) => jump.Jump(Direction, currentGameobjectState);

    void Move(IMove movable) =>
        movable.Move(Direction, currentGameobjectState, playerSensor.Rorator);

    void Shift(IShift shift) =>
        shift.Shift(Direction, currentGameobjectState, IceAcelerationTime);

    void DoubleShift(IDoubleShift doubleShift) =>
        doubleShift.DoubleShift(Direction, currentGameobjectState, IceAcelerationTime);

    private void Awake()
    {
        Direction = new PlayerDirection();
        //Cursor.lockState = CursorLockMode.Locked;
        inputSystem = new InputSystem();
        InitializePlayerState();

        waterActor = water.GetComponent<ObiActor>();

        inputSystem.Transformation.ToIce.performed += context => TransformaitionToIce();
        inputSystem.Transformation.ToAir.performed += context => TransformaitionToAir();

        inputSystem.Control.DoubleShift.performed += context => DoubleClickCheck(IceDoubleShiftDelay);
    }

    private void Update()
    {
        Direction.Forward = inputSystem.Control.MoveVertical.ReadValue<float>();
        Direction.Lateral = inputSystem.Control.MoveGorizontal.ReadValue<float>();
        Direction.Up = inputSystem.Control.Jump.ReadValue<float>();
        Direction.Shift = inputSystem.Control.Shift.ReadValue<float>();


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
        Shift();
    }


    void Move()
    {
        switch (CurrentState)
        {
            case PlayerState.Water:
                Move(water);
                break;
            case PlayerState.Ice:
                if (playerSensor.CanJump)
                    Move(ice);
                break;
            case PlayerState.Air:
                air.transform.LookAt(targetB);
                Move(air);
                break;
        }
    }

    void Jump()
    {
        switch (CurrentState)
        {
            case PlayerState.Water:
                if (playerSensor.CanJump && Direction.Up > 0)
                {
                    Jump(water);
                    playerSensor.CanJump = false;
                }

                break;
            case PlayerState.Ice:
                if (playerSensor.CanJump && Direction.Up > 0)
                {
                    Jump(ice);
                    playerSensor.CanJump = false;
                }

                break;

            case PlayerState.Air:
                Jump(air);
                break;
        }
    }

    void Shift()
    {
        if (CurrentState == PlayerState.Ice)
        {
            if (clickCount >=2)
            {
                DoubleShift(ice);
                clickCount = 0;
            }
            else
            {
                Shift(ice);
            }
        }
    }
    

    void UpdatePosition()
    {
        switch (CurrentState)
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

    void InitializePlayerState()
    {
        WaterSolwer.gameObject.SetActive(false);
        ice.gameObject.SetActive(false);
        air.gameObject.SetActive(false);
        switch (PlayerStateAtStart)
        {
            case PlayerState.Water:
                CurrentState = PlayerState.Water;
                WaterSolwer.gameObject.SetActive(true);
                break;
            case PlayerState.Ice:
                CurrentState = PlayerState.Ice;
                ice.gameObject.SetActive(true);
                break;

            case PlayerState.Air:
                CurrentState = PlayerState.Air;
                air.gameObject.SetActive(true);
                break;
        }
    }

    private void TransformaitionToWater()
    {
        if (currentState == PlayerState.Water) return;
        Debug.Log("TransformaitionToWater");
        ice.gameObject.SetActive(false);
        air.gameObject.SetActive(false);

        CurrentState = PlayerState.Water;

        WaterSolwer.gameObject.SetActive(true);

        waterActor.Teleport(transform.position, transform.rotation);

        softbodyCOM.Update();

        gameObject.transform.position = water.transform.position;
    }

    private void TransformaitionToIce()
    {
        if (currentState == PlayerState.Ice) return;

        Debug.Log("TransformaitionToIce");
        WaterSolwer.gameObject.SetActive(false);
        air.gameObject.SetActive(false);

        CurrentState = PlayerState.Ice;
        ice.transform.position = transform.position;
        ice.gameObject.SetActive(true);
        CurrentState = PlayerState.Ice;
    }

    private void TransformaitionToAir()
    {
        if (currentState == PlayerState.Air) return;

        Debug.Log("TransformaitionToAire");
        WaterSolwer.gameObject.SetActive(false);
        ice.gameObject.SetActive(false);

        CurrentState = PlayerState.Air;
        air.transform.position = transform.position;
        air.gameObject.SetActive(true);
    }

    private void DoubleClickCheck(float time )
    {
        ResetClickCount(time);
        clickCount++;
    }

    private async void ResetClickCount(float time)
    {
        await Task.Delay(TimeSpan.FromSeconds(time));
        {
            clickCount = 0;
//            Debug.LogError("RESSET");   
        }
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