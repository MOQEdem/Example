using UnityEngine;

public class Dismounter : MonoBehaviour
{
    [SerializeField] private Collider _selfCollider;
    [SerializeField] private GameObject _groundSwitcher;
    [SerializeField] private CharacterMove _characterMove;

    private void Start() => 
        _characterMove.SetSpeedMove(0f);

    public void MakeDismount()
    {
        transform.parent = null;
        _selfCollider.enabled = true;
        _groundSwitcher.SetActive(true);
        _characterMove.SetSpeedMove(_characterMove.Speed);
    }
}
