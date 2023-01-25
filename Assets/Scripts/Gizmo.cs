using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gizmo : MonoBehaviour
{
    public Transform player;
    public Transform taget;

    private Ray _ray;
    
    
    // Update is called once per frame
    void Update()
    {
        taget.position = player.position - new Vector3(transform.position.x,0,transform.position.z);
    }
}
