using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSquad : Squad
{
    [SerializeField] private BattleStarter _battleStarter;
    [SerializeField] private Player _player;

    private List<NPC> _waitingUnits = new List<NPC>();
    private UpgradeZone _upgradeZone;
    private EnemyArmyObserver _observer;
    private float _targetPositionRandomizerRadius = 4f;
    private BaseExtender _baseExtender;

    public List<NPC> WaitingUnits => _waitingUnits;
    private int _numberOfChoosenNPC = 8;

    public event Action<Dictionary<PlayerSquadZoneType, int>> NewCapacitySet;
    public event Action<int> NewLevelSet;
    public event Action BattleStarted;
    public event Action BattleEnded;

    protected override void Awake()
    {
        base.Awake();
        _observer = GetComponent<EnemyArmyObserver>();
        _upgradeZone = GetComponentInChildren<UpgradeZone>();
        _baseExtender = GetComponentInParent<BaseExtender>();
        _battleStarter.EnablePointer();
    }

    private void OnEnable()
    {
        _battleStarter.BattleStarted += OnBattleStarted;
        _observer.TargetUpdated += OnTargetUpdated;
        _observer.EnemyArmyDefeted += OnEnemyArmyDefeted;
        _upgradeZone.LevelSet += OnLevelSet;
        _baseExtender.BaseMoved += OnBaseMoved;
    }

    private void OnDisable()
    {
        _battleStarter.BattleStarted -= OnBattleStarted;
        _observer.TargetUpdated -= OnTargetUpdated;
        _upgradeZone.LevelSet -= OnLevelSet;
        _observer.EnemyArmyDefeted -= OnEnemyArmyDefeted;
        _baseExtender.BaseMoved -= OnBaseMoved;
    }

    public void AddWaitingUnit(NPC unit)
    {
        _waitingUnits.Add(unit);
    }
    public void TeleportWaitingSquad()
    {
        foreach (NPC unit in _waitingUnits)
            if (unit is AlliedNPC allied)
                allied.TeleportToStartPoint();
    }

    private void OnBattleStarted()
    {
        foreach (var unit in _waitingUnits)
            AddUnit(unit);

        _waitingUnits.Clear();

        BattleStarted?.Invoke();
        SetUnitTargetPosition();
    }

    private void OnEnemyArmyDefeted(Transform target)
    {
        int currentCount = _numberOfChoosenNPC;

        BattleEnded?.Invoke();

        foreach (var unit in Units)
        {

            Vector3 targetOffset = UnityEngine.Random.insideUnitSphere * 4;
            Vector3 targetPosition = target.position + targetOffset + Vector3.back * 4;
            targetPosition.y = 0;

            if (unit is NPCWalker walker)
            {
                if (currentCount > 0)
                {
                    if (walker is AlliedNPC npc && !npc.IsHeavy)
                    {
                        walker.SetTargetPosition(targetPosition);
                        walker.SetDefaultPosition(targetPosition);
                        --currentCount;
                    }
                }

                unit.EndBattleStatus();

                walker.SetPlayer(_player);
            }
        }
    }

    private void OnTargetUpdated()
    {
        if (!_battleStarter.IsReadyToStart)
            SetUnitTargetPosition();
    }

    private void SetUnitTargetPosition()
    {
        foreach (var unit in Units)
        {
            if (unit is AlliedNPC npc)
                npc.SetAttackStatus(true);

            if (unit.IsReadyToFight)
            {
                if (unit is NPCWalker walker)
                {
                    Vector3 _positionOffset = UnityEngine.Random.insideUnitSphere * _targetPositionRandomizerRadius;
                    _positionOffset.y = 0;

                    walker.SetTargetPosition(_observer.CurrentTarget.Units[0].transform.position + _positionOffset);
                    walker.SetDefaultPosition(_observer.CurrentTarget.Units[0].transform.position + _positionOffset);
                }
            }
        }
    }

    private void OnLevelSet(Dictionary<PlayerSquadZoneType, int> capacity)
    {
        NewCapacitySet?.Invoke(capacity);
        NewLevelSet?.Invoke(_upgradeZone.SquadLevel);
    }

    private void OnBaseMoved()
    {
        foreach (var unit in _waitingUnits)
            if (unit is AlliedNPC npc)
                npc.ResetStartPosition();
    }
}
