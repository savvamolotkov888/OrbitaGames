using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Debuger : MonoBehaviour
{
    private PlayerInstanse player;
    
    [Inject]
  private void Construct(PlayerInstanse player)
  {
      this.player = player;
  }
    void Update()
    {
        Debug.Log(player.gameObject.transform.position);
    }
}
