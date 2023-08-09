using System;
using DG.Tweening;
using UnityEngine;
using Agava.YandexMetrica;

public class Stable : MonoBehaviour
{
    [Header("Transport Settings")]
    [SerializeField] private StableTransportSetter _transportData;
    [SerializeField] private Transform _transportPoint;
    [Space]
    [Header("Activation Settings")]
    [SerializeField] private TriggerObserver _enterTrigger;
    [SerializeField] private StableTimer _stableTimer;
    [Space]
    [Header("Animation Settings")]
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField][Min(0)] private float _getTransportAnimationDuration = 1f;

    private RidingTransport _ridingTransport;
    private Character _player;
    private ADSShower _adsShower;

    public event Action MountUsed;

    private void Awake()
    {
        _adsShower = new ADSShower();

        RidingTransport prefab = _transportData.GetTransport();

        if (prefab != null)
            _ridingTransport = Instantiate(prefab, _transportPoint);
    }

    private void OnEnable()
    {
        _enterTrigger.TargetFound += OnTargetFound;
        _enterTrigger.TargetLost += OnTargetLost;
        _adsShower.Rewarded += OnRewarded;
    }

    private void OnDisable()
    {
        _enterTrigger.TargetFound -= OnTargetFound;
        _enterTrigger.TargetLost -= OnTargetLost;
        _adsShower.Rewarded -= OnRewarded;
    }

    private void OnTargetFound(Character character)
    {
        if (character is PlayableCharacter)
        {
            _player = character;

#if UNITY_EDITOR
            _stableTimer.Start(MakePlayerARider);
            return;
#endif
            Player player = character.GetComponent<Player>();

            if (player.PlayerAccess.IsPlayerHaveAccess(AccessType.Mount))
                _stableTimer.Start(MakePlayerARider);
            else
                _stableTimer.Start(_adsShower.TryWatchADS);
        }
    }

    private void OnTargetLost(Character character)
    {
        if (character is PlayableCharacter)
            _stableTimer.Stop();
    }

    private void MakePlayerARider()
    {
        _enterTrigger.gameObject.SetActive(false);
        _player.transform.DOMove(_ridingTransport.Saddle.transform.position, _getTransportAnimationDuration);
        _player.transform.DORotateQuaternion(_ridingTransport.Saddle.transform.rotation, _getTransportAnimationDuration);
        _player.transform.DOMoveY(_ridingTransport.Saddle.transform.position.y, _getTransportAnimationDuration).SetEase(_animationCurve).OnComplete(
            () =>
            {
                _player.GetComponent<ActorTakeTransport>().Take(_ridingTransport);
            });
    }

    private void OnRewarded()
    {
#if !UNITY_EDITOR
            YandexMetrica.Send("mountADS");
#endif

        MountUsed?.Invoke();

        MakePlayerARider();
    }
}
