using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Air : MonoBehaviour, IMove, IJump , IStateToWater, IStateToIce 
{
    private Rigidbody airRigidbody;

    public void Move(PlayerDirection direction, GameObject air, float MoveAcceleration ,float RotationAcceleration ,RotateDirection rotationDirection )
    {
        if (!airRigidbody)
            airRigidbody = air.GetComponent<Rigidbody>();
        
        airRigidbody.AddRelativeForce(direction.Lateral * MoveAcceleration, 0, 0);
        
        if (direction.Forward!=1 && direction.Forward!= -1 )
        {
            return;
        }
      
            airRigidbody.AddRelativeForce(direction.Lateral * MoveAcceleration, 0, direction.Forward * MoveAcceleration);
        
    }
    public void Jump(GameObject air, float jumpAcceleration)
    {
        Debug.Log("AirJump");
        if (!airRigidbody)
            airRigidbody = air.GetComponent<Rigidbody>();
        airRigidbody.AddForce(0, jumpAcceleration, 0, ForceMode.Impulse);
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
