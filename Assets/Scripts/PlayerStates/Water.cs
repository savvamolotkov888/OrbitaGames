using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour, IMove, IJump, IChangeState
{
    public void Move()
    {
        Debug.Log("WaterMove");
    }
    public void Jump()
    {
        Debug.Log("WaterJump");
    }
    public void ChangeState()
    {
        Debug.Log("WaterJump");
    }

}
