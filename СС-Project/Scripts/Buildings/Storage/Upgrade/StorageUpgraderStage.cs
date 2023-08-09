using UnityEngine;

public class StorageUpgraderStage : MonoBehaviour
{
    [SerializeField] private UpgraderLevel _level;
    [SerializeField] private SkinnedMeshRenderer _mesh;
    [SerializeField] private int _capacity;

    public UpgraderLevel Level => _level;

    public int Capacity => _capacity;

    public void SetMeshStatus(bool isActive)
    {
        _mesh.gameObject.SetActive(isActive);
    }
}
