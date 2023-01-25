using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ice : MonoBehaviour, IMove, IJump, IStateToWater, IStateToAire, IShift
{
    private Rigidbody iceRigidbody;

    public void Move(PlayerDirection direction, GameObject ice, float acceleration, RotateDirection rotationDirection)
    {
                
        if (!iceRigidbody)
            iceRigidbody = ice.GetComponent<Rigidbody>();
        
        if (direction.Forward!=1)
        {
            return;
        }
        if (rotationDirection!= RotateDirection.DontRotate)
        {
            iceRigidbody.AddTorque(0,40 * (float)rotationDirection,0);
        }

        if (rotationDirection == RotateDirection.DontRotate)
        {
            iceRigidbody.AddRelativeForce(direction.Lateral * acceleration, 0, direction.Forward * acceleration);
        }

       

        

        
        
    }

    public void Jump(GameObject ice, float acceleration)
    {
        Debug.Log("IceJump");

        if (!iceRigidbody)
            iceRigidbody = ice.GetComponent<Rigidbody>();
        
        iceRigidbody.AddForce(0, acceleration, 0, ForceMode.Impulse);
    }

    public void Shift(Rigidbody iceRigidbody, float acceleration)
    {
        Debug.Log("IceJump");
        iceRigidbody.AddForce(0, acceleration, 0, ForceMode.Impulse);
    }

    public void StateToAire()
    {
    }

    public void StateToWater()
    {
    }
}