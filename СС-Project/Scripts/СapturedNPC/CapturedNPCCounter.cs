using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CapturedNPCCounter : MonoBehaviour
{
    private TMP_Text _text;
    private List<Cage> _cages;
    private int _totalCounter;
    private int _currentOpenedCage;
    private bool _isAllOpened = false;

    public bool IsAllOpened => _isAllOpened;

    public event Action AllCagesOpened;

    private void Awake()
    {
        _cages = GetComponentsInChildren<Cage>().ToList();
    }

    private void OnEnable()
    {
        foreach (var cage in _cages)
            cage.CageOpened += OnCageOpened;
    }

    private void OnDisable()
    {
        foreach (var cage in _cages)
            cage.CageOpened -= OnCageOpened;
    }

    private void Start()
    {
        if (_cages.Count == 0)
        {
            _isAllOpened = true;
            AllCagesOpened?.Invoke();
            _text.text = "-";
        }
        else
        {
            _totalCounter = _cages.Count;
            _currentOpenedCage = 0;
            SetUIValue();
        }
    }

    private void OnCageOpened(Cage cage)
    {
        cage.CageOpened -= OnCageOpened;
        _cages.Remove(cage);
        _currentOpenedCage++;
        SetUIValue();

        if (_cages.Count == 0)
        {
            _isAllOpened = true;
            AllCagesOpened?.Invoke();
        }
    }

    private void SetUIValue()
    {
        _text.text = $"{_currentOpenedCage}/{_totalCounter}";
    }
}
