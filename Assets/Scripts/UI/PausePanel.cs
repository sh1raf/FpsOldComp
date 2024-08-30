using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{

    [SerializeField] private GameObject panel;

    [Inject] SceneLoader _loader;
    [Inject] private PlayerInput _input;
    [Inject] private Player _player;

    private void Awake()
    {
        _input.Player.Pause.performed += context => Enable();

        panel.gameObject.SetActive(false);
    }

    public void Enable()
    {
        if(_player.GetComponent<PlayerHealthLogic>().IsDead || FindObjectOfType<Finish>().IsFinished)
            return;
        if (panel.activeInHierarchy)
            Disable();

        _player.Pause();

        panel.SetActive(true);
        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
    }

    public void Disable()
    {
        _player.Unpause();

        panel.SetActive(false);
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Restart()
    {
        panel.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;

        _loader.LoadScene(1);
    }

    public void GoMainMenu()
    {
        panel.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;

        _loader.LoadScene(0);
    }
}
