using TMPro;
using UnityEngine;

public class PopUpText : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private Vector3 _offset = new Vector3(0, 1.3f, 0);
    private Vector3 _randomizeIntensity = new Vector3(0.6f, 0, 0);

    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    public void SetText(Color color, string text)
    {
        transform.localPosition += _offset;
        transform.localPosition += new Vector3(Random.Range(-_randomizeIntensity.x, _randomizeIntensity.x), 0, 0);

        _text.color = color;
        _text.text = text;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position + _offset;
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + _camera.transform.rotation * Vector3.forward, _camera.transform.rotation * Vector3.up);
    }
}
