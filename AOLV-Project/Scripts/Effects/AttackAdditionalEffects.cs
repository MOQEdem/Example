using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AttackAdditionalEffects : MonoBehaviour
{
    [SerializeField] private CameraShaking _cameraShaking;
    [SerializeField] private SlowMo _slowMo;
    [SerializeField] private PlayerAttacker _playerAttacker;
    [Header("SlowMo params")]
    [SerializeField] private float _timeScale1 = 0.25f;
    [SerializeField] private float _slowMoDuration1 = 0.05f;
    [Space]
    [SerializeField] private float _timeScale2 = 0.5f;
    [SerializeField] private float _slowMoDuration2 = 0.3f;

    private WaitForSecondsRealtime _delay;
    private Coroutine _waitEndingSlowMo;
    private Tween _tween;
    private bool _enableSlowMoType1 = true;
    private bool _enableSlowMoType2;

    private void Awake()
    {
        _delay = new WaitForSecondsRealtime(_slowMoDuration1);
    }

    private void OnEnable()
    {
        _playerAttacker.Attacked += OnAttacked;
    }

    private void OnDisable()
    {
        _playerAttacker.Attacked -= OnAttacked;
    }

    private void OnAttacked()
    {
        _cameraShaking.StartShaking();

        if (_enableSlowMoType1)
        {
            _delay = new WaitForSecondsRealtime(_slowMoDuration1);
            StartSlowMo(_timeScale1);
        }
        else if (_enableSlowMoType2)
        {
            _delay = new WaitForSecondsRealtime(_slowMoDuration2);

            StartSlowMo(_timeScale2);
        }
    }

    private void StartSlowMo(float timeScaleTime)
    {
        if (_waitEndingSlowMo != null)
        {
            StopCoroutine(_waitEndingSlowMo);
            _slowMo.Deactivate();
        }
        _waitEndingSlowMo = StartCoroutine(WaitEndingSlowMo(timeScaleTime));
    }

    private IEnumerator WaitEndingSlowMo(float targetTimeScale)
    {
        if (_tween != null && _tween.active)
            _tween.Complete();

        _slowMo.Activate(targetTimeScale);

        _tween = DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 1, _delay.waitTime).SetEase(Ease.InQuad).SetUpdate(true);

        yield return _delay;
        _slowMo.Deactivate();
    }

    public void EnableSowMo1(Toggle toggle)
    {
        _enableSlowMoType1 = toggle.isOn;
    }

    public void EnableSowMo2(Toggle toggle)
    {
        _enableSlowMoType2 = toggle.isOn;
    }
}
