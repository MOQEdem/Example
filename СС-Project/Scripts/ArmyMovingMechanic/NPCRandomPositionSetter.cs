using UnityEngine;

public class NPCRandomPositionSetter : MonoBehaviour
{
    [SerializeField] private float _nearPositionRadius = 1;
    [SerializeField] private Vector2 _changeStateInterval = new Vector2(3, 5);
    [SerializeField] private Vector2 _randomizeSpeedDiapason = new Vector2(1, 3);
    [SerializeField] private bool _randomizeSpeed = false;

    private NPCWalker _npc;
    private CharacterMove _characterMove;
    private Vector3 _defaultPosition;
    private Vector3 _gizmoPos;
    private float _timer;

    private void Awake()
    {
        _npc = GetComponent<NPCWalker>();
        _characterMove = GetComponent<CharacterMove>();

        _defaultPosition = transform.position;
    }

    public void TryGetNewDefaultPosition()
    {
        _timer -= Time.fixedDeltaTime;

        if (_timer < 0)
        {
            _gizmoPos = GetRandomPosition();

            if (_npc != null)
                _npc.SetDefaultPosition(_gizmoPos);

            _timer = GetRandomDuration();

            if (_randomizeSpeed)
                _characterMove.SetSpeedMove(GetRandomSpeed());
        }
    }

    private void OnDisable() => _characterMove.SetSpeedMove(_characterMove.Speed);

    public void SetDefaultPosition(Vector3 defaultPosition) =>
        _defaultPosition = defaultPosition;

    private Vector3 GetRandomPosition()
    {
        Vector2 pos = Random.insideUnitCircle * _nearPositionRadius;

        return _defaultPosition + new Vector3(pos.x, 0, pos.y);
    }

    private float GetRandomDuration() =>
        Random.Range(_changeStateInterval.x, _changeStateInterval.y);

    private float GetRandomSpeed() =>
        Random.Range(_randomizeSpeedDiapason.x, _randomizeSpeedDiapason.y);

    //#if UNITY_EDITOR
    //    private void OnDrawGizmos()
    //    {
    //        Gizmos.color = Color.red;
    //        Gizmos.DrawSphere(_gizmoPos, 0.1f);
    //    }

    //    private void Reset() => 
    //        SetComponents();

    //    private void OnValidate() => 
    //        SetComponents();

    //    private void SetComponents()
    //    {
    //        _npc = GetComponent<NPC>();
    //        _characterMove = GetComponent<CharacterMove>();
    //    }
    //#endif
}
