using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecyclerUI : MonoBehaviour
{
    [SerializeField] private Image _demandedResource;
    [SerializeField] private TMP_Text _demandedValue;
    [SerializeField] private Image _spawnedResource;
    [SerializeField] private TMP_Text _spawnedValue;
    [SerializeField] private ResourcesData _data;
    [SerializeField] private ResourceRecycler _recycler;

    private void Start()
    {
        _demandedResource.sprite = _data.GetIcon(_recycler.RequiredResourceType);
        _spawnedResource.sprite = _data.GetIcon(_recycler.ProducedResourceType);
        _demandedValue.text = _recycler.RequiredResourceValue.ToString();
        _spawnedValue.text = _recycler.ProducedResourceValue.ToString();
    }
}
