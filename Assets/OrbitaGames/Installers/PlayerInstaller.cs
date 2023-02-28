using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private PlayerInstanse player;
    [SerializeField] private HUD_Service _HUD_Service;

    public override void InstallBindings()
    {
        // var playerInstanse = Container.InstantiatePrefabForComponent<PlayerInstanse>(player.gameObject,
        //    new Vector3() , Quaternion.identity, null);

        Container.Bind<PlayerInstanse>().FromInstance(player).AsSingle();
        Container.Bind<HUD_Service>().FromInstance(_HUD_Service).AsSingle();
    }
}