using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DescriptionInstaller : MonoInstaller
{
    [SerializeField] private Description description;

    public override void InstallBindings()
    {
        Container.Bind<Description>().FromInstance(description).AsSingle().NonLazy();
    }
}
