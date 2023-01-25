using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerRotationController : MonoBehaviour
{
    public Transform Target;

    public RotateDirection rorator;
    private float angle;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward * 1000f, Color.blue);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag == "Target")
        {
            rorator = RotateDirection.DontRotate;
        }


        angle = -Vector3.SignedAngle(Target.position - transform.position, transform.forward, Vector3.up);


        if (angle > 5 && angle < 180)
        {
            rorator = RotateDirection.Right;
        }
        else if (angle < -5 && angle > -180)
        {
            rorator = RotateDirection.Left;
        }
        else
        {
            rorator = RotateDirection.DontRotate;
        }
    }
}

public enum RotateDirection
{
    Left = -1,
    Right = 1,
    DontRotate = 0
}