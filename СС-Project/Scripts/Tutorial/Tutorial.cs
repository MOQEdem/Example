using UnityEngine;
using Cinemachine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private TutorialPointer[] _triggers;
    [SerializeField] private StartCameraMover _startCamera;
    [SerializeField] private Animator _storeTutorial;
    [SerializeField] private Animator _chestTutorial;
    [SerializeField] private Animator _saveCitizenTutorial;
    [SerializeField] private Animator _catapultTutorial;
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private CinemachineVirtualCamera _tntCamera;
    [SerializeField] private CinemachineVirtualCamera _educationCamera;
    [SerializeField] private Transform _storePoint;
    [SerializeField] private LevelFinisher _levelFinisher;
    [SerializeField] private TargetIndicator _targetIndicator;
    [SerializeField] private Transform _firstLevelFinish;
    [SerializeField] private PlayableCharacter _player;
    [SerializeField] private bool _isLevelEducationEnable;

    private Animator[] _pointers;
    private int _currentTrigger = 0;
    private int _cameraDelay = 3;
    private float _indicatorOffset = 0.3f;

    private void OnEnable()
    {
        _startCamera.MovementComplite += Enable;

        if (_isLevelEducationEnable)
            foreach (var trigger in _triggers)
                trigger.StepDone += OnPlayerEnter;
    }

    private void OnDisable()
    {
        _startCamera.MovementComplite -= Enable;

        if (_isLevelEducationEnable)
            foreach (var trigger in _triggers)
                trigger.StepDone -= OnPlayerEnter;
    }
    public void Enable()
    {
        _pointers = new Animator[_triggers.Length];

        for (int i = 0; i < _triggers.Length; i++)
        {
            _pointers[i] = _triggers[i].GetComponentInChildren<Animator>(true);
        }

        if (_isLevelEducationEnable)
        {
            _pointers[0].gameObject.SetActive(true);
            EnableTargetIndicator(_pointers[0].gameObject.transform);

            for (int i = 1; i < _pointers.Length; i++)
                _pointers[i].gameObject.SetActive(false);
        }
        else
        {
            foreach (var pointer in _pointers)
                pointer.gameObject.SetActive(false);
        }
    }

    private void EnableTargetIndicator(Transform target)
    {
        _targetIndicator.gameObject.SetActive(true);
        _targetIndicator.gameObject.transform.SetParent(_player.gameObject.transform);
        _targetIndicator.gameObject.transform.localPosition = Vector3.up * _indicatorOffset;
        _targetIndicator.SetPoints(_player.transform, target);
    }

    public void StartStoreEducation()
    {
        foreach (var pointer in _pointers)
            pointer.gameObject.SetActive(false);

        // _catapultTutorial.gameObject.SetActive(false);
        _storeTutorial.gameObject.SetActive(true);
        EnableTargetIndicator(_storeTutorial.gameObject.transform);
        _storeTutorial.GetComponent<TutorialPointer>().StepDone += OnStoreOpened;
        LookAtEducation();
    }

    private void LookAtEducation()
    {
        _tntCamera.Priority = 0;
        _camera.Priority = 0;
        _educationCamera.Priority = 1;
        Invoke(nameof(LookAtPlayer), _cameraDelay);
    }

    private void LookAtPlayer()
    {
        _educationCamera.Priority = 0;
        _camera.Priority = 1;
    }

    private void OnPlayerEnter()
    {
        _pointers[_currentTrigger].gameObject.SetActive(false);
        _triggers[_currentTrigger].StepDone -= OnPlayerEnter;

        _currentTrigger++;

        if (_pointers.Length > _currentTrigger)
        {
            _pointers[_currentTrigger].gameObject.SetActive(true);
            _targetIndicator.SetPoints(_player.transform, _pointers[_currentTrigger].gameObject.transform);
        }
        else
        {
            _targetIndicator.gameObject.SetActive(false);
        }
    }

    public void StartChestEducation()
    {
        _chestTutorial.gameObject.SetActive(true);
        _chestTutorial.GetComponent<TutorialPointer>().StepDone += OnChestOpened;
        _targetIndicator.SetPoints(_player.transform, _chestTutorial.gameObject.transform);
        Invoke(nameof(LookAtEducation), _cameraDelay - 1);
    }

    private void OnChestOpened()
    {
        _chestTutorial.gameObject.SetActive(false);
        _saveCitizenTutorial.gameObject.SetActive(true);
        _saveCitizenTutorial.GetComponent<TutorialPointer>().StepDone += OnCitizenFree;
        _targetIndicator.SetPoints(_player.transform, _firstLevelFinish);
    }

    private void OnCitizenFree()
    {
        _saveCitizenTutorial.gameObject.SetActive(false);
        _levelFinisher.SetActiveStatus(true);
    }

    private void OnStoreOpened()
    {
        _storeTutorial.gameObject.SetActive(false);
        _levelFinisher.SetActiveStatus(true);
        _targetIndicator.SetPoints(_player.transform, _levelFinisher.transform);
    }
}
