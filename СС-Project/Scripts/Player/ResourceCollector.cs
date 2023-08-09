using UnityEngine;

public class ResourceCollector : MonoBehaviour
{
    [SerializeField] private PlayerStack _playerStack;

    public PlayerStack PlayerStack => _playerStack;
}
