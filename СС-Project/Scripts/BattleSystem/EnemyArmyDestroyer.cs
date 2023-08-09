using System;
using UnityEngine;

public class EnemyArmyDestroyer : MonoBehaviour
{
    [SerializeField] private NPC _boss;

    public event Action BossDied;

    private void OnEnable()
    {
        if (_boss != null)
            _boss.Died += OnBossDied;
    }

    private void OnDisable()
    {
        if (_boss != null)
            _boss.Died -= OnBossDied;
    }

    private void OnBossDied(Character boss)
    {
        BossDied?.Invoke();
    }
}
