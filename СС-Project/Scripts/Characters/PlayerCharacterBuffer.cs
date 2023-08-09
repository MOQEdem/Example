using System.Collections;
using UnityEngine;

public class PlayerCharacterBuffer : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer[] _renderers;

    private Coroutine _showingArmor;

    private void Awake()
    {
        foreach (var renderer in _renderers)
        {
            renderer.gameObject.SetActive(false);
        }
    }

    public void ShowArmor(Material material)
    {
        if (_showingArmor == null)
            _showingArmor = StartCoroutine(ShowingArmor(material));
    }

    private IEnumerator ShowingArmor(Material material)
    {
        foreach (var renderer in _renderers)
        {
            renderer.gameObject.SetActive(true);
            renderer.material = material;
        }

        // float currentDissolveValue = 1;

        //while (_renderers[0].material.GetFloat("_DissolveAmount") > 0)
        //{
        //    currentDissolveValue -= Time.deltaTime / 2;

        //    foreach (var renderer in _renderers)
        //        renderer.material.SetFloat("_DissolveAmount", currentDissolveValue);

        //    yield return null;
        //}

        yield return null;
        _showingArmor = null;
    }
}
