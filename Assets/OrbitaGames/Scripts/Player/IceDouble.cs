using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class IceDouble : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform TargetVertical;
    [SerializeField] private float DebugLength;

    [SerializeField] private Transform X;
    [SerializeField] private Transform Xm;
    [SerializeField] private Transform Y;
    [SerializeField] private Transform Ym;
    [SerializeField] private Transform Z;
    [SerializeField] private Transform Zm;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = player.rotation;

        Debug.DrawRay(transform.position, X.position - transform.position, Color.white);
        Debug.DrawRay(transform.position, Xm.position - transform.position, Color.red);
        Debug.DrawRay(transform.position, Y.position - transform.position, Color.green);
        Debug.DrawRay(transform.position, Ym.position - transform.position, Color.green);
        Debug.DrawRay(transform.position, Z.position - transform.position, Color.blue);
        Debug.DrawRay(transform.position, Zm.position - transform.position, Color.blue);
    }
}