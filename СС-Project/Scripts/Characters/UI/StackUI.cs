using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(PlayerStack))]
public class StackUI : MonoBehaviour
{
    [SerializeField] private PlayerResourceUI _playerResourceCounter;
    [SerializeField] private EnemyResourceUI _enemyResourceCounter;

    private PlayerStack _stack;
    private TMP_Text _playerResourceText;
    private TMP_Text _enemyResourceText;

    private void Awake()
    {
        _stack = GetComponent<PlayerStack>();
        _playerResourceText = _playerResourceCounter.GetComponent<TMP_Text>();
        _enemyResourceText = _enemyResourceCounter.GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        _stack.ResourceQuantityChanged += OnResourceQuantityChanged;
    }

    private void OnDisable()
    {
        _stack.ResourceQuantityChanged -= OnResourceQuantityChanged;
    }

    private void OnResourceQuantityChanged()
    {
        _playerResourceText.text = _stack.PlayerResource.Count.ToString();
        _enemyResourceText.text = _stack.EnemyResource.Count.ToString();
    }
}
