using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Air : MonoBehaviour, IMove, IJump , IStateToWater, IStateToIce 
{
    public void Move(Vector2 moveVector)
    {
        Debug.Log("AirMove" + moveVector);
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
