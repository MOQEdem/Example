using UnityEngine;
using UnityEngine.UI;

public class PlayerDash : MonoBehaviour
{
    [Header("DashSettings")]
    [SerializeField] private float _dashPower;
    [SerializeField] private float _dashTime;
    [Space]
    [Header("OtherSettings")]
    [SerializeField] private float _timeToCoolDown;
    [SerializeField] private Timer _timer;
    [SerializeField] private Button _dashButton;
    [Space]
    [Header("Visual")]
    [SerializeField] private ParticleSystem _particle;

    private float _dashTimeLast;

    public bool IsAbleToDash { get; private set; }
    public bool IsDashTimeLast => _dashTimeLast > 0;

    private void Awake()
    {
        IsAbleToDash = true;
        _particle.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _timer.Completed += OnTimerComplite;
        _dashButton.onClick.AddListener(StartCoolDownDash);
    }

    private void OnDisable()
    {
        _timer.Completed -= OnTimerComplite;
        _dashButton.onClick.RemoveListener(StartCoolDownDash);
    }

    private void OnTimerComplite()
    {
        IsAbleToDash = true;

        _dashButton.interactable = true;
        _particle.gameObject.SetActive(false);
    }

    public void StartCoolDownDash()
    {
        IsAbleToDash = false;
        _timer.StartTimer(_timeToCoolDown);


        _particle.gameObject.SetActive(true);

        _dashTimeLast = _dashTime;

        _dashButton.interactable = false;
    }

    public float GetDashPower()
    {
        if (_dashTimeLast > 0)
        {
            _dashTimeLast -= Time.deltaTime;

            return _dashPower;
        }
        else
        {
            return 0;
        }
    }
}
