using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class folower : MonoBehaviour
{
    public GameObject ggg;
    ObiSoftbody obi;
    
    
    private void Start()
    {
        obi = gameObject.GetComponent<ObiSoftbody>();
    }

    private void Update()
    {
        ggg.transform.position = obi.transform.position;
    }
}
