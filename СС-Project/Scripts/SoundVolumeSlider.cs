using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundVolumeSlider : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private Slider _slider;

    private static readonly string SoundVolume = nameof(SoundVolumeSlider);

    private const float _multiplier = 20f;
    private const string _mixerParameter = "MainSound";

    private void Awake()
    {
        if (!PlayerPrefs.HasKey(SoundVolume))
            PlayerPrefs.SetFloat(SoundVolume, 0.5f);

        _slider.value = PlayerPrefs.GetFloat(SoundVolume);
    }

    private void OnEnable()
    {
        SetVolume(_slider.value);
        _slider.onValueChanged.AddListener(SetVolume);
    }

    private void OnDisable()
    {
        SetVolume(_slider.value);
        _slider.onValueChanged.RemoveListener(SetVolume);
    }

    private void Start()
    {
        SetVolume(PlayerPrefs.GetFloat(SoundVolume));
    }

    private void SetVolume(float value)
    {
        var volumeValue = Mathf.Log10(value) * _multiplier;
        _mixer.SetFloat(_mixerParameter, volumeValue);
        SaveSoundSettings();
    }

    public void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat(SoundVolume, _slider.value);
    }
}
