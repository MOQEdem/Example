using UnityEngine;
using Cinemachine;
using System;
using System.Collections;

public class CinemachineController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _playerCamera;
    [SerializeField] private CinemachineVirtualCamera _startCamera;
    [SerializeField] private CinemachineVirtualCamera _tntCamera;
    [SerializeField] private CinemachineVirtualCamera _armyCamera;
    [SerializeField] private CinemachineVirtualCamera _dragonCamera;
    [SerializeField] private CinemachineVirtualCamera _bossDieCamera;
    [SerializeField] private CatapultFireSystem _catapult;
    [SerializeField] private EnemyArmyController _controller;
    [SerializeField] private DragonLair _dragonLair;
    [SerializeField] private NPC _boss;

    private ArmyCameraMover _armyCameraMover;
    private StartCameraMover _startCameraMover;
    private EnemyArmy _currentTarget;

    private void Awake()
    {
        _startCameraMover = GetComponentInChildren<StartCameraMover>();
        _armyCameraMover = GetComponentInChildren<ArmyCameraMover>();
        _armyCamera.Follow = _armyCameraMover.transform;
        _armyCamera.LookAt = _armyCameraMover.transform;

        _startCamera.Priority = 1;
        _playerCamera.Priority = 0;
        _tntCamera.Priority = 0;
        _armyCamera.Priority = 0;
        _dragonCamera.Priority = 0;
        _bossDieCamera.Priority = 0;
    }

    private void OnEnable()
    {
        _catapult.TNTLaunched += OnTNTLaunched;
        _controller.LevelFinisherReached += OnLevelFinisherReached;
        _controller.NewArmySet += OnNewArmySet;
        _dragonLair.DragonLaunched += OnDragonLaunched;
        _dragonLair.DragonComeDown += OnDragonComeDown;
        _armyCameraMover.MovementComplite += OnMovementComplite;
        _startCameraMover.MovementComplite += OnMovementComplite;

        if (_boss != null)
            _boss.Died += OnBossDied;
    }

    private void OnDisable()
    {
        _catapult.TNTLaunched -= OnTNTLaunched;
        _controller.LevelFinisherReached -= OnLevelFinisherReached;
        _controller.NewArmySet -= OnNewArmySet;
        _dragonLair.DragonLaunched -= OnDragonLaunched;
        _dragonLair.DragonComeDown -= OnDragonComeDown;
        _armyCameraMover.MovementComplite -= OnMovementComplite;
        _startCameraMover.MovementComplite -= OnMovementComplite;

        if (_boss != null)
            _boss.Died -= OnBossDied;
    }

    private void Start()
    {
        _currentTarget = _controller.CurrentArmy;
        _startCameraMover.StartToMove(_currentTarget);
    }

    private void OnTNTLaunched(TNT tnt)
    {
        tnt.Exploded += OnTNTExploded;

        _tntCamera.Follow = tnt.transform;
        _tntCamera.LookAt = tnt.transform;
        _tntCamera.Priority = 1;
        _playerCamera.Priority = 0;
    }

    private void OnTNTExploded(TNT tnt)
    {
        tnt.Exploded -= OnTNTExploded;

        _tntCamera.Priority = 0;
        _playerCamera.Priority = 1;
    }

    private void OnLevelFinisherReached(LevelFinisher finisher)
    {
        if (!finisher.IsLastFinisher)
        {
            _armyCameraMover.StartToMove(_currentTarget);

            _armyCamera.Priority = 1;
            _playerCamera.Priority = 0;
        }
    }

    private void OnMovementComplite()
    {
        _startCamera.Priority = 0;
        _armyCamera.Priority = 0;
        _playerCamera.Priority = 1;
    }

    private void OnNewArmySet(EnemyArmy army)
    {
        _currentTarget = army;
    }

    private void OnDragonLaunched(DragonPointer pointer)
    {
        _dragonCamera.Follow = pointer.transform;
        _dragonCamera.LookAt = pointer.transform;

        _dragonCamera.Priority = 1;
        _playerCamera.Priority = 0;
    }

    private void OnDragonComeDown()
    {
        _dragonCamera.Priority = 0;
        _playerCamera.Priority = 1;
    }


    private void OnBossDied(Character boss)
    {
        _bossDieCamera.transform.position = boss.transform.position;
        _bossDieCamera.Follow = boss.transform;
        _bossDieCamera.LookAt = boss.transform;

        _playerCamera.Priority = 0;
        _bossDieCamera.Priority = 1;

        StartCoroutine(WaitingToBossDie(boss));
    }

    private IEnumerator WaitingToBossDie(Character boss)
    {
        while (boss != null)
        {
            yield return null;
        }

        _playerCamera.Priority = 1;
        _bossDieCamera.Priority = 0;
    }
}
