using System.Collections;
using UnityEngine;

public class Healer : MonoBehaviour
{
    [SerializeField] private int _healOverSecond;
    [SerializeField] private ParticleSystem _healingParticle;

    private PlayableCharacter _player;
    private Coroutine _healing;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayableCharacter>(out PlayableCharacter player))
        {
            _player = player;

            if (_healing == null)
                _healing = StartCoroutine(Healing());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlayableCharacter>(out PlayableCharacter player))
        {
            if (_healing != null)
            {
                StopCoroutine(_healing);
                _healing = null;
            }
        }
    }

    private IEnumerator Healing()
    {
        WaitForSeconds delayBeforeHeal = new WaitForSeconds(0.5f);
        WaitForSeconds delayBetweenHeal = new WaitForSeconds(1f);

        yield return delayBeforeHeal;

        _healingParticle.transform.SetParent(_player.transform);
        _healingParticle.transform.localPosition = Vector3.up;
        _healingParticle.Play();

        while (_player.HealthFullness < 1)
        {
            _player.GetHeal((int)((float)_player.MaxHealthValue / 100) * _healOverSecond);

            yield return delayBetweenHeal;
        }

        _healingParticle.Stop();
        _healingParticle.transform.SetParent(null);

        _healing = null;
    }
}
