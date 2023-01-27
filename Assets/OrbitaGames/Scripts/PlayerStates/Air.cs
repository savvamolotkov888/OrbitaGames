using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Air : Player , IMove, IJump , IStateToWater, IStateToIce 
{
    [SerializeField] private float MoveAcceleration;
    [SerializeField] private float FlyAcceleration;
    private Rigidbody airRigidbody;

    public void Move(PlayerDirection direction, Player air ,RotateDirection rotationDirection )
    {
        if (!airRigidbody)
            airRigidbody = air.GetComponent<Rigidbody>();
        
        airRigidbody.AddRelativeForce(direction.Lateral * MoveAcceleration, 0, direction.Forward*MoveAcceleration);
        
        if (direction.Forward!=1 && direction.Forward!= -1 )
        {
            return;
        }
      
            airRigidbody.AddRelativeForce(direction.Lateral * MoveAcceleration, 0, direction.Forward * MoveAcceleration);
        
    }
    public void Jump(Player air )
    {
        Debug.Log("AirJump");
        if (!airRigidbody)
            airRigidbody = air.GetComponent<Rigidbody>();
        
        airRigidbody.AddForce(0, FlyAcceleration, 0, ForceMode.Force);
    }
    public void Stet()
    {
        Debug.Log("AirJump");
    }
    public void StateToIce()
    {

    }
    public void StateToWater()
    {

    }
}
