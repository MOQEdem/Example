using System.Collections;
using UnityEngine;

public class Healer : MonoBehaviour
{
    [SerializeField] private Unit _unit;
    [SerializeField] private float _healOverSecond;
    [SerializeField] private float _delayBeforHeal;

    private float _currentHealthValue;
    private float _newHealthValue;
    private float _timer;
    private Coroutine _healing;

    private void Start()
    {
        _unit.Health.HealthUpdated += OnHealthUpdated;
        _currentHealthValue = _unit.Health.CurrentHealth;
    }

    private void OnDisable()
    {
        _unit.Health.HealthUpdated -= OnHealthUpdated;
    }

    private void OnHealthUpdated()
    {
        _newHealthValue = _unit.Health.CurrentHealth;

        if (_unit.Health.CurrentHealth == _unit.Health.MaxHealth)
            return;

        if (_newHealthValue < _currentHealthValue)
        {
            _timer = _delayBeforHeal;

            if (_healing != null)
            {
                StopCoroutine(_healing);
                _healing = null;
            }
        }

        if (_healing == null)
            _healing = StartCoroutine(Healing());

        _currentHealthValue = _newHealthValue;
    }

    private IEnumerator Healing()
    {
        while (_timer > 0)
        {
            _timer -= Time.deltaTime;
            yield return null;
        }

        while (_unit.Health.CurrentHealth != _unit.Health.MaxHealth)
        {
            _unit.Health.IncreaseHealth(_healOverSecond * Time.deltaTime);

            yield return null;
        }
    }
}
