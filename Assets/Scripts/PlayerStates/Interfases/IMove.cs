using UnityEngine;
public interface IMove
{
    public void Move(Vector2 moveVector,Rigidbody rigidbody,float acceleration);
}
