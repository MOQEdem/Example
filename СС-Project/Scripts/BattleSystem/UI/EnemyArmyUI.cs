using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyArmyUI : MonoBehaviour
{
    [SerializeField] private EnemyArmyUISlider _enemyArmyUI;

    private EnemyArmy[] _enemyArmies;
    private List<Character> _enemies = new List<Character>();
    private List<int> _enemiesOnStages = new List<int>();
    private Slider _progressBar;
    private CompleteIconChanger _iconChanger;
    private LevelGoalMapper _goalMapper;
    private int _enemiesKilled = 0;

    private void Awake()
    {
        _enemyArmies = GetComponentsInChildren<EnemyArmy>();
        _progressBar = _enemyArmyUI.GetComponent<Slider>();
        _iconChanger = _enemyArmyUI.GetComponent<CompleteIconChanger>();
        _goalMapper = _enemyArmyUI.GetComponent<LevelGoalMapper>();
    }

    private void Start()
    {
        foreach (EnemyArmy enemyArmy in _enemyArmies)
        {
            int armyCount = 0;
            
            foreach (EnemySquad squad in enemyArmy.Army)
            {
                foreach (Character unit in squad.Units)
                {
                    _enemies.Add(unit);
                    unit.Died += OnEnemyDied;
                    armyCount++;
                }
            }

            _enemiesOnStages.Add(armyCount);         
        }

        SetIconDistance();
        _goalMapper.Enable();
        _progressBar.maxValue = _enemies.Count;
        _progressBar.minValue = 0;
        _progressBar.value = _enemiesKilled;
    }

    private void OnEnemyDied(Character unit)
    {
        unit.Died -= OnEnemyDied;
        _enemiesKilled++;
        _progressBar.value = _enemiesKilled;
        _iconChanger.TryChangeIcons();
    }

    private void SetIconDistance()
    {
        for (int i = 0; i < _enemiesOnStages.Count; i++)
            _goalMapper.AddDistance(_enemiesOnStages[i]);
    }
}
