using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Plugins.Audio.Core;

public class Music : MonoBehaviour
{
    private void Awake()
    {
        if(FindObjectsOfType<Music>().Length == 1)
        {
            DontDestroyOnLoad(gameObject);
            GetComponent<SourceAudio>().Play("MainMusic");  
        }
    }
}
