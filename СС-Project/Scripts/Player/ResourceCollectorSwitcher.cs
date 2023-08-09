using UnityEngine;

public class ResourceCollectorSwitcher : MonoBehaviour
{
    [SerializeField] private ResourceCollector _smallCollector;
    [SerializeField] private ResourceCollector _bigCollector;
    [SerializeField] private MeshRenderer _magnetModel;

    private void Awake()
    {
        _smallCollector.gameObject.SetActive(true);
        _bigCollector.gameObject.SetActive(false);
        _magnetModel.gameObject.SetActive(false);
    }

    public void SwitchCollectors()
    {
        _smallCollector.gameObject.SetActive(false);
        _bigCollector.gameObject.SetActive(true);
        _magnetModel.gameObject.SetActive(true);
    }
}
