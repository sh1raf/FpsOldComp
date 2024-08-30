using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Plugins.Audio.Core;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup master;
    
    public bool MusicOn { get { return _musicOn; } private set { } }
    public bool SoundsOn { get { return _soundsOn; } private set { } }


    private bool _musicOn = true;
    private bool _soundsOn = true;

    public void MusicToggle()
    {
        Debug.Log("MusicVolumeController");

        _musicOn = !_musicOn;

        if(_musicOn)
        {
            master.audioMixer.SetFloat("MusicVolume", 0f);
        }
        else
        {
            master.audioMixer.SetFloat("MusicVolume", -80f);
        }
    }

    public void MasterOff()
    {
        master.audioMixer.SetFloat("MasterVolume", -80f);
    }

    public void MasterOn()
    {
        master.audioMixer.SetFloat("MasterVolume", 0f);
    }

    public void SoundsToggle()
    {
        Debug.Log("SoundsVolumeController");

        _soundsOn = !_soundsOn;

        if (_soundsOn)
        {
            master.audioMixer.SetFloat("SoundsVolume", 0f);
        }
        else
        {
            master.audioMixer.SetFloat("SoundsVolume", -80f);
        }
    }
}
