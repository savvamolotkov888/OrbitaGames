using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private PlayerInstanse player;
    public override void InstallBindings()
    {
        // var playerInstanse = Container.InstantiatePrefabForComponent<PlayerInstanse>(player.gameObject,
        //    new Vector3() , Quaternion.identity, null);

        Container.Bind<PlayerInstanse>().FromInstance(player).AsSingle();

    }
}