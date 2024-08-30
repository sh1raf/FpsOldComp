using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ShopInstaller : MonoInstaller
{
    [SerializeField] private Shop shop;

    public override void InstallBindings()
    {
        Container.Bind<Shop>().FromInstance(shop).AsSingle().NonLazy();
    }
}
