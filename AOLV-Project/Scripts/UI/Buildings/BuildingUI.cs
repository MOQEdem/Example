using System.Collections;
using UnityEngine;

public class BuildingUI : MonoBehaviour
{
    [SerializeField] private Trigger<Player> _trigger;
    [SerializeField] private float _movingSpeed;

    private float _openXPosition;
    private float _closeXPosition;
    private Coroutine _moving;


    private void Awake()
    {
        _openXPosition = -transform.position.x;
        _closeXPosition = transform.position.x;
        transform.position = new Vector3(_closeXPosition, transform.position.y, transform.position.z);
    }

    private void OnEnable()
    {
        _trigger.Enter += OnPlayerEnter;
        _trigger.Exit += OnPlayerExit;
    }

    private void OnDisable()
    {
        _trigger.Enter -= OnPlayerEnter;
        _trigger.Exit -= OnPlayerExit;
    }

    private void OnPlayerEnter(Player player)
    {
        if (_moving != null)
        {
            StopCoroutine(_moving);
            _moving = null;
        }

        _moving = StartCoroutine(Moving(_openXPosition));
    }

    private void OnPlayerExit(Player player)
    {
        if (_moving != null)
        {
            StopCoroutine(_moving);
            _moving = null;
        }

        _moving = StartCoroutine(Moving(_closeXPosition));
    }

    private IEnumerator Moving(float position)
    {
        Vector3 targetPosition = new Vector3(position, transform.position.y, transform.position.z);

        while (transform.position.x != position)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, _movingSpeed * Time.deltaTime);

            yield return null;
        }
    }
}
