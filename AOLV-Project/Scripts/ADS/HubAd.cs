using System.Collections;
using System.Collections.Generic;
using Agava.VKGames;
using Agava.YandexGames;
using Agava.YandexMetrica;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VideoAd = Agava.YandexGames.VideoAd;

public class HubAd : MonoBehaviour
{
    [SerializeField] private List<ResourceHolder> _holders;
    [SerializeField] private int _reward;
    [SerializeField] private ResourcesData _data;
    [SerializeField] private ResourceBag _bag;
    [SerializeField] private Resizer _resizer;
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Timer _timer;
    [SerializeField] private float _duration;
    [SerializeField] private float _resizeTime;
    [SerializeField] private PlayerTrigger _trigger;
    [SerializeField] private Vector3 _normalScale;
    
    private ResourceType _resourceType;
    private float _normalTimeScale;
    
    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
        _normalTimeScale = Time.timeScale;
    }
    
    private void Start()
    {
        _text.text = "+" + _reward;
    }

    private void OnEnable()
    {
        _bag.HolderEmpty += ShowCanvas;
        _trigger.Exit += onClosedButtonClick;
    }

    private void OnDisable()
    {
        _bag.HolderEmpty -= ShowCanvas;
        _trigger.Exit -= onClosedButtonClick;
    }

    private void ShowCanvas(ResourceType type)
    {
        _resizer.Resize(_resizeTime, _normalScale);
        _icon.sprite = _data.GetIcon(type);
        _resourceType = type;
        StartCoroutine(CountDown());
    }

    private IEnumerator CountDown()
    {
        yield return _timer.CountingDown(_duration);
        _resizer.Resize(0.5f, 0f);
    }
    
    public void onClosedButtonClick(Player player)
    {
        _resizer.Resize(0.1f, 0f);
        transform.gameObject.SetActive(false);
    }

    public void onRewardButtonClick()
    {
        Time.timeScale = 0;
        VideoAd.Show(onRewardedCallback: Reward, onCloseCallback: PlayScale, onErrorCallback: PlayScale);
    }
    
    private void Reward()
    {
        foreach (var holder in _holders)
        {
            if (holder.Type == _resourceType)
            {
                holder.Add(_reward);
            }
        }

        YandexMetrica.Send("AdWatched");
        _resizer.Resize(0.1f, 0f);
        transform.gameObject.SetActive(false);
    }

    private void PlayScale(string strind)
    {
        Time.timeScale = _normalTimeScale;
    }
    
    private void PlayScale()
    {
        Time.timeScale = _normalTimeScale;
    }
}
