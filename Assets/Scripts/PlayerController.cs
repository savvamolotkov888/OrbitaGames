using Obi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float waterAcceleration;
    [SerializeField] private float iceAcceleration;
    [SerializeField] private float airAcceleration;

    [SerializeField] private float waterJumpBoost;
    [SerializeField] private float iceJumpBoost;
    [SerializeField] private float airJumpBoost;

    private ObiActor obiActor;

    private GameObject Water;
    private GameObject Ice;
    private GameObject Air;
    private GameObject currentGameobjectState;

    private float acceleration;
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

    private float MoveGorizontalDirection;
    

    public IJump jump;

    void Jump(IJump jump) => jump.Jump(currentGameobjectState,jumpBoost);
    void MoveForward(IMove movable) => movable.Move(new Vector2(), currentGameobjectState , acceleration);
    private void Awake()
    {
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
        MoveGorizontalDirection = inputSystem.Control.MoveVertical.ReadValue<float>();
        Debug.LogError(Input.GetAxis("Mouse X")*Time.deltaTime);
        if (MoveGorizontalDirection == 1)
            MoveForward();
        if (MoveGorizontalDirection == -1)
            MoveBack();
   
    }
    private void LateUpdate()
    {
        UpdatePosition();
    }

    void MoveForward()
    {
        Debug.Log("F");
        switch (currentState)
        {
            case PlayerState.Water:
                acceleration = waterAcceleration;
                MoveForward(water);
                break;
            case PlayerState.Ice:
                acceleration = iceAcceleration;
                MoveForward(ice);
                break;
            case PlayerState.Air:
                acceleration = airAcceleration;
                MoveForward(air);
                break;
        }
    }
    void MoveBack()
    {
        Debug.Log("B");
        switch (currentState)
        {
            case PlayerState.Water:
                acceleration = waterAcceleration;
                MoveForward(water);
                break;
            case PlayerState.Ice:
                acceleration = iceAcceleration;
                MoveForward(ice);
                break;
            case PlayerState.Air:
                acceleration = airAcceleration;
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
public enum PlayerState { Water, Ice, Air }
