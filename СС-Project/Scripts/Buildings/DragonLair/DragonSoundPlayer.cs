using System.Collections;
using UnityEngine;

public class DragonSoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource[] _flyings;
    [SerializeField] private AudioSource[] _attacks;
    [SerializeField] private AudioSource _jump;
    [SerializeField] private AudioSource _fall;

    private Coroutine _flying;

    public void Run(bool isRun)
    {
        if (isRun)
        {
            if (_flying == null)
                _flying = StartCoroutine(PlayingFlySound());
        }
        else
        {
            if (_flying != null)
            {
                StopCoroutine(_flying);
                _flying = null;
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

    private IEnumerator PlayingFlySound()
    {
        WaitForSeconds delay;

        while (true)
        {
            int randomSoundIndex = Random.Range(0, _flyings.Length);
            _flyings[randomSoundIndex].Play();

            delay = new WaitForSeconds(_flyings[randomSoundIndex].clip.length);

            yield return delay;
        }
    }
}
