using UnityEngine;

public class CharacterProduser : MonoBehaviour
{
    [SerializeField] private NPCWalker _prefab;
    [SerializeField] private float _timeBetweenSpawn;
    [SerializeField] private PlayerSquadZone _squadZone;
    [SerializeField] private Squad _playerSquad;
    [SerializeField] private Forge[] _forges;
    [SerializeField] private UpgraderLevel _level;
    [SerializeField] private int _tutorialSpawn = 0;

    private bool _isActive = true;
    private float _currentTime;
    private CharacterProduserProgressView _progressView;

    public UpgraderLevel Level => _level;
    public bool CanSpawn { get; private set; } = true;

    private void Awake()
    {
        _progressView = GetComponentInChildren<CharacterProduserProgressView>();
    }

    private void FixedUpdate()
    {
        if (_isActive && !_squadZone.IsFull)
        {
            _currentTime += Time.fixedDeltaTime;

            if (_tutorialSpawn > 0)
            {
                _currentTime += _timeBetweenSpawn;
                _tutorialSpawn--;
            }

            _progressView.SetProgressValue((_currentTime) / _timeBetweenSpawn);

            if (_currentTime >= _timeBetweenSpawn && CanSpawn)
            {
                Create(_prefab);
                _currentTime = 0;
            }
        }
        else
        {
            _currentTime = 0;
            _progressView.SetProgressValue((_currentTime) / _timeBetweenSpawn);
        }
    }

    public void DisableSpawning()
    {
        _currentTime = 0;
        _isActive = false;
    }

    public void EnableSpawning()
    {
        _currentTime = 0;
        _isActive = true;
    }

    private void Create(NPCWalker unit)
    {
        NPCWalker currentUnit = Instantiate(unit, transform.position, Quaternion.identity, _playerSquad.transform);

        currentUnit.SetDefaultPosition(_squadZone.CurrentPoint.position);

        if (currentUnit is AlliedNPC npc)
        {
            npc.SetStartPoint(_squadZone.CurrentPoint);
            // npc.CharacterMove.NavMeshAgent.avoidancePriority += _squadZone.NumberOfCurrentPoint;
            npc.SetAttackStatus(false);
        }


        if (_forges.Length > 0)
        {
            foreach (var forge in _forges)
            {
                if (forge != null && forge.IsAbleToBuff)
                {
                    forge.BookBuff();
                    currentUnit.SetTargetPosition(forge.BuffPoint);
                    _squadZone.SetUnit(currentUnit);
                    return;
                }
            }
        }

        currentUnit.SetTargetPosition(_squadZone.CurrentPoint.position);
        _squadZone.SetUnit(currentUnit);
    }
}
