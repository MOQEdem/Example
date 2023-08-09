using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCGroupData", menuName = "GameAssets/NPCGroupData")]
public class NPCGroupData : ScriptableObject
{
    [SerializeField] private NPCData[] _data;

    public Bot GetNPCPrefab(NPCType type)
    {
        foreach (NPCData data in _data)
        {
            if (data.Type == type)
            {
                return data.Prefab;
            }
        }

        return null;
    }
}

[Serializable]
public class NPCData
{
    [SerializeField] private NPCType _type;
    [SerializeField] private Bot _prefab;

    public NPCType Type => _type;
    public Bot Prefab => _prefab;
}

[Serializable]
public enum NPCType
{
    Lumberman,
    Miner,
    Warrior,
    Enemy
}
