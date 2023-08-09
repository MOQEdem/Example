using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Forge : MonoBehaviour
{
    [SerializeField] private GameObject _stack;
    [SerializeField] private int _maxBuffCount = 20;

    private ForgeResourceProducer _producer;
    private List<ForgeRecource> _forgeRecources = new List<ForgeRecource>();
    private int _numberOfBuffs;
    private NPCTrigger _trigger;

    public Vector3 BuffPoint => _trigger.transform.position;
    public int ResourceCount => _forgeRecources.Count;
    public int MaxBuffCount => _maxBuffCount;
    public bool IsAbleToBuff => _numberOfBuffs > 0;

    public event Action CountChange;

    private void Awake()
    {
        _producer = GetComponentInChildren<ForgeResourceProducer>();
        _trigger = GetComponentInChildren<NPCTrigger>();

        _numberOfBuffs = _forgeRecources.Count;

        CountChange?.Invoke();
    }

    private void OnEnable()
    {
        _trigger.Enter += OnNPCEnter;
        _producer.ResourceProduced += OnResourceProduced;
    }

    private void OnDisable()
    {
        _trigger.Enter -= OnNPCEnter;
        _producer.ResourceProduced -= OnResourceProduced;

    }

    public void BookBuff()
    {
        _numberOfBuffs--;
    }

    private void OnResourceProduced(ForgeRecource recource)
    {
        float animationTime = 0.7f;

        _forgeRecources.Add(recource);
        _numberOfBuffs++;

        if (_maxBuffCount <= _forgeRecources.Count)
            _producer.SetActivity(false);

        CountChange?.Invoke();

        recource.transform.DOKill();
        recource.transform.DOMove(GetCurrentFreePoint() + _stack.transform.position, animationTime);
    }

    private void OnNPCEnter(NPC npc)
    {
        if (npc is AlliedNPC allied && !allied.IsBuffed)
        {
            allied.SetForgeBuff();

            var resource = _forgeRecources[_forgeRecources.Count - 1];
            resource.transform.DOKill();
            resource.BuffNPC(npc);
            _forgeRecources.Remove(resource);

            if (_maxBuffCount > _forgeRecources.Count)
                _producer.SetActivity(true);

            CountChange?.Invoke();
        }
    }

    private Vector3 GetCurrentFreePoint()
    {
        float offset = 0.3f;
        int resourceInRow = 4;

        int currentResourceCount = _forgeRecources.Count - 1;

        int row = currentResourceCount / resourceInRow;
        int position = currentResourceCount % resourceInRow;

        return new Vector3(row * offset, 0, -position * offset);
    }
}
