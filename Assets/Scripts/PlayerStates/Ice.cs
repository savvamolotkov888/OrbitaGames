using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : IMove, IJump
{
    public void Move()
    {
        Debug.Log("IceMove");
    }
    public void Jump()
    {
        Debug.Log("IceJump");
    }
}
