using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour, IMove, IJump 
{
    public void Move()
    {
        Debug.Log("WaterMove");
    }
    public void Jump()
    {
        Debug.Log("WaterJump");
    }
}
