using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ice : MonoBehaviour, IMove, IJump, IStateToWater, IStateToAire, IShift
{
    private Rigidbody iceRigidbody;

    public void Move(float forwardMoveDirection, GameObject ice, float acceleration,Vector3 targetDirection)
    {

        if (!iceRigidbody)
            iceRigidbody = ice.GetComponent<Rigidbody>();
        
        iceRigidbody.transform.LookAt(iceRigidbody.position , targetDirection);
        
        iceRigidbody.AddRelativeForce(forwardMoveDirection * acceleration, 0, 0 * acceleration);
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