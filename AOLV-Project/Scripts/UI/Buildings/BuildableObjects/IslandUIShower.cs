using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandUIShower : MonoBehaviour
{
    [SerializeField] private Resizer _resizer;
    [SerializeField] private Trigger<Player> _trigger;
    [SerializeField] private Vector3 _smallSize = new Vector3(0, 0, 0);

    private Vector3 _normalSize;

    private void Awake()
    {
        _normalSize = _resizer.transform.localScale;
        _resizer.transform.localScale = _smallSize;
    }

    private void OnEnable()
    {
        _trigger.Enter += OnPlayerEnter;
        _trigger.Exit += OnPlayerExit;
    }

    private void OnDisable()
    {
        _trigger.Enter -= OnPlayerEnter;
        _trigger.Exit -= OnPlayerExit;
    }

    private void OnPlayerEnter(Player player)
    {
        _resizer.Resize(1f, _normalSize);
    }

    private void OnPlayerExit(Player player)
    {
        _resizer.Resize(1f, _smallSize);
    }
}
