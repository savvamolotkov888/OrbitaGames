using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ice : MonoBehaviour, IMove, IJump, IStateToWater, IStateToAire, IShift
{
    private bool setIceRigidbody;
    private Rigidbody iceRigidbody;
    public void Move(Vector2 moveVector, GameObject ice, float acceleration)
    {
        Debug.Log("IceMove" + moveVector);

        if (!setIceRigidbody)
        {
            iceRigidbody = ice.GetComponent<Rigidbody>();
            setIceRigidbody = true;
        }
        iceRigidbody.AddForce(moveVector.x * acceleration, 0, moveVector.y * acceleration);
    }
    public void Jump(GameObject ice, float acceleration)
    {
        Debug.Log("IceJump");

        if (!setIceRigidbody)
        {
            iceRigidbody = ice.GetComponent<Rigidbody>();
            setIceRigidbody = true;
        }
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
