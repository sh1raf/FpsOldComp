using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(HorizontalLayoutGroup))]
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image heart;

    [Inject] private PlayerHealthLogic _playerHealth;

    private List<Image> _hearts = new();

    private int _currentHealth;

    private void Awake()
    {
        _playerHealth.OnHealthChange += OnValueChange;

        for(int i = 0; i < _playerHealth.MaxHealth; i++)
        {
            var instantiated = Instantiate(heart, transform);
            _hearts.Add(instantiated);
        }
        _currentHealth = _playerHealth.MaxHealth;
    }

    private void OnDisable() 
    {
        _playerHealth.OnHealthChange -= OnValueChange;
    }

    private void OnValueChange(int value)
    {
        if(value < _currentHealth)
        {
            for(int i = 0; i < _currentHealth - value; i++)
            {
                Destroy(_hearts[_hearts.Count - 1].gameObject);
                _hearts.RemoveAt(_hearts.Count - 1);
                Debug.Log(i);
            }
        }
        else if(value > _currentHealth)
        {
            for(int i = 0; i < value - _currentHealth; i++)
            {
                var instantiated = Instantiate(heart, transform);
                _hearts.Add(instantiated);
            }
        }

        _currentHealth = value;
    }
}
