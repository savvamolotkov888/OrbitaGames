using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour, IMove, IJump 
{
    public void Move(Vector2 moveVector)
    {
        Debug.Log("WaterMove" + moveVector);
    }
    public void Jump()
    {
        Debug.Log("WaterJump");
    }
}
