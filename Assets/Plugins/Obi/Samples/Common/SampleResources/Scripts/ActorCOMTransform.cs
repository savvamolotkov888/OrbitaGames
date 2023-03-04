using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;
/// <summary>
/// За этим следить
/// </summary>

public class ActorCOMTransform : MonoBehaviour
{
   
    public Vector3 offset;
    public ObiActor actor;

    public void Update()
    {
        if (actor != null && actor.isLoaded)
        {
            Vector3 com;
            actor.GetMass(out com);
            transform.position = actor.solver.transform.TransformPoint(com) + offset;
        }
    }
}
