using System.Collections;
using System.Collections.Generic;
using Agava.VKGames;
using Agava.YandexGames;
using Agava.YandexMetrica;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VideoAd = Agava.YandexGames.VideoAd;

public class HubHeap : MonoBehaviour
{
    [SerializeField] private Transform _OffObjekt;
    [SerializeField] private List<ResourceHolder> _holders;
    [SerializeField] private int _reward;
    [SerializeField] private ResourcesData _data;
    [SerializeField] private ResourceBag _bag;
    [SerializeField] private Resizer _resizer;
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private ResourceType _resourceType;
    
    private Vector3 _normalScale;
    private float _resizeTime;
    private float _normalTimeScale;
    
    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
        _normalTimeScale = Time.timeScale;
    }
    
    private void Start()
    {
        _text.text = "+" + _reward;
        _normalScale = transform.localScale;
        _resizer.Resize(0.1f, 0.01f);
    }

    private void OnEnable()
    {
        _bag.HolderEmpty += ShowCanvas;
    }

    private void OnDisable()
    {
        _bag.HolderEmpty -= ShowCanvas;
    }

    private void ShowCanvas(ResourceType type)
    {
        _resizer.Resize(_resizeTime, _normalScale);
        _icon.sprite = _data.GetIcon(_resourceType);
    }
    
    public void onClosedButtonClick()
    {
        _resizer.Resize(0.1f, 0.01f);
    }

    public void onRewardButtonClick()
    {
        Time.timeScale = 0;
        VideoAd.Show(onRewardedCallback: Reward, onCloseCallback: PlayScale);
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
        _OffObjekt.gameObject.SetActive(false);
    }
    
    private void PlayScale()
    {
        Time.timeScale = _normalTimeScale;
    }
}
