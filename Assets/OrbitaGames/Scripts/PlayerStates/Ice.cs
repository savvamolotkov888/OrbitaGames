using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ice : MonoBehaviour, IMove, IJump, IStateToWater, IStateToAire, IShift
{
    private Rigidbody iceRigidbody;

    [Header("Jump stats")] [SerializeField]
    private float maxJumpTime;

    [SerializeField] private float maxJumpHeight;

    public void Move(PlayerDirection direction, GameObject ice, float MoveAcceleration, float RotationAcceleration,
        RotateDirection rotationDirection)
    {
        if (!iceRigidbody)
            iceRigidbody = ice.GetComponent<Rigidbody>();


        iceRigidbody.AddRelativeForce(direction.Lateral * MoveAcceleration, 0, 0);

        if (direction.Forward != 1 && direction.Forward != -1)
        {
            return;
        }

        if (rotationDirection != RotateDirection.DontRotate)
        {
            iceRigidbody.AddTorque(0, RotationAcceleration * (float)rotationDirection, 0);
        }

        if (rotationDirection == RotateDirection.DontRotate)
        {
            iceRigidbody.AddRelativeForce(direction.Lateral * MoveAcceleration, 0,
                direction.Forward * MoveAcceleration);

            if (direction.Lateral == 1 && direction.Forward == 1)
            {
                Debug.Log("->");
                //   iceRigidbody.gameObject.transform.rotation = Quaternion.Euler(0,45, 0);
            }
        }
    }

    public void Jump(GameObject ice, float jumpAcceleration)
    {
        Debug.Log("IceJump");
        if (!iceRigidbody)
            iceRigidbody = ice.GetComponent<Rigidbody>();

        iceRigidbody.AddForce(0, jumpAcceleration, 0, ForceMode.Impulse);
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