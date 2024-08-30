using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SceneVolume : MonoBehaviour
{
    [Inject] private VolumeController controller;

    [SerializeField] private Image sounds;
    [SerializeField] private Image music;

    [SerializeField] private Image soundsCross;
    [SerializeField] private Image musicCross;

    [SerializeField] private Sprite soundsOn;
    [SerializeField] private Sprite soundsOff;

    [SerializeField] private Sprite musicOn;
    [SerializeField] private Sprite musicOff;

    private  void Awake()
    {
        if (soundsCross && controller.SoundsOn)
            soundsCross.gameObject.SetActive(false);
        else if(soundsCross && !controller.SoundsOn)
            soundsCross.gameObject.SetActive(true);

        if (musicCross && controller.MusicOn)
            musicCross.gameObject.SetActive(false);
        else if(musicCross && !controller.MusicOn)
            musicCross.gameObject.SetActive(true);
    }

    private void Start()
    {
        StartCoroutine(StartDelay());
    }

    private IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(2);

        for (int i = 0; i < 2; i++)
        {
            MusicToggle();
            SoundsToggle();
        }
    }

    public void MusicToggle()
    {
        Debug.Log("Music");

        controller.MusicToggle();

        if(controller.MusicOn)
        {
            musicCross.gameObject.SetActive(false);
        }
        else
        {
            musicCross.gameObject.SetActive(true);
        }
    }

    public void SoundsToggle()
    {
        Debug.Log("Sounds");

        controller.SoundsToggle();

        if (controller.SoundsOn)
        {
            soundsCross.gameObject.SetActive(false);
        }
        else
        {
            soundsCross.gameObject.SetActive(true);
        }
    }

    public void MusicToggleSecond()
    {
        controller.MusicToggle();

        if (controller.MusicOn)
        {
            music.sprite = musicOn;
        }
        else
        {
            music.sprite = musicOff;
        }
    }

    public void SoundsToggleSecond()
    {
        controller.SoundsToggle();

        if (controller.SoundsOn)
        {
            sounds.sprite = soundsOn;
        }
        else
        {
            sounds.sprite = soundsOff;
        }
    }
}
