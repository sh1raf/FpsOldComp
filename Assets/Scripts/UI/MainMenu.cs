using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class MainMenu : MonoBehaviour
{
    [Inject] private SceneLoader loader;
    [Inject] private LevelSystem levelSystem;
    [Inject] private Shop shop;

    private YandexSDK _yaSDK;

    private void Awake()
    {
        _yaSDK = FindObjectOfType<YandexSDK>();

        levelSystem.gameObject.SetActive(false);
        shop.transform.localScale = Vector3.zero;
    }

    private void ShowFullScreenAd()
    {
        _yaSDK.ShowFullscreenAd();
    }

    public void OpenLevelsSystem()
    {
        levelSystem.gameObject.SetActive(true);
    }
    public void CloseLevelsSystem()
    {
        levelSystem.gameObject.SetActive(false);
    }

    public void OpenShop()
    {
        shop.transform.localScale = Vector3.one;
    }
    public void CloseShop()
    {
        shop.transform.localScale = Vector3.zero;
    }

    public void StartRandomMaze()
    {
        //ShowFullScreenAd();
        loader.LoadScene(1);
    }
}
