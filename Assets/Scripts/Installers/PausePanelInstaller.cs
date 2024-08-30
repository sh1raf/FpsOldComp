using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PausePanelInstaller : MonoInstaller
{
    [SerializeField] private PausePanel panel;

    public override void InstallBindings()
    {
        Container.Bind<PausePanel>().FromInstance(panel).AsSingle().NonLazy();
    }
}
