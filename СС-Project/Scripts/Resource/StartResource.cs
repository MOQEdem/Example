using UnityEngine;

public class StartResource : MonoBehaviour
{
    [SerializeField] private GameObject[] _goldPacks;

    private void Awake()
    {
        foreach (GameObject goldPack in _goldPacks)
            goldPack.SetActive(false);
    }

    public void SetStartGold(int numberOfActivePacks)
    {
        numberOfActivePacks = Mathf.Clamp(numberOfActivePacks, 0, _goldPacks.Length);

        for (int i = 0; i < numberOfActivePacks; i++)
            _goldPacks[i].SetActive(true);
    }
}
