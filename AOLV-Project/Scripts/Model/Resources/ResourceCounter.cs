using System;
using UnityEngine;

public class ResourceCounter : MonoBehaviour
{
    private ResourceBag _bag;
    private LeaderboardValue _leaderboardValue;

    public int Value => _leaderboardValue.Value;

    private void Awake()
    {
        _leaderboardValue = new LeaderboardValue();
        _leaderboardValue.Load();

        _bag = GetComponentInParent<ResourceBag>();
    }

    private void OnEnable()
    {
        _bag.ResourceTaken += OnResourceTaken;
    }

    private void OnDisable()
    {
        _bag.ResourceTaken -= OnResourceTaken;
    }

    private void OnResourceTaken(ResourcePack pack, Transform position)
    {
        _leaderboardValue.Add(pack.Value);
    }

    [Serializable]
    public class LeaderboardValue : SavedObject<LeaderboardValue>
    {
        private const string SaveKey = nameof(LeaderboardValue);

        [SerializeField] private int _value;

        public int Value => _value;

        public LeaderboardValue()
            : base(SaveKey)
        { }

        public void Add(int value)
        {
            _value += value;
            Save();
        }

        protected override void OnLoad(LeaderboardValue loadedObject)
        {
            _value = loadedObject.Value;
        }
    }
}
