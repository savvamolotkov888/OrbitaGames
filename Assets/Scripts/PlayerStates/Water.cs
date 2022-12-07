using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class Water : MonoBehaviour, IMove, IJump, IStateToIce, IStateToAire
{
    private ObiSoftbody obi;
    private bool setWaterObi;

    public void Move(Vector2 moveVector, GameObject water, float acceleration)
    {
        if (!setWaterObi)
        {
            obi = water.GetComponent<ObiSoftbody>();
            setWaterObi = true;
        }
        Debug.Log("WaterMove" + moveVector);
        obi.AddForce(new Vector3(moveVector.x * acceleration, 0, moveVector.y * acceleration), ForceMode.VelocityChange);
    }
    public void Jump(GameObject water, float acceleration)
    {
        if (!setWaterObi)
        {
            obi = water.GetComponent<ObiSoftbody>();
            setWaterObi = true;
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
