using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NPCGroupList : SavedObject<NPCGroupList>
{
    private const string SaveKey = nameof(NPCGroupList);

    [SerializeField] private List<NPCType> _npcList = new List<NPCType>();

    public List<NPCType> NPCList => _npcList;

    public NPCGroupList()
        : base(SaveKey)
    { }

    public void AddNPC(NPCType npc)
    {
        _npcList.Add(npc);
        Save();
    }

    public void RemoveNPC(NPCType npc)
    {
        _npcList.Remove(npc);
        Save();
    }

    public void RemoveAll()
    {
        _npcList.Clear();
        Save();
    }

    protected override void OnLoad(NPCGroupList loadedObject)
    {
        _npcList = loadedObject.NPCList;
    }
}
