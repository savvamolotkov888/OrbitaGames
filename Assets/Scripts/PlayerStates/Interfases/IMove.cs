using UnityEngine;
public interface IMove
{
    public void Move(float forwardMoveDirection,GameObject gameObject,float acceleration, Vector3 targetDirection);
}
