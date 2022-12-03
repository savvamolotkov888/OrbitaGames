using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Air : IMove, IJump
{
    public void Move()
    {
        Debug.Log("AirMove");
    }
    public void Jump()
    {
        Debug.Log("AirJump");
    }
}
