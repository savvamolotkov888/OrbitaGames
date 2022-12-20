using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class folower : MonoBehaviour
{
    public GameObject ggg;
   Transform obi;
    
    
    private void Start()
    {
        obi = gameObject.GetComponent<ObiSoftbody>().transform;
    }

    private void Update()
    {
        ggg.transform.position = obi.position;
    }
}
