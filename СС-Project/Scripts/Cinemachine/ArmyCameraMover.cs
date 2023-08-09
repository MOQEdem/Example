using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class ArmyCameraMover : MonoBehaviour
{
    [SerializeField] private float _moveTime;

    public event Action MovementComplite;

    public void StartToMove(EnemyArmy army)
    {
        Vector3 startPoint = army.Army[0].transform.position;
        Vector3 endPoint = army.LevelFinisher.transform.position;

        Vector3[] path = new Vector3[] { startPoint, endPoint };

        StartCoroutine(Moving(path));
    }

    private IEnumerator Moving(Vector3[] path)
    {
        Tween patchMoveTeween = transform.DOPath(path, _moveTime, PathType.CatmullRom);
        yield return patchMoveTeween.WaitForCompletion();

        MovementComplite?.Invoke();
    }
}
