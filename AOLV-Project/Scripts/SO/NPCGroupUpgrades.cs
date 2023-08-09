using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCGroupUpgrades", menuName = "GameAssets/NPCGroupUgrades")]
public class NPCGroupUpgrades : ScriptableObject
{
    [SerializeField] private NPCGroupUpgrade[] _upgradesBook;

    public NPCGroupUpgrade[] UpgradesBook => _upgradesBook;
    public int UpgradesCount => _upgradesBook.Length;
}

[Serializable]
public class NPCGroupUpgrade
{
    [SerializeField] private int _cost;
    [SerializeField] private ResourceType _resourceType;
    [SerializeField] private int _countOfNPC;

    public int Cost => _cost;
    public ResourceType ResourceType => _resourceType;
    public int CountOfNPC => _countOfNPC;
}
