using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;
using UnityEngine.EventSystems;

public class Water : Player, IMove, IJump, IStateToIce, IStateToAire
{
    [SerializeField] private float MoveAcceleration;
    [SerializeField] private float JumpAcceleration;

    public Transform TargetB;
    
    private ObiSoftbody obi;
    public void Move(PlayerDirection moveDirection, Player water , RotateDirection rotationDirection)
    {
        if (!obi)
            obi = water.GetComponent<ObiSoftbody>();
        
        obi.AddForce(new Vector3(moveDirection.Lateral *TargetB.position.x, 0, moveDirection.Forward*MoveAcceleration*TargetB.position.z), ForceMode.Force);
    }
    public void Jump(Player water)
    {
        if (!obi)
            obi = water.GetComponent<ObiSoftbody>();
        Debug.Log("WaterJump");
        obi.AddForce(new Vector3(0, JumpAcceleration,0), ForceMode.Force);
    }
    public void StateToAire()
    {

    }
    public void StateToIce()
    {

    }
}
