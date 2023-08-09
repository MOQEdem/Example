using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private float _fillSpeed;
    [SerializeField] private Image _healthBarFilling;
    [SerializeField] private Unit _unit;

    private Camera _camera;
    private Coroutine _healthChanged;


    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Start()
    {
        _unit.Health.HealthUpdated += OnHealthChanged;
        OnHealthChanged();
    }

    private void OnDisable()
    {
        _unit.Health.HealthUpdated -= OnHealthChanged;
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + _camera.transform.rotation * Vector3.back, _camera.transform.rotation * Vector3.up);

    }

    private void OnHealthChanged()
    {
        if (_healthChanged != null)
        {
            StopCoroutine(_healthChanged);
        }
        _healthChanged = StartCoroutine(HealthChanged(_unit.Health.HealthFill));
    }

    private IEnumerator HealthChanged(float valueAsPercentage)
    {
        while (_healthBarFilling.fillAmount != valueAsPercentage)
        {
            _healthBarFilling.fillAmount = Mathf.MoveTowards(_healthBarFilling.fillAmount, valueAsPercentage, _fillSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
