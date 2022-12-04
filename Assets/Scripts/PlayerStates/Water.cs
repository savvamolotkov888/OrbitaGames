using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour, IMove, IJump ,IStateToIce, IStateToAire
{
    public void Move(Vector2 moveVector,Rigidbody waterRigidbody , float acceleration)
    {
        Debug.Log("WaterMove" + moveVector);
        waterRigidbody.AddForce(moveVector.x * acceleration ,0, moveVector.y * acceleration);
    }
    public void Jump()
    {
        Debug.Log("WaterJump");
    }
    public void StateToAire()
    {

    }
    public void StateToIce()
    {

    }
}
