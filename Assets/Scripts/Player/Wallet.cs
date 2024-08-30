using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Wallet : MonoBehaviour
{
    [SerializeField] private TMP_Text tmp;
    public event Action<int> CoinsCountChange;
    private int _currentCoinsCount;

    private void Awake()
    {
        tmp.text = "0";
    }

    public void AddCoin()
    {
        _currentCoinsCount+= 5;
        PlayerPrefs.SetInt("Goals", PlayerPrefs.GetInt("Goals") + 5);
        tmp.text = _currentCoinsCount.ToString();
    }

    public bool SpendCoins(int value)
    {
        if(value <= 0 || value > _currentCoinsCount)
            return false;

        _currentCoinsCount -= value;
        CoinsCountChange?.Invoke(_currentCoinsCount);
        
        return true;
    }
}
