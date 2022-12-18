using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Air : MonoBehaviour, IMove, IJump , IStateToWater, IStateToIce 
{
    private Rigidbody airRigidbody;
    private bool setAirRigidbody;

    public void Move(Vector2 moveVector, GameObject air, float acceleration)
    {
        Debug.Log("AirMove" + moveVector);
        if (!setAirRigidbody)
        {
            airRigidbody = air.GetComponent<Rigidbody>();
            setAirRigidbody = true;
        }
        airRigidbody.AddForce(moveVector.x * acceleration, 0, moveVector.y * acceleration);
    }
    public void Jump(GameObject air, float acceleration)
    {
        Debug.Log("AirJump");
        if (!setAirRigidbody)
        {
            airRigidbody = air.GetComponent<Rigidbody>();
            setAirRigidbody = true;
        }
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
