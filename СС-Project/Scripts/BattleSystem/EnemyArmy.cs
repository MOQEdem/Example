using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyArmy : MonoBehaviour
{
    private List<EnemySquad> _army;
    private LevelFinisher _levelFinisher;
    private int _tutorialStoreScene = 2;
    private int _tutorialChestScene = 1;
    private EnemyArmyDestroyer _destroyer;

    public List<EnemySquad> Army => _army;
    public LevelFinisher LevelFinisher => _levelFinisher;

    public event Action<EnemyArmy> Defeated;
    public event Action SquadLost;

    private void Awake()
    {
        _army = GetComponentsInChildren<EnemySquad>(true).ToList();
        _levelFinisher = GetComponentInChildren<LevelFinisher>(true);
        _destroyer = GetComponent<EnemyArmyDestroyer>();
    }

    private void OnEnable()
    {
        foreach (EnemySquad squad in _army)
        {
            squad.SquadEmpty += OnEnemySquadEmpty;
        }

        if (_destroyer != null)
            _destroyer.BossDied += OnBossDied;
    }

    private void OnDisable()
    {
        foreach (EnemySquad squad in _army)
        {
            squad.SquadEmpty -= OnEnemySquadEmpty;
        }

        if (_destroyer != null)
            _destroyer.BossDied -= OnBossDied;
    }

    private void Start()
    {
        _levelFinisher.SetActiveStatus(false);
    }

    private void OnEnemySquadEmpty(Squad squad)
    {
        if (squad is EnemySquad enemySquad)
        {
            _army.Remove(enemySquad);
            SquadLost?.Invoke();
        }

        if (_army.Count == 0)
        {
            Defeated?.Invoke(this);

            int currentLevel = SceneManager.GetActiveScene().buildIndex;

            if (currentLevel == _tutorialChestScene)
                _levelFinisher.StartChestTutorial();
            else if (currentLevel == _tutorialStoreScene)
                _levelFinisher.StartStoreTutorial();
            else
                _levelFinisher.SetActiveStatus(true);
        }
    }

    private void OnBossDied()
    {
        EnemySquad[] _squads = _army.ToArray();

        foreach (EnemySquad squad in _squads)
            if (_squads != null)
                squad.Destroy(squad.Units.ToArray());
    }
}
