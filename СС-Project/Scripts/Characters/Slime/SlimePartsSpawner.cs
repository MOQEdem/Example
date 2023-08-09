using UnityEngine;

public class SlimePartsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _slimePartPrefab;
    [SerializeField] private EnemyCharacter[] _miniSlimes;

    public void Spawn()
    {
        foreach (EnemyCharacter slime in _miniSlimes)
        {
            slime.transform.parent = null;
            slime.gameObject.SetActive(true);
        }
    }
}
