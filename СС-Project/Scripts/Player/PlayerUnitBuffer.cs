using System.Collections.Generic;
using UnityEngine;

public class PlayerUnitBuffer : MonoBehaviour
{
    [SerializeField] private PlayerSquad _playerSquad;
    [SerializeField] private int _additionalHealth;
    [SerializeField] private int _additionalDamage;
    [SerializeField] private float _additionalScale;
    [SerializeField] private float _buffRadius = 3;

    private SphereCollider _buffZoneCollider;
    private ParticleSystem _buffZoneEffect;
    private List<AlliedNPC> _npcs = new List<AlliedNPC>();
    public float BuffRadius => _buffRadius;

    private void Awake()
    {
        _buffZoneEffect = GetComponentInChildren<ParticleSystem>();
        _buffZoneCollider = GetComponent<SphereCollider>();

        _buffZoneCollider.enabled = false;
        _buffZoneEffect.Stop();
    }

    private void OnEnable()
    {
        _playerSquad.BattleStarted += OnBattleStarted;
        _playerSquad.BattleEnded += OnBattleEnded;
        _playerSquad.SquadEmpty += OnPlayerSquadEmpty;
    }

    private void OnDisable()
    {
        _playerSquad.BattleStarted -= OnBattleStarted;
        _playerSquad.BattleEnded -= OnBattleEnded;
        _playerSquad.SquadEmpty -= OnPlayerSquadEmpty;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<AlliedNPC>(out AlliedNPC unit))
        {
            if (!unit.IsHeavy && unit != null)
            {
                unit.SetBuff(true, _additionalHealth, _additionalDamage, _additionalScale);
                _npcs.Add(unit);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<AlliedNPC>(out AlliedNPC unit))
        {
            if (!unit.IsHeavy && unit != null)
            {
                unit.SetBuff(false, -_additionalHealth, -_additionalDamage, -_additionalScale);
                _npcs.Remove(unit);
            }
        }
    }

    public void SetNewBuffRadius(float newRadius)
    {
        _buffRadius = newRadius;
        _buffZoneCollider.radius = newRadius;
        _buffZoneEffect.transform.localScale = new Vector3(newRadius, 1, newRadius);
    }

    private void OnBattleStarted()
    {
        _buffZoneCollider.enabled = true;
        _buffZoneEffect.Play();
    }

    private void OnBattleEnded()
    {
        DisableZone();
    }

    private void OnPlayerSquadEmpty(Squad squad)
    {
        DisableZone();
    }

    private void DisableZone()
    {
        foreach (var unit in _npcs)
            if (unit != null)
                unit.SetBuff(false, -_additionalHealth, -_additionalDamage, -_additionalScale);

        _npcs.Clear();

        _buffZoneCollider.enabled = false;
        _buffZoneEffect.Stop();
    }
}
