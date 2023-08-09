using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCGroup : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private NPCGroupData _data;
    [SerializeField] private int _maxGroupCount;
    [SerializeField] private bool _isNeedToSpawn;

    private NPCGroupList _groupList;

    public List<NPCType> NPCList => _groupList.NPCList;
    public bool IsFreeSlot => _groupList.NPCList.Count < _maxGroupCount;

    private void Awake()
    {
        _groupList = new NPCGroupList();
        _groupList.Load();

        if (SceneManager.GetActiveScene().name == SceneName.NewHub)
            _groupList.RemoveAll();

        if (_isNeedToSpawn)
            foreach (var npc in _groupList.NPCList)
                SpawnNPC(npc);
    }

    public void AddNPC(NPCType type)
    {
        _groupList.AddNPC(type);

        if (_isNeedToSpawn)
            SpawnNPC(type);
    }

    public void RemoveNPC(NPCType type)
    {
        _groupList.RemoveNPC(type);
    }

    private void SpawnNPC(NPCType type)
    {
        var spawnPoint = _player.transform.position + (Random.insideUnitSphere * 2);

        var npc = Instantiate(_data.GetNPCPrefab(type), spawnPoint, _player.transform.rotation);
        npc.Died += OnNPCDied;
        npc.SetPlayer(_player);
    }

    private void OnNPCDied(Unit unit)
    {
        if (unit is Bot npc)
        {
            unit.Died -= OnNPCDied;
            RemoveNPC(npc.NPCType);
        }
    }
}