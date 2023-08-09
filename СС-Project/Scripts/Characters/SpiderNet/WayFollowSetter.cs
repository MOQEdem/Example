using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WayFollowSetter : MonoBehaviour
{
    [SerializeField] private NPCWalker _enemyCharacter;
    [SerializeField] private CharacterMove _characterMove;
    [SerializeField] private List<WayPoint> _wayPoints;
    [SerializeField] private Vector2 _moveToNextWayPointDelay = new Vector2(1, 2);
    [SerializeField] private Vector2 _randomizeSpeedDiapason = new Vector2(1, 3);
    [SerializeField] private bool _randomizeSpeed = false;

    private int _currentWayPointIndex;

    private void Start() =>
        StartCoroutine(SetNextWayPointWithDelay());

    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, _wayPoints[_currentWayPointIndex].transform.position) <= 0.5f)
            StartCoroutine(SetNextWayPointWithDelay());
    }

    private IEnumerator SetNextWayPointWithDelay()
    {
        _currentWayPointIndex++;
        if (_wayPoints.Count == _currentWayPointIndex)
            _currentWayPointIndex = 0;
        yield return new WaitForSeconds(Random.Range(_moveToNextWayPointDelay.x, _moveToNextWayPointDelay.y));
        _enemyCharacter.SetDefaultPosition(_wayPoints[_currentWayPointIndex].transform.position);
        if (_randomizeSpeed)
            _characterMove.SetSpeedMove(Random.Range(_randomizeSpeedDiapason.x, _randomizeSpeedDiapason.y));
    }

#if UNITY_EDITOR
    private void Reset() =>
        SetComponents();

    private void OnValidate() =>
        SetComponents();

    private void SetComponents()
    {
        _wayPoints = GetComponentsInChildren<WayPoint>().ToList();
        _enemyCharacter = GetComponent<NPCWalker>();
        _characterMove = GetComponent<CharacterMove>();
    }
#endif
}
