using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private InputSystem inputSystem;
    public PlayerState currentState = PlayerState.Water;

    public Water water;
    public Ice ice;
    public Air air;

    public IJump jump;

    void DoAction(IJump movable) => movable.Jump();
    private void Awake()
    {
        inputSystem = new InputSystem();
        inputSystem.Control.Jump.performed += context => Jump();
        inputSystem.Transformation.ToWater.performed += context => Debug.Log("SSS");


    }
    private void Update()
    {
        


    }
    void Jump()
    {
        switch (currentState)
        {
            case PlayerState.Water:
                DoAction(water);
                Debug.Log(11);
                break;
            case PlayerState.Ice:
                DoAction(ice);
                break;
            case PlayerState.Aire:
                DoAction(air);
                break;
            default:
                break;
        }

    }
}
public enum PlayerState { Water, Ice, Aire }
