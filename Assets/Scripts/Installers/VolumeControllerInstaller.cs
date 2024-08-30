using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class VolumeControllerInstaller : MonoInstaller
{
    [SerializeField] private VolumeController volumeController;
    public override void InstallBindings()
    {
        Container.Bind<VolumeController>().FromInstance(volumeController).AsSingle();
    }
}
