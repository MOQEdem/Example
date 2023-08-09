using UnityEngine;

public class PetSpawner : MonoBehaviour
{
    [SerializeField] private NPC _pet;
    [SerializeField] private Player _player;
    [SerializeField] private BuildableObject _unlockObject;

    private PetStatus _status;

    private void OnEnable()
    {
        _status = new PetStatus();
        _status.Load();

        if (_status.IsUnlocked)
            SpawnNPC();
        else
            if (_unlockObject != null)
            _unlockObject.Builded += UnlockPet;
    }

    private void OnDisable()
    {
        if (!_status.IsUnlocked)
            if (_unlockObject != null)
                _unlockObject.Builded -= UnlockPet;
    }

    private void UnlockPet()
    {
        _status.Unlock();
        SpawnNPC();

        _unlockObject.Builded -= UnlockPet;
    }

    private void SpawnNPC()
    {
        var spawnPoint = _player.transform.position + (Random.insideUnitSphere * 2);

        var npc = Instantiate(_pet, spawnPoint, _player.transform.rotation);
        npc.SetPlayer(_player);
    }
}
