using UnityEngine;

public class ForgeSound : MonoBehaviour
{
    [SerializeField] private AudioSource _warmingSound;
    [SerializeField] private AudioSource _workSound;
    [SerializeField] private AudioSource _hitSound;

    public void PlayWarmingSound()
    {
        _warmingSound.Play();
    }

    public void PlayWorkSound()
    {
        _workSound.Play();
    }

    public void PlayHitSound()
    {
        _hitSound.Play();
    }
}
