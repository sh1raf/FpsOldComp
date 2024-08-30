using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class MainMenu : MonoBehaviour
{
    [Inject] private SceneLoader _loader;
    [Inject] private LevelSystem _levelSystem;
    [Inject] private Shop _shop;

    private YandexSDK _yaSDK;

    private void Awake()
    {
        _yaSDK = FindObjectOfType<YandexSDK>();

        _levelSystem.gameObject.SetActive(false);
        _shop.transform.localScale = Vector3.zero;
    }

    private void ShowFullScreenAd()
    {
        _yaSDK.ShowFullscreenAd();
    }

    public void OpenLevelsSystem()
    {
        _levelSystem.gameObject.SetActive(true);
    }
    public void CloseLevelsSystem()
    {
        _levelSystem.gameObject.SetActive(false);
    }

    public void OpenShop()
    {
        _shop.transform.localScale = Vector3.one;
    }
    public void CloseShop()
    {
        _shop.transform.localScale = Vector3.zero;
    }

    public void StartRandomMaze()
    {
        //ShowFullScreenAd();
        _loader.LoadScene(1);
    }
}
