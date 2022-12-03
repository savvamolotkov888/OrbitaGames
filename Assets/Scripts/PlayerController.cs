using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    private InputSystem inputSystem;
    public PlayerState currentState = PlayerState.Water;

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
        Move(moveDirection);

    }
    void Move(Vector2 moveDirection)
    {
        switch (currentState)
        {
            case PlayerState.Water:
                Move(water);
                break;
            case PlayerState.Ice:
                Move(ice);
                break;
            case PlayerState.Aire:
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
            case PlayerState.Aire:
                Jump(air);
                break;
            default:
                break;
        }
    }

    void TransformaitionToWater()
    {
        Debug.Log("TransformaitionToWater");
        currentState = PlayerState.Water;
    }

    void TransformaitionToIce()
    {
        Debug.Log("TransformaitionToIce");
        currentState = PlayerState.Ice;
    }
    void TransformaitionToAire()
    {
        Debug.Log("TransformaitionToAire");
        currentState = PlayerState.Aire;
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
public enum PlayerState { Water, Ice, Aire }
