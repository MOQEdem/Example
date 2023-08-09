using System;
using UnityEngine;

public class BotStateMachine : MonoBehaviour
{
    [SerializeField] private State _firstState;

    private State _currentState;
    private Unit _unit;

    private void Awake()
    {
        if (_firstState == null)
            throw new NullReferenceException();
        _unit = GetComponent<Unit>();
    }

    private void Start()
    {
        _currentState = _firstState;
        _currentState.EnterState();
    }

    private void OnEnable()
    {
        _unit.Died += OnDied;
    }

    private void OnDisable()
    {
        _unit.Died -= OnDied;
    }

    public void Activate()
    {
        enabled = true;
    }

    private void Update()
    {
        State nextState = _currentState.TryGetNextState();
        if (nextState != null)
        {
            Transit(nextState);
        }
    }

    private void Transit(State nextState)
    {
        _currentState.Exit();
        _currentState = nextState;
        _currentState.EnterState();
    }

    private void OnDied(Unit unit)
    {
        _currentState.Exit();
        _currentState = null;
        enabled = false;
    }
}
