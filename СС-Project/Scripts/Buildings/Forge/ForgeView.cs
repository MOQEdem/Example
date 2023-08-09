using UnityEngine;
using TMPro;

public class ForgeView : MonoBehaviour
{
    [SerializeField] private TMP_Text _currentRecources;

    private Forge _forge;

    private void Awake()
    {
        _forge = GetComponent<Forge>();
    }

    private void OnEnable()
    {
        _forge.CountChange += OnCountChange;
    }

    private void OnDisable()
    {
        _forge.CountChange -= OnCountChange;
    }

    private void OnCountChange()
    {
        _currentRecources.text = $"{_forge.ResourceCount}/{_forge.MaxBuffCount}";
    }
}
