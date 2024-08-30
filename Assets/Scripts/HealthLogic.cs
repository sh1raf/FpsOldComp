using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public abstract class HealthLogic : MonoBehaviour
{
    [SerializeField] protected int maxHealth;
    public int MaxHealth {get{ return maxHealth;} private set{}}

    protected int _currentHealth;
    public int CurrentHealth {get{return _currentHealth;} private set{}}

    public event Action<int> OnHealthChange;

    protected bool _isDead = false;
    public bool IsDead { get { return _isDead;} private set { } }

    protected virtual void OperationsInAwake()
    {
        _currentHealth = maxHealth;
        OnHealthChange?.Invoke(_currentHealth);
    }

    protected void HealthChange(int value)
    {
        OnHealthChange?.Invoke(value);
    }

    public virtual void TakeDamage(int damage)
    {
        if(damage < 0)
        {
            Debug.LogError("Damage can't be below 0");
            return;
        }

        if(damage >= _currentHealth && !_isDead)
        {
            _currentHealth = 0;
            _isDead = true;
            Die();
        }
        else
        {
            _currentHealth -= damage;
        }

        HealthChange(_currentHealth);
    }

    [Button]
    protected void HealOnTests()
    {
        _currentHealth++;
        HealthChange(_currentHealth);
    }
    [Button]
    protected void DamageOnTests()
    {
        _currentHealth--;
        HealthChange(_currentHealth);
    }

    protected virtual void Die()
    {
        
    }
}
