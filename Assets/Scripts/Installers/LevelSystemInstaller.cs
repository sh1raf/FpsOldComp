using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelSystemInstaller : MonoInstaller
{
    [SerializeField] private LevelSystem levelSystem;
    public override void InstallBindings()
    {
        Container.Bind<LevelSystem>().FromInstance(levelSystem).AsSingle().NonLazy();
    }
}
