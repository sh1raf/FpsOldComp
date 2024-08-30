using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SceneLoaderInstaller : MonoInstaller
{
    [SerializeField] private SceneLoader loader;

    public override void InstallBindings()
    {
        Container.Bind<SceneLoader>().FromInstance(loader).AsSingle().NonLazy();
    }
}
