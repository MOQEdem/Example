using System.Collections;
using UnityEngine;
using Cinemachine;

public class CameraShaking : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private float _duration = 0.1f;
    [SerializeField] private float _amplitudeGain = 1;
    [SerializeField] private float _frequencyGain = 1;

    private CinemachineBasicMultiChannelPerlin _perlin;
    private Coroutine _waitEndShakingCamera;

    private void Awake()
    {
        _perlin = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }   

    public void StartShaking()
    {
        if(_waitEndShakingCamera != null)
        {
            StopCoroutine(_waitEndShakingCamera);
        }
        float amplitodeMultiplier = 1;
        StartCoroutine(WaitEndShakingCamera(amplitodeMultiplier));
    }

    private IEnumerator WaitEndShakingCamera(float ampitodeMultiplier)
    {
        SetValuePerlin(_amplitudeGain * ampitodeMultiplier, _frequencyGain);
        yield return new WaitForSecondsRealtime(_duration);
        SetValuePerlin(0, 0);
    }

    private void SetValuePerlin(float amplitudeGain, float frequencyGain)
    {
        _perlin.m_AmplitudeGain = amplitudeGain;
        _perlin.m_FrequencyGain = frequencyGain;
    }
}
