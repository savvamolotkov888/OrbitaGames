using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Air : MonoBehaviour, IMove, IJump , IStateToWater, IStateToIce 
{
    private Rigidbody airRigidbody;

    public void Move(PlayerDirection direction, GameObject air, float acceleration , Vector3 targetDirection ,RotateDirection rotationDirection )
    {
        if (!airRigidbody)
            airRigidbody = air.GetComponent<Rigidbody>();
       // airRigidbody.AddForce(moveVector.x * acceleration, 0, moveVector.y * acceleration);
    }
    public void Jump(GameObject air, float acceleration)
    {
        Debug.Log("AirJump");
        if (!airRigidbody)
            airRigidbody = air.GetComponent<Rigidbody>();
        airRigidbody.AddForce(0, acceleration, 0, ForceMode.Impulse);
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
