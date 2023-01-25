using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDirection : MonoBehaviour
{
    public Transform player;
    public Transform camera;

    
    
    // Update is called once per frame
    void Update()
    {
        transform.position = player.position - new Vector3(camera.position.x,0,camera.position.z);
    }
}
