using System.Collections;
using UnityEngine;

public class UnitSpawnPoint : MonoBehaviour
{
    [SerializeField] private ParticleSystem _spawnParticle;

    public void PlayParticle()
    {
        _spawnParticle.Play();
    }

    public void GrowUnitSize(Unit unit)
    {
        StartCoroutine(GrowingUnit(unit));
    }

    private IEnumerator GrowingUnit(Unit unit)
    {
        Vector3 normalSize = unit.transform.localScale;

        unit.transform.localScale = Vector3.zero;

        while (unit.transform.localScale.x < normalSize.x)
        {
            unit.transform.localScale = Vector3.MoveTowards(unit.transform.localScale, normalSize, 3f * Time.deltaTime);
            yield return null;
        }
    }
}
