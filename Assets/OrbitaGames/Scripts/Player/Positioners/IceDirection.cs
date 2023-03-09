using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceDirection : MonoBehaviour
{
    [SerializeField] private Transform ice;

    // Update is called once per frame
    void Update()
    {
        transform.position = ice.transform.position;
        transform.rotation =
            Quaternion.Euler(0, ice.transform.rotation.eulerAngles.y, 0);

        // if (transform.rotation.eulerAngles.y < 180 &&
        //     transform.rotation.eulerAngles.y > 179 ||
        //     transform.rotation.eulerAngles.y > -180 &&
        //     transform.rotation.eulerAngles.y < -179
        //    )
        //     Debug.LogError("BAG1!");
        //
        // else if (transform.rotation.eulerAngles.y < 1 &&
        //          transform.rotation.eulerAngles.y > 0)
        //     Debug.LogError("BAG2!");
        
        //Чекать 
    }
}