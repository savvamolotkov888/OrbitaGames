using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ice : MonoBehaviour, IMove, IJump, IStateToWater, IStateToAire ,IShift
{
    public void Move(Vector2 moveVector , Rigidbody iceRigidbody , float acceleration)
    {
        Debug.Log("IceMove" + moveVector);
        iceRigidbody.AddForce(moveVector.x * acceleration, 0, moveVector.y * acceleration);
    }
    public void Jump(Rigidbody iceRigidbody, float acceleration)
    {
        Debug.Log("IceJump");
        iceRigidbody.AddForce(0, acceleration, 0, ForceMode.Impulse);
    }
    public void Shift(Rigidbody iceRigidbody, float acceleration)
    {
        Debug.Log("IceJump");
        iceRigidbody.AddForce(0, acceleration, 0, ForceMode.Impulse);
    }
    public void StateToAire()
    {

    }
    public void StateToWater()
    {

    }
}
