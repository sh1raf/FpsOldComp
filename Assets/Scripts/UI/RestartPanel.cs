using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RestartPanel : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [Inject] private SceneLoader loader;

    public void Enable()
    {
        panel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        loader.LoadScene(1);
        Time.timeScale = 1f;
    }

    public void GoMainMenu()
    {
        loader.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void NextLevel(int level)
    {
        loader.LoadScene($"Level {level}");
        Time.timeScale = 1f;
    }
}
