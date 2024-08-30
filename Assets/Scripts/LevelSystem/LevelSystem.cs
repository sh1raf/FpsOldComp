using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{
    [SerializeField] private List<Image> levels = new();

    [Inject] private SceneLoader _sceneLoader;
    

    private int _lastOpenedLevel;
    private int _currentLevelNumber;
    

    private void Awake()
    {
        for(int i = 0; i < levels.Count; i++)
        {
            if(PlayerPrefs.GetInt("LevelCompleted" + i) == 1 || PlayerPrefs.GetInt("LevelCompleted" + (i - 1)) == 1)
            {
                levels[i].color = new Color(levels[i].color.r, levels[i].color.g, levels[i].color.b, 1f);
                _lastOpenedLevel = i;
            }
        }
    }
    

    private void SceneAwake(Scene scene, Scene scene2)
    {
        for(int i = 0; i < levels.Count; i++)
        {
            if(scene2.name == "Level" + i)
            {
                _currentLevelNumber = i;
            }
        }
    }

    public void OpenLevel(int levelNumber)
    {
        if (PlayerPrefs.GetInt("LevelCompleted" + levelNumber) == 1 || PlayerPrefs.GetInt("LevelCompleted" + (levelNumber - 1)) == 1 || levelNumber - 1 <= -1)
            _sceneLoader.LoadScene($"Level {levelNumber}");
    }

    public void LoadLevelInfo(int levelNumber, bool levelCompleted, float levelRecord)
    {
        if(levelCompleted)
        {
            PlayerPrefs.SetInt("LevelCompleted" + levelNumber, 1);
            if(levelRecord != 0)
            {
                PlayerPrefs.SetFloat("LevelRecord" + levelNumber, levelRecord);
            }
        }
    }

    public void LevelExit()
    {
        
    }

    private void OnEnable() 
    {
        SceneManager.activeSceneChanged += SceneAwake;
    }

    private void OnDisable()
    {

    }

    private void MazeEnded()
    {

    }
}

