using TMPro;
using UnityEngine;

public class RecyclerWarehouseUI : MonoBehaviour
{
    [SerializeField] private RecyclerWarehouse _recycler;
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        _recycler.Resource.BalanceChanged += OnBalanceChanged;
    }

    private void OnDisable()
    {
        _recycler.Resource.BalanceChanged -= OnBalanceChanged;
    }

    private void Start()
    {
        OnBalanceChanged(_recycler.Resource.Value);
    }

    private void OnBalanceChanged(int value)
    {
        _text.text = value.ToString();
    }
}
