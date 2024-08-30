using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Zenject;

public class WaultUI : MonoBehaviour
{
    [SerializeField] private TMP_Text tmp;
    [Inject] Shop _shop;

    private void Awake()
    {
        WaultChange();
        _shop.OnWaultChange += WaultChange;
    }

    private void OnDisable()
    {
        _shop.OnWaultChange -= WaultChange;
    }

    public void WaultChange()
    {
        tmp.text = PlayerPrefs.GetInt("Goals").ToString();
    }
}
