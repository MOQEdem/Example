using System;
using UnityEngine;

public class Health
{
    private int _maxHealth;
    private int _currentHealth;
    private int _minHealth = 0;

    public int MaxHealth => _maxHealth;
    public int CurrentHealth => _currentHealth;
    public float HealthFullness => (float)_currentHealth / _maxHealth;

    public event Action HealthLost;
    public event Action HealthUpdated;

    public Health(int maxHealth)
    {
        _maxHealth = maxHealth;
        _currentHealth = maxHealth;
    }

    public void ApplyDamage(int damage)
    {
        if (_currentHealth <= _minHealth)
            return;

        _currentHealth = Mathf.Clamp(_currentHealth - damage, _minHealth, _maxHealth);

        HealthUpdated?.Invoke();

        if (_currentHealth == _minHealth)
            HealthLost?.Invoke();
    }

    public void IncreaseHealth(int health)
    {
        if (_currentHealth < _maxHealth)
        {
            _currentHealth = Mathf.Clamp(_currentHealth + health, _minHealth, _maxHealth);
            HealthUpdated?.Invoke();
        }
    }

    public void ChangeMaxValue(int additionalHealth)
    {
        _maxHealth += additionalHealth;
        _currentHealth = Mathf.Clamp(_currentHealth + additionalHealth, _minHealth + 1, _maxHealth);
    }

    public void SetNewHealthMaxValue(int newValue)
    {
        _maxHealth = newValue;
        _currentHealth = newValue;
    }
}
