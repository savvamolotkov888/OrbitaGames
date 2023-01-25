using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class Water : MonoBehaviour, IMove, IJump, IStateToIce, IStateToAire
{
    private ObiSoftbody obi;

    public void Move(float forwardMoveDirection, GameObject water, float acceleration,Vector3 targetDirection)
    {
        if (!obi)
            obi = water.GetComponent<ObiSoftbody>();
        
     //   obi.AddForce(new Vector3(moveVector.x * acceleration, 0, moveVector.y * acceleration), ForceMode.VelocityChange);
    }
    public void Jump(GameObject water, float acceleration)
    {
        if (!obi)
        {
            obi = water.GetComponent<ObiSoftbody>();
        }
        Debug.Log("WaterJump");
        obi.AddForce(new Vector3(0, acceleration,0), ForceMode.VelocityChange);
    }
    public void StateToAire()
    {

    }
    public void StateToIce()
    {

    }
}
