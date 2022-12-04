using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Air : MonoBehaviour, IMove, IJump , IStateToWater, IStateToIce 
{
    public void Move(Vector2 moveVector,Rigidbody airRigidbody,float acceleration)
    {
        Debug.Log("AirMove" + moveVector);
        airRigidbody.AddForce(moveVector.x * acceleration, 0, moveVector.y *acceleration);
    }
    public void Jump()
    {
        Debug.Log("AirJump");
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
