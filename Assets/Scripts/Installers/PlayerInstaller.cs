using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private Player player;
    public override void InstallBindings()
    {
        PlayerInput input = new();
        input.Enable();
        Container.Bind<PlayerInput>().FromInstance(input).AsSingle();

        Container.Bind<Player>().FromInstance(player).AsSingle();
        Container.Bind<PlayerHealthLogic>().FromInstance(player.GetComponent<PlayerHealthLogic>()).AsSingle();
    }
}
