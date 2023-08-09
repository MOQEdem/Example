using System;
using UnityEngine;
using UnityEngine.UI;

public class NPCGroupBuyer : MonoBehaviour
{
    [SerializeField] private NPCGroup _group;
    [SerializeField] private NPCType _type;
    [SerializeField] private int _maxNPCCount;
    [SerializeField] private BuildableObject _unlockObject;
    [SerializeField] private Button _addButton;
    [SerializeField] private Button _removeButton;

    private int _currentNPCCount = 0;

    public bool IsNPCUnlocked => _unlockObject.IsBuilded;
    public bool IsAbleToRemove => _currentNPCCount > 0;
    public bool IsAbleToAdd => _currentNPCCount < _maxNPCCount && _group.IsFreeSlot;
    public int CurrentNPCCount => _currentNPCCount;

    public event Action Updated;
    public event Action Unlocked;

    private void OnEnable()
    {
        if (!_unlockObject.IsBuilded)
            _unlockObject.Builded += OnBuildableObjectBuild;

        _addButton.onClick.AddListener(AddNPC);
        _removeButton.onClick.AddListener(RemoveNPC);
    }

    private void OnDisable()
    {
        if (_unlockObject.gameObject.activeSelf)
            if (!_unlockObject.IsBuilded)
                _unlockObject.Builded -= OnBuildableObjectBuild;

        _addButton.onClick.RemoveListener(AddNPC);
        _removeButton.onClick.RemoveListener(RemoveNPC);

        _currentNPCCount = 0;
    }

    private void Start()
    {
        foreach (var npc in _group.NPCList)
        {
            if (npc == _type)
            {
                _currentNPCCount++;
            }
        }

        Debug.Log(_currentNPCCount);

        Updated?.Invoke();
    }

    private void AddNPC()
    {
        if (_currentNPCCount < _maxNPCCount)
        {
            _currentNPCCount++;
            _group.AddNPC(_type);
            Updated?.Invoke();
        }
    }

    private void RemoveNPC()
    {
        if (_currentNPCCount > 0)
        {
            if (_group.NPCList.Contains(_type))
            {
                _currentNPCCount--;
                _group.RemoveNPC(_type);
                Updated?.Invoke();
            }
        }
    }

    private void OnBuildableObjectBuild()
    {
        Unlocked?.Invoke();
        _unlockObject.Builded -= OnBuildableObjectBuild;
    }
}
