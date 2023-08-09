using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ground : MonoBehaviour
{
    [SerializeField] private Material _magmaMaterial;
    [SerializeField] private Material _coalMaterial;
    [SerializeField] private Material _normalMaterial;

    private MeshRenderer _renderer;
    private Material _currentMaterial;
    private List<GroundSwitcher> _magmaSwitchers = new List<GroundSwitcher>();
    private List<GroundSwitcher> _coalSwitchers = new List<GroundSwitcher>();
    private Coroutine _changingMaterial;
    private float _baseGroundHieght;

    private void Awake()
    {
        _baseGroundHieght = this.transform.position.y;

        _renderer = GetComponent<MeshRenderer>();

        if (_normalMaterial == null)
            _normalMaterial = _renderer.material;

        _currentMaterial = _normalMaterial;
    }

    public void SetGameStartStatus(GroundSwitcher switcher, bool isAddSwitcher)
    {
        if (switcher.Type == GroundSwitcherType.Magma)
        {
            if (isAddSwitcher)
                _magmaSwitchers.Add(switcher);
            else
                _magmaSwitchers.Remove(switcher);
        }
        else
        {
            if (isAddSwitcher)
                _coalSwitchers.Add(switcher);
            else
                _coalSwitchers.Remove(switcher);
        }

        if (_magmaSwitchers.Count > 0)
        {
            SetMaterial(_magmaMaterial);
        }
        else if (_coalSwitchers.Count > 0)
        {
            SetMaterial(_coalMaterial);
        }
    }

    private void SetMaterial(Material material)
    {
        if (_currentMaterial != material)
        {
            _currentMaterial = material;
            _renderer.material = _currentMaterial;
        }
    }

    public void SetSwitcherStatus(bool isSwitcherActive, GroundSwitcher groundSwitcher)
    {
        if (isSwitcherActive)
        {
            if (groundSwitcher.Type == GroundSwitcherType.Magma)
            {
                if (!_magmaSwitchers.Contains(groundSwitcher))
                    _magmaSwitchers.Add(groundSwitcher);
            }
            else
            {
                if (!_coalSwitchers.Contains(groundSwitcher))
                    _coalSwitchers.Add(groundSwitcher);
            }
        }
        else
        {
            if (groundSwitcher.Type == GroundSwitcherType.Magma)
                _magmaSwitchers.Remove(groundSwitcher);
            else
                _coalSwitchers.Remove(groundSwitcher);
        }

        if (_currentMaterial != GetNewCurrentMaterial())
        {
            _currentMaterial = GetNewCurrentMaterial();
            StartChangingMaterial();
        }
    }

    private Material GetNewCurrentMaterial()
    {
        if (_magmaSwitchers.Count > 0)
            return _magmaMaterial;
        else if (_coalSwitchers.Count > 0)
            return _coalMaterial;
        else
            return _normalMaterial;
    }

    private void StartChangingMaterial()
    {
        StopChangingMaterial();

        var delay = Random.Range(0, 0.7f);

        _changingMaterial = StartCoroutine(ChangingMaterial(delay));
    }

    private void StopChangingMaterial()
    {
        if (_changingMaterial != null)
        {
            StopCoroutine(_changingMaterial);
            _changingMaterial = null;
        }
    }

    private IEnumerator ChangingMaterial(float delay)
    {
        yield return new WaitForSeconds(delay);

        Tween tween = transform.DOMoveY(_baseGroundHieght + 0.07f, 0.25f);
        yield return tween.WaitForCompletion();

        _renderer.material = _currentMaterial;

        tween = transform.DOMoveY(_baseGroundHieght - 0.10f, 0.25f);
        yield return tween.WaitForCompletion();

        tween = transform.DOMoveY(_baseGroundHieght, 0.2f);
        yield return tween.WaitForCompletion();

        _changingMaterial = null;
    }
}
