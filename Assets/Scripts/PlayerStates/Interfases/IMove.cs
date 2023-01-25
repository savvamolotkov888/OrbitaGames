using UnityEngine;
public interface IMove
{
    public void Move(PlayerDirection direction,GameObject gameObject,float acceleration ,RotateDirection rotationDirection );
}
