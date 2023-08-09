using UnityEngine;

public class StatueBuilder : MonoBehaviour
{
    private StatuePart[] _parts;
    private StatueBuildingZone _upgradeZone;
    private StatueEndlessZone _statueEndlessZone;
    private StatueLevel _level;
    private StatueFull _statueFull;

    private void Awake()
    {
        _level = new StatueLevel();
        _level.Load();

        _upgradeZone = GetComponentInChildren<StatueBuildingZone>();
        _parts = GetComponentsInChildren<StatuePart>();
        _statueFull = GetComponentInChildren<StatueFull>();
        _statueEndlessZone = GetComponentInChildren<StatueEndlessZone>();

    }

    private void OnEnable()
    {
        _upgradeZone.ChangeCount += OnChangeCount;
    }

    private void OnDisable()
    {
        _upgradeZone.ChangeCount -= OnChangeCount;
    }

    private void Start()
    {
        if (!_upgradeZone.IsFilled)
        {
            _statueFull.gameObject.SetActive(false);
            _statueEndlessZone.gameObject.SetActive(false);

            if (_level.Level >= 0)
            {
                for (int i = 0; i <= _level.Level; i++)
                {
                    _parts[i].ActivateAtStart();
                }
            }
        }
        else
        {
            for (int i = 0; i < _parts.Length; i++)
            {
                Destroy(_parts[i].gameObject);
            }

            _upgradeZone.ChangeCount -= OnChangeCount;
            _upgradeZone.gameObject.SetActive(false);
        }
    }

    public void Save()
    {
        if (_level != null)
            _level.Save();
    }

    private void OnChangeCount()
    {
        float currentPercentageOfSpendResources = (float)_upgradeZone.Resources.Count / _upgradeZone.TargetCount;

        if (currentPercentageOfSpendResources >= 1)
        {
            _statueFull.gameObject.SetActive(true);
            _statueEndlessZone.gameObject.SetActive(true);

            for (int i = 0; i < _parts.Length; i++)
            {
                Destroy(_parts[i].gameObject);
            }

            _upgradeZone.ChangeCount -= OnChangeCount;
            _upgradeZone.gameObject.SetActive(false);
        }
        else
        {
            if (currentPercentageOfSpendResources > 0)
            {
                float currentActiveStatuePart = (float)_parts.Length * currentPercentageOfSpendResources;

                if ((int)currentActiveStatuePart > _level.Level)
                {
                    _level.LevelUp();

                    if (!_parts[_level.Level].IsActivated)
                        _parts[_level.Level].Activate();
                }
            }
        }
    }
}
