using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ZombieHealthBar : MonoBehaviour
{
    private Slider _slider;

    private Camera _cam;
    private HealthLogic _health;
    private Canvas _canvas;

    private void OnEnable()
    {
        _health = GetComponentInParent<HealthLogic>();
        _canvas = GetComponentInParent<Canvas>();
        _slider = GetComponent<Slider>();

        _cam = Camera.main;
        _slider.maxValue = _health.MaxHealth;

        _health.OnHealthChange += OnValueChange;
    }
    private void OnDisable()
    {
        _health.OnHealthChange -= OnValueChange;
    }

    private void Update()
    {
        _canvas.transform.rotation = _cam.transform.rotation;
    }

    private void OnValueChange(int value)
    {
        if(value >= 0)
        {
            _slider.value = value;
        }

        if(value <= 0)
        {
            enabled = false;
        }
    }
}
