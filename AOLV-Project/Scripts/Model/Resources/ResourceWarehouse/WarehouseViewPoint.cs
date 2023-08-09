using UnityEngine;

[RequireComponent(typeof(Resizer))]
public class WarehouseViewPoint : MonoBehaviour
{
    private ResourceModel _model;
    private Resizer _resizer;
    private Vector3 _normalScale;

    public Resizer Resizer => _resizer;

    private void Awake()
    {
        _resizer = GetComponent<Resizer>();
        _normalScale = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    public void PlaceResource(ResourceModel model)
    {
        if (_model == null)
            _model = Instantiate(model, this.transform);

        _resizer.Resize(1f, _normalScale);
    }

    public void ReplaceResource(float delay)
    {
        if (_model != null)
        {
            Destroy(_model.gameObject, delay);
            _model = null;
        }
    }
}
