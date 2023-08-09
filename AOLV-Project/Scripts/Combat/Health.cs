using UnityEngine;
using UnityEngine.Events;

public class Health
{
    private float _maxHealth;
    private float _currentHealth;
    private float _minHealth = 0;
    private float _lastAppliedDamage = 0;

    public float MaxHealth => _maxHealth;
    public float CurrentHealth => _currentHealth;
    public float HealthFill => _currentHealth / _maxHealth;
    public float LastAppliedDamage => _lastAppliedDamage;

    public event UnityAction HealthLost;
    public event UnityAction HealthUpdated;

    public Health(float maxHealth)
    {
        _maxHealth = maxHealth;
        _currentHealth = maxHealth;
    }

    public void ApplyDamage(float damage)
    {
        _lastAppliedDamage = 0;
        if (_currentHealth <= _minHealth)
            return;
        float lastHealhValue = _currentHealth;
        _currentHealth = Mathf.Clamp(_currentHealth - damage, _minHealth, _maxHealth);
        _lastAppliedDamage = lastHealhValue - _currentHealth;
        HealthUpdated?.Invoke();

        if (_currentHealth == _minHealth)
            HealthLost?.Invoke();
    }

    public void IncreaseHealth(float health)
    {
        _currentHealth = Mathf.Clamp(_currentHealth + health, _minHealth, _maxHealth);
        HealthUpdated?.Invoke();
    }
}
