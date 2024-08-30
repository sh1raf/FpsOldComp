using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RestartPanelInstaller : MonoInstaller
{
    [SerializeField] private RestartPanel panel;

    public override void InstallBindings()
    {
        Container.Bind<RestartPanel>().FromInstance(panel).AsSingle().NonLazy();
    }
}
