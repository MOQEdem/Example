using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TriggerProgressBar : MonoBehaviour
{
    [SerializeField] private ResourceSpender _spender;
    [SerializeField] private Image _image;

    private float _curretFill;
    private Coroutine _fillingBar;

    private void OnEnable()
    {
        if (_spender != null)
        {
            foreach (var holder in _spender.ResourceHolders)
            {
                holder.BalanceChanged += SetFill;
            }
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        if (_spender != null)
        {
            foreach (var holder in _spender.ResourceHolders)
            {
                holder.BalanceChanged -= SetFill;
            }
        }
    }

    private void Start()
    {
        if (_spender != null)
        {
            UpdateData();
            _image.fillAmount = _curretFill;
        }

    }

    private void SetFill(int holderValue)
    {
        UpdateData();

        if (_fillingBar == null)
            _fillingBar = StartCoroutine(FillingBar());
    }

    private void UpdateData()
    {
        var fullValue = 0;
        var progress = 0;

        foreach (var holder in _spender.ResourceHolders)
        {
            fullValue += holder.StartValue;
            progress += holder.StartValue - holder.Value;
        }

        _curretFill = (float)progress / (float)fullValue;
    }

    private IEnumerator FillingBar()
    {
        while (_image.fillAmount != _curretFill)
        {
            _image.fillAmount = Mathf.MoveTowards(_image.fillAmount, _curretFill, 1.2f * Time.deltaTime);

            yield return null;
        }

        _fillingBar = null;
    }
}
