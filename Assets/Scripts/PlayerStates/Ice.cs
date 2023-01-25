using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ice : MonoBehaviour, IMove, IJump, IStateToWater, IStateToAire, IShift
{
    private Rigidbody iceRigidbody;

    public void Move(PlayerDirection direction, GameObject ice, float acceleration,Vector3 targetDirection , RotateDirection rotationDirection)
    {
        //TODO Поворот

        if (!iceRigidbody)
            iceRigidbody = ice.GetComponent<Rigidbody>();
        
      //  ice.transform.LookAt( targetDirection);
        iceRigidbody.AddRelativeForce(direction.Lateral * acceleration, 0, direction.Forward * acceleration);

        if (rotationDirection!= RotateDirection.DontRotate)
        {
            iceRigidbody.AddTorque(0,10 * (float)rotationDirection,0);
            Debug.Log(rotationDirection);

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