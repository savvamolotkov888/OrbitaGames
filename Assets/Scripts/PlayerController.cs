using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{

    private Rigidbody WaterRigidbody;
    private Rigidbody iceRigidbody;
    private Rigidbody airRigidbody;
    private Rigidbody currentRigidbody;

    private Vector3 lastPosition;

    private InputSystem inputSystem;
    private PlayerState currentState = PlayerState.Water;
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
                    currentRigidbody = WaterRigidbody;
                    break;
                case PlayerState.Ice:
                    ice.transform.position = lastPosition;
                    ice.gameObject.SetActive(true);
                    currentRigidbody = iceRigidbody;
                    break;
                case PlayerState.Air:
                    air.transform.position = lastPosition;
                    air.gameObject.SetActive(true);
                    currentRigidbody = airRigidbody;
                    break;
                default:
                    break;
            }
        }
    }



    public Water water;
    public Ice ice;
    public Air air;

    private Vector2 moveDirection;

    public IJump jump;

    void Jump(IJump jump) => jump.Jump();
    void Move(IMove movable) => movable.Move(moveDirection, currentRigidbody);
    private void Awake()
    {
        WaterRigidbody = water.GetComponent<Rigidbody>();
        iceRigidbody = ice.GetComponent<Rigidbody>();
        airRigidbody = water.GetComponent<Rigidbody>();

        currentRigidbody = WaterRigidbody;

        inputSystem = new InputSystem();
        inputSystem.Control.Jump.performed += context => Jump();

        inputSystem.Transformation.ToWater.performed += context => TransformaitionToWater();
        inputSystem.Transformation.ToIce.performed += context => TransformaitionToIce();
        inputSystem.Transformation.ToAir.performed += context => TransformaitionToAir();
    }

    private void Update()
    {
        moveDirection = inputSystem.Control.Move.ReadValue<Vector2>();
        Move();

    }
    private void LateUpdate()
    {
        UpdatePosition();
    }

    void Move()
    {
        switch (currentState)
        {
            case PlayerState.Water:
                Move(water);
                break;
            case PlayerState.Ice:
                Move(ice);
                break;
            case PlayerState.Air:
                Move(air);
                break;
            default:
                break;
        }
    }
    void Jump()
    {
        switch (currentState)
        {
            case PlayerState.Water:
                Jump(water);
                break;
            case PlayerState.Ice:
                Jump(ice);
                break;
            case PlayerState.Air:
                Jump(air);
                break;
            default:
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
            default:
                break;
        }
        Debug.LogError(lastPosition);
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
public enum PlayerState { Water, Ice, Air }
