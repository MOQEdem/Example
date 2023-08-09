using UnityEngine;

public class HeavyZoneOpener : MonoBehaviour
{
    [SerializeField] private HeavyBarracks _barracks;

    private CanvasGroup _canvas;

    private void Awake()
    {
        _canvas = GetComponentInChildren<CanvasGroup>();
    }

    public void Start()
    {
        if (!_barracks.gameObject.activeSelf || !_barracks.IsBuilt)
        {
            _canvas.alpha = 0f;
            _barracks.Built += OnRequireBuildingBuilt;
        }
    }

    private void OnRequireBuildingBuilt()
    {
        _canvas.alpha = 1f;
        _barracks.Built -= OnRequireBuildingBuilt;
    }
}
