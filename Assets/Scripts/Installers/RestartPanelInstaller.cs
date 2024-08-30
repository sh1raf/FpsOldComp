using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RestartPanelInstaller : MonoInstaller
{
    [SerializeField] private RestartPanel _panel;

    public override void InstallBindings()
    {
        Container.Bind<RestartPanel>().FromInstance(_panel).AsSingle().NonLazy();
    }
}
