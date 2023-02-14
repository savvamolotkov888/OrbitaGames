using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ice : Player, IMove, IJump, IShift
{
    [SerializeField] private float MoveAcceleration;
    [SerializeField] private float RotationAcceleration;
    [SerializeField] private float JumpAcceleration;
    [SerializeField] private float ShiftAcceleration;
    private float shiftAcceleration = 1f;

    private Rigidbody iceRigidbody;

    public void Move(PlayerDirection direction, Player ice,
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
            iceRigidbody.AddTorque(0, RotationAcceleration * (float)rotationDirection * shiftAcceleration, 0);
        }

        if (rotationDirection == RotateDirection.DontRotate)
        {
            iceRigidbody.AddRelativeForce(direction.Lateral * MoveAcceleration, 0,
                direction.Forward * MoveAcceleration * shiftAcceleration);

            if (direction.Lateral == 1 && direction.Forward == 1)
            {
                //TODO поворот анимация
                Debug.Log("->");
                //   iceRigidbody.gameObject.transform.rotation = Quaternion.Euler(0,45, 0);
            }
        }
    }

    public void Jump(PlayerDirection direction, Player ice)
    {
        Debug.Log("IceJump");
        if (!iceRigidbody)
            iceRigidbody = ice.GetComponent<Rigidbody>();

        iceRigidbody.AddForce(0, JumpAcceleration, 0, ForceMode.Impulse);
    }

    public void Shift(PlayerDirection direction, Player ice, RotateDirection rotationDirection)
    {
        if (!iceRigidbody)
            iceRigidbody = ice.GetComponent<Rigidbody>();

        //  iceRigidbody.AddRelativeForce(direction.Lateral * MoveAcceleration, 0, 0);

        if ((direction.Forward == 1 || direction.Lateral == 0) && direction.Shift > 0)
            shiftAcceleration = ShiftAcceleration;

        else if (direction.Shift <= 0)
            shiftAcceleration = 1;
    }
}