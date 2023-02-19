using System;
using UnityEngine;
using Zenject;

public class UIEventBus : MonoBehaviour
{
    private PlayerInstanse player;
    private PlayerController playerController;

    [Inject]
    private void Construct(PlayerInstanse player)
    {
        this.player = player;
    }

    private void Start()
    {
        Debug.Log("S");
        playerController = player.GetComponentInChildren<PlayerController>();
        playerController.ToWater += Water;
        playerController.ToIce += Ice;
        playerController.ToAir += Air;
    }

    void Water() => Debug.Log("ToWaterHUD");
    void Ice() => Debug.Log("ToIceHUD");
    void Air() => Debug.Log("ToAirHUD");
}