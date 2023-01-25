using UnityEngine;
public interface IMove
{
    public void Move(PlayerDirection direction,GameObject gameObject,float acceleration, Vector3 targetDirection);
}
