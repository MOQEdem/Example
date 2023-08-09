using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Squad : MonoBehaviour
{
    private List<NPC> _units;

    public List<NPC> Units => _units;

    public event Action<Squad> SquadEmpty;

    protected virtual void Awake()
    {
        _units = GetComponentsInChildren<NPC>(true).ToList();
    }

    private void OnEnable()
    {
        foreach (var unit in _units)
            unit.Died += OnDied;
    }

    private void OnDisable()
    {
        foreach (var unit in _units)
            unit.Died -= OnDied;
    }

    private void Start()
    {
        foreach (var unit in _units)
        {
            unit.SetTarget(null);
        }
    }

    public void AddUnit(NPC unit)
    {
        _units.Add(unit);
        unit.Died += OnDied;
    }

    public void RemoveUnit(NPC unit)
    {
        unit.Died -= OnDied;
        _units.Remove(unit);

        if (_units.Count == 0)
        {
            SquadEmpty?.Invoke(this);
        }
    }

    private void OnDied(Character unit)
    {
        if (unit is NPC npc)
            RemoveUnit(npc);
    }

    public void ClearSquad()
    {
        while (_units.Count > 0)
        {
            NPC npc = _units[0];
            RemoveUnit(npc);
            Destroy(npc.gameObject);
        }

        _units.Clear();

        SquadEmpty?.Invoke(this);
    }
}
