using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class YandexSDK : MonoBehaviour
{
    public static YandexSDK Instance;

    [Inject] private VolumeController volume;

    [DllImport("__Internal")] private static extern void FullAdShow();
    [DllImport("__Internal")] private static extern void RewardedShow(int id);

    private void Awake()
    {
        Instance = this;
    }

    public void ShowFullscreenAd()
    {
        volume.MasterOff();
        FullAdShow();
    }

    public void ShowRewardedAd()
    {
        volume.MasterOff();
        RewardedShow(0);
    }

    public void CloseFullscreenAd()
    {
        volume.MasterOn();
    }

    public void CloseRewardedAd()
    {
        volume.MasterOn();
    }

    public void RewardRewardedAd()
    {
        CloseRewardedAd();
    }
}
