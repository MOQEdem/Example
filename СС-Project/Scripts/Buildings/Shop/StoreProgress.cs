using System;
using UnityEngine;

[Serializable]
public class StoreProgress : SavedObject<StoreProgress>
{
    private const string SaveKey = nameof(StoreProgress);

    [SerializeField] private LotProgress _health;
    [SerializeField] private LotProgress _damage;
    [SerializeField] private LotProgress _speed;
    [SerializeField] private LotProgress _buffRadius;
    [SerializeField] private LotProgress _stackCapacity;

    [SerializeField] private LotProgress[] _progresses;

    public StoreProgress() : base(SaveKey)
    {
        _health = new LotProgress(PlayerStatType.Health);
        _damage = new LotProgress(PlayerStatType.Damage);
        _speed = new LotProgress(PlayerStatType.Speed);
        _buffRadius = new LotProgress(PlayerStatType.BuffRadius);
        _stackCapacity = new LotProgress(PlayerStatType.StackCapacity);

        _progresses = new LotProgress[] { _health, _damage, _speed, _buffRadius, _stackCapacity };
    }

    public void UpLotStep(PlayerStatType type)
    {
        var progress = GetLotProgress(type);

        if (progress != null)
            progress.UpStep();
    }

    public void AddSpendedResource(PlayerStatType type)
    {
        var progress = GetLotProgress(type);

        if (progress != null)
            progress.AddSpendedResource();
    }

    public int GetLotStep(PlayerStatType type)
    {
        var progress = GetLotProgress(type);

        if (progress != null)
            return progress.Step;
        else
            return 0;
    }

    public int GetLotSpendedResource(PlayerStatType type)
    {
        var progress = GetLotProgress(type);

        if (progress != null)
            return progress.ResourceSpended;
        else
            return 0;
    }

    public LotProgress GetLotProgress(PlayerStatType type)
    {
        foreach (var progress in _progresses)
        {
            if (progress.Type == type)
                return progress;
        }

        return null;
    }

    protected override void OnLoad(StoreProgress loadedObject)
    {
        _health = loadedObject._health;
        _damage = loadedObject._damage;
        _speed = loadedObject._speed;
        _buffRadius = loadedObject._buffRadius;
        _stackCapacity = loadedObject._stackCapacity;

        _progresses = loadedObject._progresses;
    }

    [Serializable]
    public class LotProgress
    {
        [SerializeField] private PlayerStatType _type;
        [SerializeField] private int _step;
        [SerializeField] private int _resourceSpended;

        public PlayerStatType Type => _type;
        public int Step => _step;
        public int ResourceSpended => _resourceSpended;

        public LotProgress(PlayerStatType type)
        {
            _type = type;
        }

        public void UpStep()
        {
            _step++;
            _resourceSpended = 0;
        }

        public void AddSpendedResource()
        {
            _resourceSpended++;
        }
    }
}
