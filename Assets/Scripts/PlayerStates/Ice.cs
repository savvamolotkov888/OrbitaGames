using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ice : MonoBehaviour, IMove, IJump, IStateToWater, IStateToAire
{
    public void Move(Vector2 moveVector)
    {
        Debug.Log("IceMove" + moveVector);
    }
    public void Jump()
    {
        Debug.Log("IceJump");
    }
    public void StateToAire()
    {

    }
    public void StateToWater()
    {

    }
}
