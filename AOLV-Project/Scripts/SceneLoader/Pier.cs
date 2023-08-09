using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Pier : MonoBehaviour
{
    [SerializeField] private bool _isHubPier;
    [SerializeField] private Trigger<Player> _trigger;

    public event UnityAction<bool> PlayerEnter;
    public event UnityAction<bool> PlayerExit;

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
        PlayerEnter?.Invoke(_isHubPier);
    }

    private void OnPlayerExit(Player player)
    {
        PlayerExit?.Invoke(_isHubPier);
    }
}