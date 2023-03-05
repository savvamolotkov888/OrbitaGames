using System;
using Obi;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using Task = System.Threading.Tasks.Task;


public class PlayerController : MonoBehaviour
{
    public static event Action ToWater;
    public static event Action ToIce;
    public static event Action ToAir;

    public PlayerDirection Direction;
    [SerializeField] private PlayerSensor playerSensor;
    [SerializeField] private PlayerState PlayerStateAtStart;

    [SerializeField] private float IceAcelerationTime; // пока не используем
    [SerializeField] private float IceDoubleShiftDelay;
    private short clickCount; //for double clicks

    [FormerlySerializedAs("IceRotator")] [SerializeField] private Transform IceY_Rotator;

    #region CameraDebug

    [SerializeField] private Transform targetA;
    public Transform targetB;
    public float smoothFacor;

    #endregion

    [SerializeField] private ObiSolver WaterSolwer;
    [SerializeField] private ObiActor waterActor;

    private Player currentGameobjectState;

    private float moveAcceleration;
    private float RotationAcceleration;
    private float jumpBoost;
    [Range(0, 1)] public float waterAirControl = 0.3f;

    private InputSystem inputSystem;

    public PlayerState currentState { get; private set; }
    private Vector3 com;
    private PlayerState previousState;

    private PlayerState CurrentState
    {
        get => currentState;
        set
        {
            previousState = currentState;
            currentState = value;
            switch (currentState)
            {
                case PlayerState.Water:
                    currentGameobjectState = water;
                    ToWater?.Invoke();
                    break;
                case PlayerState.Ice:
                    currentGameobjectState = ice;
                    ToIce?.Invoke();
                    break;
                case PlayerState.Air:
                    currentGameobjectState = air;
                    ToAir?.Invoke();
                    break;
            }
        }
    }


    #region PlayerTypes

    public Water water;
    public Ice ice;
    public Air air;

    # endregion

    #region Rigidbody

    private Rigidbody iceRigidbody;
    private Rigidbody airRigidbody;

    # endregion

    #region BooostHP

    [SerializeField] private float IceBoostHP;

    #endregion

    private Vector3 LookAtpoz;

    #region Interfaces

    void Jump(IJump jump) => jump.Jump(Direction, currentGameobjectState);

    void Move(IMove movable) =>
        movable.Move(Direction, currentGameobjectState, playerSensor.Rorator);

    void Shift(IShift shift) =>
        shift.Shift(Direction, currentGameobjectState, IceAcelerationTime);

    void DoubleShift(IDoubleShift doubleShift) =>
        doubleShift.DoubleShift(Direction, currentGameobjectState, IceAcelerationTime);

    #endregion

    private void Awake()
    {
        Direction = new PlayerDirection();
        Direction.AirControll = 1;

        inputSystem = new InputSystem();


        iceRigidbody = ice.GetComponent<Rigidbody>();
        waterActor = water.GetComponent<ObiActor>();
        airRigidbody = air.GetComponent<Rigidbody>();

        inputSystem.Transformation.ToIce.performed += context => TransformaitionToIce();
        inputSystem.Transformation.ToAir.performed += context => TransformaitionToAir();

        inputSystem.Control.DoubleShift.performed += context => DoubleClickCheck(IceDoubleShiftDelay);
        inputSystem.Control.ShiftButton.performed += context => WaterShift();
    }

    private void Start()
    {
        InitializePlayerState();
    }


    private void Update()
    {
        Direction.Forward = inputSystem.Control.MoveVertical.ReadValue<float>();
        Direction.Lateral = inputSystem.Control.MoveGorizontal.ReadValue<float>();
        Direction.Up = inputSystem.Control.Jump.ReadValue<float>();
        Direction.Shift = inputSystem.Control.ShiftValue.ReadValue<float>();

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
        IceShift();
    }


    void Move()
    {
        switch (CurrentState)
        {
            case PlayerState.Water:
                Move(water);
                break;
            case PlayerState.Ice:
                //  if (playerSensor.CanJump)
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
                    Direction.AirControll = waterAirControl;

                    Jump(water);
                    Direction.AirControll = waterAirControl = 1;
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

    void IceShift()
    {
        if (CurrentState == PlayerState.Ice)
        {
            if (clickCount >= 2)
            {
                DoubleShift(ice);
                clickCount = 0;
            }
            else if (Direction.Shift > 0)
            {
                Shift(ice);
            }
        }
    }

    void WaterShift()
    {
        if (CurrentState == PlayerState.Water)
            Shift(water);
    }


    void UpdatePosition()
    {
        switch (CurrentState)
        {
            case PlayerState.Water:
                waterActor.GetMass(out com);
                transform.position = waterActor.solver.transform.TransformPoint(com);
                break;
            case PlayerState.Ice:
                transform.position = IceY_Rotator.transform.position = ice.transform.position;
                IceY_Rotator.transform.rotation = Quaternion.Euler(0, ice.transform.rotation.eulerAngles.y, 0);

                Debug.LogError(ice.transform.rotation.eulerAngles.y);


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
        try
        {
            waterActor.Teleport(transform.position, transform.rotation);
        }
        catch
        {
            Debug.LogError("TO DO");
        }

        transform.position = waterActor.solver.transform.TransformPoint(com);
        ;
    }

    private void TransformaitionToIce()
    {
        if (currentState == PlayerState.Ice) return;

        Debug.Log("TransformaitionToIce");

        WaterSolwer.gameObject.SetActive(false);
        air.gameObject.SetActive(false);

        CurrentState = PlayerState.Ice;

        iceRigidbody.velocity = new Vector3();
        ice.transform.position = transform.position;
        ice.gameObject.SetActive(true);
    }

    private void TransformaitionToAir()
    {
        if (currentState == PlayerState.Air) return;

        Debug.Log("TransformaitionToAire");
        WaterSolwer.gameObject.SetActive(false);
        ice.gameObject.SetActive(false);

        airRigidbody.velocity = new Vector3();
        CurrentState = PlayerState.Air;
        air.transform.position = transform.position;
        air.gameObject.SetActive(true);
    }

    public void TransformaitionToPreviousState()
    {
        Debug.LogError(previousState);
        switch (previousState)
        {
            case PlayerState.Water:
                TransformaitionToWater();
                break;
            case PlayerState.Ice:
                TransformaitionToIce();
                break;

            case PlayerState.Air:
                TransformaitionToAir();
                break;
        }
    }


    private void DoubleClickCheck(float time)
    {
        ResetClickCount(time);
        clickCount++;
    }

    private async void ResetClickCount(float time)
    {
        await Task.Delay(TimeSpan.FromSeconds(time));
        {
            clickCount = 0;
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