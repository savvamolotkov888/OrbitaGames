using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector3 position;
    public Vector3 rotation;

    public int health;
}

public enum PlayerState { Water, Ice, Ire }
