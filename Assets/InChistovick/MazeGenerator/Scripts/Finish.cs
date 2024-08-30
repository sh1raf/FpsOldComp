using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Collider))]
public class Finish : MonoBehaviour
{
    [SerializeField] private string sceneNumber;

    public bool IsFinished { get; private set; }
    public event Action MazeComplete;
    private void OnTriggerEnter(Collider other) 
    {
        if (other.GetComponent<Player>())
        {
            IsFinished = true;
            Debug.Log("Completed");
            MazeComplete?.Invoke();
            FindObjectOfType<Player>().Pause();
            Cursor.lockState = CursorLockMode.None;

            Time.timeScale = 0f;
            PlayerPrefs.SetInt("LevelCompleted" + sceneNumber, 1);
        }
    }
}
