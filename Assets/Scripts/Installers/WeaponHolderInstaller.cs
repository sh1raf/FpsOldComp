using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WeaponHolderInstaller : MonoInstaller
{
    [SerializeField] private WeaponHolder weaponHolder;
    public override void InstallBindings()
    {
        Container.Bind<WeaponHolder>().FromInstance(weaponHolder).AsSingle().NonLazy();
    }
}
