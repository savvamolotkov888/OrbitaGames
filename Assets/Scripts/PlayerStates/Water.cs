using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour, IMove, IJump ,IStateToIce, IStateToAire
{
    public void Move(Vector2 moveVector,Rigidbody waterRigidbody)
    {
        Debug.Log("WaterMove" + moveVector);
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
