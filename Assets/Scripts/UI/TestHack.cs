using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class TestHack : MonoBehaviour
{
    [Button]
    private void AddGoals()
    {
        PlayerPrefs.SetInt("Goals", PlayerPrefs.GetInt("Goals") + 500);

    }

    [Button]
    private void Reset()
    {
        PlayerPrefs.SetInt("Goals", 0);
    }
}
