using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class Water : MonoBehaviour, IMove, IJump, IStateToIce, IStateToAire
{
    [SerializeField] private float MoveAcceleration;
    [SerializeField] private float JumpAcceleration;
    
    private ObiSoftbody obi;
    public void Move(PlayerDirection forwardMoveDirection, GameObject water , RotateDirection rotationDirection)
    {
        if (!obi)
            obi = water.GetComponent<ObiSoftbody>();
        
        obi.AddForce(new Vector3(0, 0, 1*MoveAcceleration), ForceMode.VelocityChange);
    }
    public void Jump(GameObject water)
    {
        if (!obi)
            obi = water.GetComponent<ObiSoftbody>();
        Debug.Log("WaterJump");
        obi.AddForce(new Vector3(0, JumpAcceleration,0), ForceMode.VelocityChange);
    }
    public void StateToAire()
    {

    }
    public void StateToIce()
    {

    }
}
