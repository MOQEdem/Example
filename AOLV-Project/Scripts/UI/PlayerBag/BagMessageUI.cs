using System.Collections;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BagMessageUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Image _icon;
    [SerializeField] private Color _addColor;
    [SerializeField] private Color _spendColor;
    [SerializeField] private float _lifeTime;

    private Vector3 _offset;
    private Vector3 _randomizeIntensity = new Vector3(0.6f, 0, 0);

    private Camera _camera;
    private string _prefix;

    private void Awake()
    {
        //_offset = new Vector3(Random.Range(-1f, 1f), Random.Range(0, 1f), Random.Range(-1f, 0));
        _offset = new Vector3(0, 1f, 0);
        _camera = Camera.main;
        transform.position += _offset;
        Destroy(this.gameObject, _lifeTime);

        StartCoroutine(ScalingSizeDown());
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + _camera.transform.rotation * Vector3.forward, _camera.transform.rotation * Vector3.up);
        transform.position += Vector3.up * Time.deltaTime;
    }

    public void SetMessage(int resourceValue, bool isResourceAdded, Sprite icon)
    {
        if (isResourceAdded)
        {
            _text.color = _addColor;
            _prefix = "+";
        }
        else
        {
            _text.color = _spendColor;
            _prefix = "-";
        }

        string messageText;

        if (resourceValue > 0)
            messageText = resourceValue.ToString();
        else
            messageText = "Full";

        _text.text = _prefix + messageText;
        _icon.sprite = icon;

        transform.localPosition += _offset;
        transform.localPosition += new Vector3(Random.Range(-_randomizeIntensity.x, _randomizeIntensity.x), 0, 0);
    }

    private IEnumerator ScalingSizeDown()
    {
        while (transform.localScale.x > 0)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, 1.2f * Time.deltaTime);

            yield return null;
        }

    }
}
