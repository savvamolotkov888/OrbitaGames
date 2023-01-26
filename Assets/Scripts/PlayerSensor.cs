using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerSensor : MonoBehaviour
{
    public Transform Target;
    public LayerMask playerMask;
    public RotateDirection rorator;
    private float angle;
    public float rayDist;
    public Ice Ice;

    public bool OnTheFloar;

    void FixedUpdate()
    {
        Ray ray = new Ray(transform.position,-Vector3.up);
        RaycastHit hit; 
        
        if (Physics.Raycast(ray, out hit,rayDist))
        {
//            Debug.Log(hit.distance);
            
            if (hit.distance < 0.5)
                OnTheFloar = true;
            else
                OnTheFloar = false;


                ;
        }
        
            
        
        angle = -Vector3.SignedAngle(Target.position - Ice.transform.position, Ice.transform.forward, Vector3.up);
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