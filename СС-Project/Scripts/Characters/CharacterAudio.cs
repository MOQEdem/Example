using System.Collections;
using UnityEngine;

public class CharacterAudio : MonoBehaviour
{
    [SerializeField] private AudioSource[] _runs;
    [SerializeField] private AudioSource[] _attacks;
    [SerializeField] private AudioSource _die;
    [SerializeField] private AudioSource _victory;
    [SerializeField] private AudioSource _jump;
    [SerializeField] private AudioSource _fall;
    [SerializeField] private AudioSource _resurrection;

    private Coroutine _running;

    public void Run(bool isRun)
    {
        if (isRun)
        {
            if (_running == null)
                _running = StartCoroutine(PlayingRunSound());
        }
        else
        {
            if (_running != null)
            {
                StopCoroutine(_running);
                _running = null;
            }
        }
    }

    public void Jump(bool isJump)
    {
        if (isJump)
            _jump.Play();
        else
            _fall.Play();
    }

    public void Attack()
    {
        int randomSondIndex = Random.Range(0, _attacks.Length);
        _attacks[randomSondIndex].Play();
    }

    public void Die()
    {
        _die.Play();
    }

    public void Victory()
    {
        _victory.Play();
    }

    public void Resurrect()
    {
        _resurrection.Play();
    }

    private IEnumerator PlayingRunSound()
    {
        WaitForSeconds delay;

        while (true)
        {
            int randomSoundIndex = Random.Range(0, _runs.Length);
            _runs[randomSoundIndex].Play();

            delay = new WaitForSeconds(_runs[randomSoundIndex].clip.length);

            yield return delay;
        }
    }
}
