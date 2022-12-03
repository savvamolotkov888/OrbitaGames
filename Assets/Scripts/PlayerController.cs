using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    private InputSystem inputSystem;
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
                    water.gameObject.SetActive(true);
                    break;
                case PlayerState.Ice:
                    ice.gameObject.SetActive(true);
                    break;
                case PlayerState.Air:
                    air.gameObject.SetActive(true);
                    break;
                default:
                    break;
            }


        }
    }
    private PlayerState currentState = PlayerState.Water;
    public Water water;
    public Ice ice;
    public Air air;

    private Vector2 moveDirection;

    public IJump jump;

    void Jump(IJump jump) => jump.Jump();
    void Move(IMove movable) => movable.Move(moveDirection);
    private void Awake()
    {
        inputSystem = new InputSystem();
        inputSystem.Control.Jump.performed += context => Jump();

        inputSystem.Transformation.ToWater.performed += context => TransformaitionToWater();
        inputSystem.Transformation.ToIce.performed += context => TransformaitionToIce();
        inputSystem.Transformation.ToAir.performed += context => TransformaitionToAire();
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
                Debug.Log(PlayerData.position);
                break;
            case PlayerState.Ice:
                Debug.Log(PlayerData.position);

                break;
            case PlayerState.Air:
                Debug.Log(PlayerData.position);

                break;
            default:
                break;
        }
    }

    void TransformaitionToWater()
    {
        Debug.Log("TransformaitionToWater");
        CurrentState = PlayerState.Water;
    }

    void TransformaitionToIce()
    {
        Debug.Log("TransformaitionToIce");
        CurrentState = PlayerState.Ice;
    }
    void TransformaitionToAire()
    {
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
